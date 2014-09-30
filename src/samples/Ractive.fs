[<ReflectedDefinition>]
module FunScript.HTML.Samples.Ractive

open System.Collections.Generic
open FunScript.TypeScript
open FunScript.HTML

type FunnyImage = { image: string; text: string }

let test1() =
    let images =
        ResizeArray<_> [| {image="pipeline"; text="The deployment pipeline"}
                          {image="rewrite"; text="What happens when I am allowed to rewrite code from scratch"}
                          {image="mutation"; text="x=!x"} |]

    let rec loop index (state: RactiveState<FunnyImage>): Async<unit> = async {
        let! ev = Async.AwaitObservable(state.ractive.onStream("changeImage"))
        let index = if index < images.Count - 1 then index + 1 else 0
        let! state = RactiveState.mkAsync(state, images.[index], hasOutTransitions=true)
        return! loop index state
    }

    let ractive = Globals.Ractive.CreateFast("#container1", "#template1")
    RactiveState.init(ractive, images.[0])
    |> loop 0
    |> Async.StartImmediate

// Utility function to make adding values to a dictionary more F#esque
let add (key: _) (value: obj) (dic: Dictionary<_,_>) =
    dic.Add(key, unbox value)
    dic

let createRactive (el: string) (template: string) (data: obj) =
    let options = createEmpty<RactiveNewOptions>()
    options.template <- template
    options.el <- el
    options.data <- data
    Globals.Ractive.Create(options)

let test2() =
    let data = createEmpty()
    let r = createRactive "#container2" "#template2" data

    let rec waiter(): Async<unit> = async {
        let ev1, ev2 = r.onStream("activate2", "fire2")
        let! choice = Async.AwaitObservable(ev1, ev2)
        match choice with
        | Choice1Of2 _ -> Globals.alert("activating")
        | Choice2Of2 _ -> Globals.alert("firing")

        return! waiter() // Remain in the loop to keep listening for the event
    }

    let cts = new System.Threading.CancellationTokenSource()
    Async.StartImmediate(waiter(), cts.Token)
    r.on("remove2", fun ev args -> cts.Cancel()) |> ignore

let test3() =
    let data = createEmpty()
    let ractive = createRactive "#container3" "#template3" data
    
    // Note that you can send an argument array through Ractive proxy events, check the html
    let subscriber =
        ractive.onStream("activate3") |> Observable.merge (ractive.onStream("fire3"))
        |> Observable.subscribe (fun (ev, arg) -> Globals.alert(unbox arg))
    ractive.on("remove3", fun ev args -> subscriber.Dispose()) |> ignore

    // The lines above have the same effect as writing the following commented lines.
    // Note that you need to wrap the F# passed as callbacks if you don't use a lambda directly,
    // this is the same behaviour as when calling C# methods from F#

//    let alert (ev: RactiveEvent) (arg: obj) = Globals.alert(unbox arg)
//    let observer1 = ractive.on("activate3", System.Func<_,_,_>(alert))
//    let observer2 = ractive.on("fire3", System.Func<_,_,_>(alert))
//    ractive.on("remove3", fun ev arg -> observer1.cancel(); observer2.cancel()) |> ignore


let test4() =
    let data =
        Dictionary<_,_>()
        |> add "red" 0.45
        |> add "green" 0.61
        |> add "blue" 0.2
    createRactive "#container4" "#template4" data
    |> ignore

let main() =
    test1()
    test2()
    test3()
    test4()
