[<ReflectedDefinition>]
module FunScript.HTML.Event

open System
open FunScript.TypeScript

type HTMLEventStream<'T>(el: EventTarget, ev: string) =
    interface IObservable<'T> with
        member x.Subscribe observer =
            let listener = unbox (fun ev -> observer.OnNext(ev))
            el.addEventListener(ev, listener, false)
            new FunScript.Core.Events.ActionDisposable(fun () ->
                el.removeEventListener(ev, listener, false)) :> IDisposable