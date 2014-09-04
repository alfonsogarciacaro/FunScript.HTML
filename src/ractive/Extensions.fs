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


type Async with
    static member AwaitRactiveEvent(r: Ractive, ev: string) =
        Async.FromContinuations(fun (cont, econt, ccont) ->
            let observe1 = ref Unchecked.defaultof<RactiveObserve> // TODO: Check how this is translated to JavaScript
            observe1 := r.on(ev, fun ev args -> (!observe1).cancel(); cont(ev, args))
        )

    static member AwaitRactiveEvent2(r: Ractive, ev1: string, ev2: string) =
        Async.FromContinuations(fun (cont, econt, ccont) ->
            let observe1 = ref Unchecked.defaultof<RactiveObserve>
            let observe2 = ref Unchecked.defaultof<RactiveObserve>
            observe1 := r.on(ev1, fun ev args -> (!observe1).cancel(); (!observe2).cancel(); cont(Choice1Of2(ev, args)))
            observe2 := r.on(ev2, fun ev args -> (!observe1).cancel(); (!observe2).cancel(); cont(Choice2Of2(ev, args)))
        )

    static member AwaitRactiveEvent3(r: Ractive, ev1: string, ev2: string, ev3: string) =
        Async.FromContinuations(fun (cont, econt, ccont) ->
            let observe1 = ref Unchecked.defaultof<RactiveObserve>
            let observe2 = ref Unchecked.defaultof<RactiveObserve>
            let observe3 = ref Unchecked.defaultof<RactiveObserve>
            observe1 := r.on(ev1, fun ev args -> (!observe1).cancel(); (!observe2).cancel(); (!observe3).cancel(); cont(Choice1Of3(ev, args)))
            observe2 := r.on(ev2, fun ev args -> (!observe1).cancel(); (!observe2).cancel(); (!observe3).cancel(); cont(Choice2Of3(ev, args)))
            observe3 := r.on(ev3, fun ev args -> (!observe1).cancel(); (!observe2).cancel(); (!observe3).cancel(); cont(Choice3Of3(ev, args)))
        )

    static member AwaitRactiveEvent4(r: Ractive, ev1: string, ev2: string, ev3: string, ev4: string) =
        Async.FromContinuations(fun (cont, econt, ccont) ->
            let observe1 = ref Unchecked.defaultof<RactiveObserve>
            let observe2 = ref Unchecked.defaultof<RactiveObserve>
            let observe3 = ref Unchecked.defaultof<RactiveObserve>
            let observe4 = ref Unchecked.defaultof<RactiveObserve>
            observe1 := r.on(ev1, fun ev args -> (!observe1).cancel(); (!observe2).cancel(); (!observe3).cancel(); (!observe4).cancel(); cont(Choice1Of4(ev, args)))
            observe2 := r.on(ev2, fun ev args -> (!observe1).cancel(); (!observe2).cancel(); (!observe3).cancel(); (!observe4).cancel(); cont(Choice2Of4(ev, args)))
            observe3 := r.on(ev3, fun ev args -> (!observe1).cancel(); (!observe2).cancel(); (!observe3).cancel(); (!observe4).cancel(); cont(Choice3Of4(ev, args)))
            observe4 := r.on(ev4, fun ev args -> (!observe1).cancel(); (!observe2).cancel(); (!observe3).cancel(); (!observe4).cancel(); cont(Choice4Of4(ev, args)))
        )

