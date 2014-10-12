namespace FunScript.HTML

open System
open FunScript.TypeScript

[<AutoOpen; ReflectedDefinition>]
module AsyncExtensions =

    [<FunScript.JSEmitInline("window.setInterval({0}, {1})")>]
    let private setInterval(handler:unit -> unit, milliseconds:float): int = failwith "never"

    [<FunScript.JSEmitInline("window.clearInterval({0})")>]
    let private clearInterval(id: int) = failwith "never"

    [<FunScript.JSEmitInline("window.requestAnimationFrame({0})")>]
    let private requestAnimationFrame(handler:float -> unit): int = failwith "never"

    type ElapsedEventArgs() =
        member val SignalTime = System.DateTime.Now

    /// The timer will be disposed at the same time as the subscriber (ms: interval in milliseconds)
    type TimerStream(ms) =
        interface IObservable<ElapsedEventArgs> with
            member x.Subscribe observer =
                let handler = fun () -> observer.OnNext(ElapsedEventArgs())
                let id = setInterval(handler, ms)
                new FunScript.Core.Events.ActionDisposable(fun () ->
                    clearInterval(id)) :> IDisposable

    type System.Timers.Timer with
        /// The timer will be disposed at the same time as the subscriber (ms: interval in milliseconds)
        static member CreateStream(ms) = TimerStream(ms)

    type Async with
        /// Use this if you want to have animations in async workflows that can be easily cancelled
        static member AwaitAnimationFrame(): Async<float> =
            unbox(FunScript.Core.Async.protectedCont <| fun k ->
                requestAnimationFrame(fun ts ->
                    k.Aux.CancellationToken.ThrowIfCancellationRequested()
                    k.Cont ts) |> ignore)

        [<CompiledName("AwaitObservable1")>]
        static member AwaitObservable(w1: IObservable<'T>): Async<'T> =
            unbox(FunScript.Core.Async.protectedCont <| fun k ->
                let remover: System.IDisposable ref = ref null
                let observer = FunScript.Core.Events.ActionObserver(fun value ->
                    (!remover).Dispose()
                    k.Aux.CancellationToken.ThrowIfCancellationRequested()
                    k.Cont value)
                remover := (unbox w1: FunScript.Core.Events.IObservable<'T>).Subscribe(observer))
      
        [<CompiledName("AwaitObservable2")>]
        static member AwaitObservable(ev1:IObservable<_>, ev2:IObservable<_>) =
            let ev1 = Observable.map Choice1Of2 ev1
            let ev2 = Observable.map Choice2Of2 ev2
            Async.AwaitObservable(Observable.merge ev1 ev2)

        [<CompiledName("AwaitObservable3")>]
        static member AwaitObservable(ev1:IObservable<_>, ev2:IObservable<_>, ev3:IObservable<_>) =
            let ev1 = Observable.map Choice1Of3 ev1
            let ev2 = Observable.map Choice2Of3 ev2
            let ev3 = Observable.map Choice3Of3 ev3
            Async.AwaitObservable(Observable.merge ev1 ev2 |> Observable.merge ev3)

        [<CompiledName("AwaitObservable4")>]
        static member AwaitObservable(ev1:IObservable<_>, ev2:IObservable<_>, ev3:IObservable<_>, ev4:IObservable<_>) =
            let ev1 = Observable.map Choice1Of4 ev1
            let ev2 = Observable.map Choice2Of4 ev2
            let ev3 = Observable.map Choice3Of4 ev3
            let ev4 = Observable.map Choice4Of4 ev4
            Async.AwaitObservable(Observable.merge ev1 ev2 |> Observable.merge ev3 |> Observable.merge ev4)

    type Net.WebRequest with
        member req.AsyncGetJSON<'T>() =
            let req: FunScript.Core.Web.WebRequest = unbox req
            Async.FromContinuations(fun (onSuccess, onError, _) ->
                let onReceived(data) = onSuccess(unbox<'T>(Globals.JSON.parse data))
                let onErrorReceived() = onError(null)
                FunScript.Core.Web.sendRequest(
                    "GET", req.Url, req.Headers.Keys, req.Headers.Values, 
                    null, onReceived, onErrorReceived)
            )
        member req.AsyncPostJSON<'T>(data: 'T) =
            let req: FunScript.Core.Web.WebRequest = unbox req
            req.Headers.Add("Content-Type", "application/json")
            Async.FromContinuations(fun (onSuccess, onError, _) ->
                let onReceived(data) = onSuccess(unbox<'T>(Globals.JSON.parse data))
                let onErrorReceived() = onError(null)
                FunScript.Core.Web.sendRequest(
                    "POST", req.Url, req.Headers.Keys, req.Headers.Values, 
                    Globals.JSON.stringify(data), onReceived, onErrorReceived)
            )
