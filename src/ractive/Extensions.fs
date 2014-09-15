namespace FunScript.HTML

open System
open FunScript.TypeScript

[<AutoOpen>]
[<ReflectedDefinition>]
module RactiveExtensions =
    type RactiveEventStream(ractive: Ractive, eventName: string) =
        interface IObservable<RactiveEvent*obj[]> with
            member x.Subscribe observer =
                let remover = ractive.on(eventName, (fun ev args -> observer.OnNext(ev, args)))
                new FunScript.Core.Events.ActionDisposable(fun () -> remover.cancel()) :> IDisposable

    type Ractive with
        [<CompiledName("onStream1")>]
        member x.onStream(eventName: string) =
            RactiveEventStream(x, eventName)

        [<CompiledName("onStream2")>]
        member x.onStream(eventName1: string, eventName2: string) =
            RactiveEventStream(x, eventName1), RactiveEventStream(x, eventName2)

        [<CompiledName("onStream3")>]
        member x.onStream(eventName1: string, eventName2: string, eventName3: string) =
            RactiveEventStream(x, eventName1), RactiveEventStream(x, eventName2), RactiveEventStream(x, eventName3)

        [<CompiledName("onStream4")>]
        member x.onStream(eventName1: string, eventName2: string, eventName3: string, eventName4: string) =
            RactiveEventStream(x, eventName1), RactiveEventStream(x, eventName2), RactiveEventStream(x, eventName3), RactiveEventStream(x, eventName4)


    type RactiveEventPlugin with
        static member makeCustomKeyEvent keyCode =
            let plugin =
                fun (node: HTMLElement) (fire: System.Func<RactiveEvent,obj>) ->
                    let keydownHandler =
                        EventListenerDelegate( 
                            fun (ev: Event) ->
                                if (ev :?> KeyboardEvent).which = (float keyCode) then
                                    let f = createEmpty<RactiveEvent>() in f.node <- node; f.original <- ev
                                    fire.Invoke(f) |> ignore )

                    node.addEventListener("keydown", keydownHandler, false)
                    let teardown = System.Collections.Generic.Dictionary<_,_>()
                    teardown.Add("teardown", fun () -> node.removeEventListener("keydown", keydownHandler, false))
                    teardown
            unbox<RactiveEventPlugin> (System.Func<_,_,_>(plugin))
