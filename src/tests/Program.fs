module FunScript.HTML.Tests

open System
open System.Collections.Generic
open FunScript.TypeScript

[<ReflectedDefinition>]
let mainVanillaJS() =
    let doc = Globals.document
    let results = doc.getElementById("result")

    let appendResult result =
        let p = doc.createElement_p()
        let t = doc.createTextNode(result)
        p.appendChild(t) |> ignore
        results.appendChild(p) |> ignore

    doc.getElementById("buttonClear").onclickStream
    |> Observable.add (fun ev -> results.innerHTML <- "")

    let stream1 =
        doc.getElementById("button1").onclickStream
        |> Observable.map (fun ev -> 1)

    doc.getElementById("button2").onclickStream
    |> Observable.map (fun ev -> 2)
    |> Observable.merge stream1
    |> Observable.map (fun id ->
        match id with
        | 1 -> id, 1, 0
        | 2 -> id, 0, 1
        | _ -> failwith "Unknown button")
    |> Observable.scan (fun (_, st1, st2) (id, click1, click2) -> id, st1 + click1, st2 + click2) (0,0,0)
    |> Observable.add (fun (id, clicks1, clicks2) ->
        let clicks = match id with 1 -> clicks1 | 2 -> clicks2 | _ -> failwith "Unknown button"
        let s = String.Format("Button {0} clicked {1} time{2}!", id, clicks, if clicks>1 then "s" else "")
        appendResult s)

//    let rec waiter(state1: int, state2: int): Async<unit> = async {
//        let button1 = doc.getElementById("button1")
//        let button2 = doc.getElementById("button2")
//
//        let! choice = Async.AwaitObservable2(button1.onclickStream, button2.onclickStream)
//        let buttonid, state, state1, state2 =
//            match choice with
//            | Choice1Of2 _ -> 1, state1+1, state1+1, state2
//            | Choice2Of2 _ -> 2, state2+1, state1, state2+1
//
//        appendResult <| String.Format("Button {0} clicked {1} time{2}!", buttonid, state, if state>1 then "s" else "")
//        return! waiter(state1, state2)
//    }
//    Async.StartImmediate(waiter(0, 0))

[<ReflectedDefinition>]
let mainRactive() =
    let data = new Dictionary<_,_>()
    data.Add("name", "world")

    let options = createEmpty<RactiveNewOptions>()
    options.template <- "#template"
    options.el <- "container"
    options.data <- data
    let ractive = Globals.Ractive.Create(options)

//    ractive.on("activate", fun ev args -> Globals.alert("activating"))

//    ractive.onStream("activate")
//    |> Observable.add (fun (ev, args) -> let i = unbox args.[0] |> (+) -1 in Globals.alert(unbox args.[i]))

    let waiter(): Async<unit> = async {
        let! choice = Async.AwaitRactiveEvent2(ractive, "activate", "fire")
        match choice with
        | Choice1Of2 _ -> Globals.alert("activating")
        | Choice2Of2 _ -> Globals.alert("firing")
    }
    Async.StartImmediate(waiter())






// Translate the code above to JS and open the html
open System.IO

let path = Directory.GetCurrentDirectory()
let code = FunScript.Compiler.compileWithoutReturn(<@ mainRactive() @>)
let html = File.ReadAllText(Path.Combine(path, "..\..\index.html"))
               .Replace("""<script id="funscript"></script>""", (sprintf "<script>%s</script>" code))

let htmlPath = Path.Combine(path, "index.html")
File.WriteAllText(htmlPath, html)
System.Diagnostics.Process.Start(Path.Combine(path, "index.html")) |> ignore





