namespace FunScript.HTML

open System
open FunScript.TypeScript

[<AutoOpen>]
[<ReflectedDefinition>]
module AsyncExtensions =

    type Async with
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
        member req.AsyncGetString() =
            let req: FunScript.Core.Web.WebRequest = unbox req
            Async.FromContinuations(fun (onSuccess, onError, _) ->
                let onReceived(data : string) = onSuccess data
                let onErrorReceived() = onError null
                let body =
                    if req.Method = "GET" then null
                    else Text.Encoding.UTF8.GetString(req.GetRequestStream().Contents)
                FunScript.Core.Web.sendRequest(
                    req.Method, req.Url, req.Headers.Keys, req.Headers.Values, 
                    body, onReceived, onErrorReceived)
            )
        member req.AsyncGetJSON() = async {
            let! data = req.AsyncGetString()
            return! unbox <| Globals.JSON.parse(data)
        }
