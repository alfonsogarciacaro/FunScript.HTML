[<ReflectedDefinition>]
module FunScript.HTML.Event

open System
open FunScript.TypeScript

type RactiveEventStream(ractive: Ractive, eventName: string) =
    interface IObservable<RactiveEvent*obj[]> with
        member x.Subscribe observer =
            let remover = ractive.on(eventName, (fun ev args -> observer.OnNext(ev, args)))
            new FunScript.Core.Events.ActionDisposable(fun () -> remover.cancel()) :> IDisposable

