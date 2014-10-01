[<ReflectedDefinition>]
module FunScript.HTML.Samples.VanillaJS

open System
open System.Collections.Generic
open FunScript.TypeScript
open FunScript.HTML

// This literal here is OK, but see comment below at the beginning of test1FRP()
let LEFT_CLICK = 1.

// Functional Reactive Programming: use the following types and utility functions to carry state without mutable variables
// This test is largely based on: http://steellworks.blogspot.com.es/2014/03/tutorial-functional-reactive.html
type DragState = { dragging : bool; position: float*float; offset: float*float }
type DragChange = StartDrag of float*float*float*float | StopDrag | UpdatePosition of float*float

let update_drag_state (state : DragState) (change : DragChange) : DragState =
    match change with
    | StartDrag(x, y, x', y') -> { state with dragging=true; position=(x,y); offset=(x',y') }
    | StopDrag ->                { state with dragging=false }
    | UpdatePosition(x, y) ->    if state.dragging then { state with position=(x,y) } else state

let get_mouse_position_and_offset (rel_to : HTMLElement) (ev: MouseEvent) =
    let rect = rel_to.getBoundingClientRect()
    ev.x, ev.y, ev.x - rect.left, ev.y - rect.top

let set_element_position (el: HTMLElement) ((x,y): float*float) : unit =
    el.style.left <- String.Format("{0}px", x)
    el.style.top <- String.Format("{0}px", y)


let test1FRP() =

    // If your FunScript project is compiling itself to JavaScript like in this case, you must be careful not to place
    // non-literal values outside functions or you'll likely get Runtime errors (either in F# or JS)
    let initial_state = { dragging=false; position=(0.,0.); offset=(0.,0.) }
    let svg = Globals.document.getElementsByTagName("svg").[0] |> unbox<HTMLElement>

    let start_stream =
        Globals.window.onmousedownStream
        |> Observable.filter (fun ev -> ev.which = LEFT_CLICK)
        |> Observable.map ((get_mouse_position_and_offset svg) >> StartDrag)
        
    let stop_stream =
        Globals.window.onmouseupStream
        |> Observable.filter (fun ev -> ev.which = LEFT_CLICK)
        |> Observable.map (fun _ -> StopDrag)
        
    let move_stream =
        Globals.window.onmousemoveStream
        |> Observable.map (fun ev -> UpdatePosition(ev.x, ev.y))

    Observable.merge start_stream stop_stream |> Observable.merge move_stream
    |> Observable.scan update_drag_state initial_state
    |> Observable.filter (fun state -> state.dragging)
    |> Observable.map (fun state -> (fst state.position) - (fst state.offset), (snd state.position) - (snd state.offset))
    |> Observable.add (set_element_position svg)


// With Async workflows we can create state machines and represent part of the state in the workflow itself,
// so here we don't need the dragging flag nor the DragChange type
type PositionState = { position: float*float; offset: float*float }

let test1Async() =
    let initial_state = { position=(0.,0.); offset=(0.,0.) }
    let svg = Globals.document.getElementsByTagName("svg").[1] |> unbox<HTMLElement>

    let rec draggingLoop(state: PositionState): Async<unit> = async {
        // We wait for either mousemove or mouseup events and use pattern matching to get the actual event
        let! choice = Async.AwaitObservable(Globals.window.onmousemoveStream, Globals.window.onmouseupStream)
        match choice with

        // If the mouse moves update the position
        | Choice1Of2 ev ->
            let state = { state with position=(ev.x, ev.y) }
            set_element_position svg ((fst state.position) - (fst state.offset), (snd state.position) - (snd state.offset))
            return! draggingLoop(state)

        // If the left button is up, we don't do anything so we leave the dragging loop
        | Choice2Of2 ev  when ev.which = LEFT_CLICK -> ()
        
        // Stay within the loop without changing the state
        | _ -> return! draggingLoop(state)
    }

    let rec waitingLoop(state: PositionState): Async<unit> = async {
        let! ev = Async.AwaitObservable(Globals.window.onmousedownStream)
        
        // Enter dragging loop if left button is down
        if (ev.which = LEFT_CLICK) then
            let x,y,x',y' = get_mouse_position_and_offset svg ev
            do! draggingLoop({ position=(x,y); offset=(x',y') })

        return! waitingLoop(state)
    }
    Async.StartImmediate <| waitingLoop(initial_state)


