[<ReflectedDefinition>]
module FunScript.HTML.Extensions

open FunScript.TypeScript
open FunScript.HTML.Event

type Ractive with
    member x.onStream(eventName: string) =
        RactiveEventStream(x, eventName)

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
