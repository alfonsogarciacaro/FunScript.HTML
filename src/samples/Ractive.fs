[<ReflectedDefinition>]
module FunScript.HTML.Samples.Ractive

open System.Collections.Generic
open FunScript.TypeScript
open FunScript.HTML.Extensions

// Utility function to make adding values to a dictionary more F#esque
let add (key: _) (value: obj) (dic: Dictionary<_,_>) =
    dic.Add(key, unbox value)
    dic

let createRactive (el: string) (template: string) (data: obj) =
    let options = createEmpty<RactiveNewOptions>()
    options.template <- template
    options.el <- el
    options.data <- data
    Globals.Ractive.Create(unbox options)

let test1() =
    let data = Dictionary<_,_>() |> add "name" "World"
    createRactive "#container1" "#template1" data |> ignore

let test2() =
    let data = createEmpty()
    let r = createRactive "#container2" "#template2" data
    let rec waiter(): Async<unit> = async {
        let! choice = Async.AwaitObservable2(r.onStream("activate2"), r.onStream("fire2"))
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
        |> Observable.subscribe (fun (ev, args) -> Globals.alert(unbox args.[0]))
    ractive.on("remove3", fun ev args -> subscriber.Dispose()) |> ignore

    // The lines above have the same effect as writing the following commented lines.
    // Note that you need to wrap the F# passed as callbacks if you don't use a lambda directly,
    // this is the same behaviour as when calling C# methods from F#

//    let alert (ev: RactiveEvent) (args: obj[]) = Globals.alert(unbox args.[0])
//    let observer1 = ractive.on("activate3", System.Func<_,_,_>(alert))
//    let observer2 = ractive.on("fire3", System.Func<_,_,_>(alert))
//    ractive.on("remove3", fun ev args -> observer1.cancel(); observer2.cancel()) |> ignore


let test4() =
    let data =
        Dictionary<_,_>()
        |> add "red" 0.45
        |> add "green" 0.61
        |> add "blue" 0.2
    createRactive "#container4" "#template4" data |> ignore

let main() =
    test1()
    test2()
    test3()
    test4()
