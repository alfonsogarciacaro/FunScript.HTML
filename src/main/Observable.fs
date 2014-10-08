namespace Microsoft.FSharp.Control

[<assembly:AutoOpen("Microsoft.FSharp.Control")>]
do()

module Observable =
    open System

    [<CompiledName("Take")>]
    let take (count: int) (w: IObservable<'T>): IObservable<'T> = failwith "never"

    [<CompiledName("TakeWhile")>]
    let takeWhile (predicate:unit->bool) (w: IObservable<'T>): IObservable<'T> = failwith "never"

    [<CompiledName("Skip")>]
    let skip (count: int) (w: IObservable<'T>): IObservable<'T> = failwith "never"

    [<CompiledName("SkipWhile")>]
    let skipWhile (predicate:unit->bool) (w: IObservable<'T>): IObservable<'T> = failwith "never"
    

namespace FunScript.HTML

[<ReflectedDefinition>]
module ObservableExtensions =
    open FunScript.Core.Events

    type private TakeObservable<'T>(count: int, w: IObservable<'T>) =
        interface IObservable<'T> with
            member x.Subscribe(observer) =
                let state = ref 0
                let remover: System.IDisposable ref = ref null
                let newObserver =
                    ActionObserver<_>(
                        onNext = (fun v -> if !state < count then state := !state + 1; observer.OnNext(v) else (!remover).Dispose()),
                        onError = (fun e -> observer.OnError(e)),
                        onCompleted = (fun () -> observer.OnCompleted())
                    )
                remover := w.Subscribe(newObserver)
                new ActionDisposable(fun () -> (!remover).Dispose()) :> System.IDisposable

    type private TakeWhileObservable<'T>(predicate:unit->bool, w: IObservable<'T>) =
        interface IObservable<'T> with
            member x.Subscribe(observer) =
                let remover: System.IDisposable ref = ref null
                let newObserver =
                    ActionObserver<_>(
                        onNext = (fun v -> if predicate() then observer.OnNext(v) else (!remover).Dispose()),
                        onError = (fun e -> observer.OnError(e)),
                        onCompleted = (fun () -> observer.OnCompleted())
                    )
                remover := w.Subscribe(newObserver)
                new ActionDisposable(fun () -> (!remover).Dispose()) :> System.IDisposable

    type private SkipObservable<'T>(count: int, w: IObservable<'T>) =
        interface IObservable<'T> with
            member x.Subscribe(observer) =
                let state = ref 0
                let newObserver =
                    ActionObserver<_>(
                        onNext = (fun v -> if !state < count then state := !state + 1 else observer.OnNext(v)),
                        onError = (fun e -> observer.OnError(e)),
                        onCompleted = (fun () -> observer.OnCompleted())
                    )
                w.Subscribe(newObserver)

    type private SkipWhileObservable<'T>(predicate:unit->bool, w: IObservable<'T>) =
        interface IObservable<'T> with
            member x.Subscribe(observer) =
                let started = ref false
                let newObserver =
                    ActionObserver<_>(
                        onNext = (fun v -> if !started || not (predicate()) then started := true; observer.OnNext(v)),
                        onError = (fun e -> observer.OnError(e)),
                        onCompleted = (fun () -> observer.OnCompleted())
                    )
                w.Subscribe(newObserver)

    [<CompiledName("Take")>]
    let take count (w: IObservable<'T>) =
        TakeObservable(count, w) :> IObservable<'T>

    [<CompiledName("TakeWhile")>]
    let takeWhile predicate (w: IObservable<'T>) =
        TakeWhileObservable(predicate, w) :> IObservable<'T>

    [<CompiledName("Skip")>]
    let skip count (w: IObservable<'T>) =
        TakeObservable(count, w) :> IObservable<'T>

    [<CompiledName("SkipWhile")>]
    let skipWhile predicate (w: IObservable<'T>) =
        TakeWhileObservable(predicate, w) :> IObservable<'T>