let appendResult (doc: Document) (results: HTMLElement) result =
    let p = doc.createElement_p()
    let t = doc.createTextNode(result)
    p.appendChild(t) |> ignore
    results.appendChild(p) |> ignore

let test2() =
    let doc = Globals.document
    let results = doc.getElementById("resultsAsync")
    doc.getElementById("buttonAsyncClear").onclick <- (fun ev -> results.innerHTML <- ""; null)

    let rec waiter(state1: int, state2: int): Async<unit> = async {
        let button1 = doc.getElementById("buttonAsync1")
        let button2 = doc.getElementById("buttonAsync2")

        let! choice = Async.AwaitObservable(button1.onclickStream, button2.onclickStream)
        let buttonid, state, state1, state2 =
            match choice with
            | Choice1Of2 _ -> 1, state1+1, state1+1, state2
            | Choice2Of2 _ -> 2, state2+1, state1, state2+1

        let s = String.Format("Button {0} clicked {1} time{2}!", buttonid, state, if state>1 then "s" else "")
        appendResult doc results s
        return! waiter(state1, state2) // Remain in the loop to keep listening for the event
    }

    let cts = new System.Threading.CancellationTokenSource()
    Async.StartImmediate(waiter(0, 0), cts.Token)
    doc.getElementById("buttonAsyncRemove").onclick <- fun _ -> cts.Cancel(); null


// Same as the test above but using 'event streams'
// If we don't need to remove the listeners, we can just use add instead of subscribe
let test3() =
    let doc = Globals.document
    let results = doc.getElementById("resultsFRP")

    let stream1 =
        doc.getElementById("buttonFRP1").onclickStream
        |> Observable.map (fun ev -> 1)

    let stream2 =
        doc.getElementById("buttonFRP2").onclickStream
        |> Observable.map (fun ev -> 2)

    let subscriber =
        Observable.merge stream1 stream2
        |> Observable.map (fun id ->
            match id with
            | 1 -> id, 1, 0
            | 2 -> id, 0, 1
            | _ -> failwith "Unknown button")
        |> Observable.scan (fun (_, st1, st2) (id, click1, click2) -> id, st1 + click1, st2 + click2) (0,0,0)
        |> Observable.subscribe (fun (id, clicks1, clicks2) ->
            let clicks = match id with 1 -> clicks1 | 2 -> clicks2 | _ -> failwith "Unknown button"
            let s = String.Format("Button {0} clicked {1} time{2}!", id, clicks, if clicks>1 then "s" else "")
            appendResult doc results s)

    doc.getElementById("buttonFRPClear").onclick <- fun _ -> results.innerHTML <- ""; null
    doc.getElementById("buttonFRPRemove").onclick <- fun _ -> subscriber.Dispose(); null


open System.Text.RegularExpressions
let testRegex() =
    let doc = Globals.document
    let result = doc.getElementById("regex-result")
    let input = doc.getElementById("regex-input") |> unbox<HTMLTextAreaElement>
    let pattern = doc.getElementById("regex-pattern") |> unbox<HTMLTextAreaElement>
    pattern.onkeyupStream
    |> Observable.add (fun _ ->
        result.innerHTML <- Regex.Replace(
            input.value, pattern.value, "<mark>$&</mark>", RegexOptions.IgnoreCase))

type Point = {x: float; y: float}
             static member (-) (a: Point, b: Point) = {x=a.x-b.x; y=a.y-b.y}
             member p.Magnitude with get() = sqrt <| (pown p.x 2) + (pown p.y 2)

let main() =
    test1FRP()
    test1Async()
    test2()
    test3()
    testRegex()

    // Additional tests
    Globals.document.onclickStream
    |> Observable.filter (fun c -> c.clientX > Globals.window.innerWidth / 2.)
    |> Observable.take 10
    |> Observable.map (fun c -> {x=c.clientX; y=c.clientY})
    |> Observable.pairwise
    |> Observable.map (fun pair -> ((snd pair) - (fst pair)).Magnitude)
    |> Observable.scan (fun (st: float) dist -> dist + st) 0.
    |> Observable.add (fun dist ->
        System.Console.WriteLine(sprintf "Accumulated distance: %.2f" dist))

    System.Timers.Timer.CreateStream(2000.)
    |> Observable.take 5
    |> Observable.add (fun e ->
        System.Console.WriteLine("{0:yyyy/MM/dd HH:mm:ss}", e.SignalTime))
