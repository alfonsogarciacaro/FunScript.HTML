namespace FunScript.HTML

open System
open FunScript.TypeScript

[<AutoOpen>]
[<ReflectedDefinition>]
module RactiveExtensions =
    type RactiveState<'T> private (ractive: Ractive, ?keypath: string)  =
        let getData =
            match keypath with
            | Some k -> fun () -> unbox<'T>(ractive.get(k))
            | None   -> fun () -> unbox<'T>(ractive.get())

        new (oldState: RactiveState<'T>, data: 'T) =
            RactiveState.set(oldState.ractive, data, ?keypath=oldState.keypath)
            RactiveState<'T>(oldState.ractive, ?keypath = oldState.keypath)

        static member private set(ractive: Ractive, data: 'T, ?keypath: string) =
            match keypath with
            | Some k -> ignore(ractive.set(k, data))
            | None   -> ignore(ractive.set(data))

        member st.ractive with get() = ractive
        member st.keypath with get() = keypath
        member st.data    with get() = getData()

        member st.scope(keypath: string) =
            RactiveState(st.ractive, keypath=keypath)

        static member init(ractive: Ractive, data: 'T) =
            RactiveState.set(ractive, data)
            RactiveState<'T>(ractive)

    type RactiveEventStream(ractive: Ractive, eventName: string) =
        interface IObservable<RactiveEvent*obj> with
            member x.Subscribe observer =
                let remover = ractive.on(eventName, (fun ev arg -> observer.OnNext(ev, arg)))
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

    type RactiveStatic with
        member x.makeCustomKeyEvent keyCode =
            let plugin =
                fun (node: HTMLElement) (fire: System.Func<RactiveEvent,obj>) ->
                    let keydownHandler =
                        EventListenerDelegate( 
                            fun (ev: Event) ->
                                if (ev :?> KeyboardEvent).which = (float keyCode) then
                                    let f = createEmpty<RactiveEvent>() in f.node <- node; f.original <- ev
                                    fire.Invoke(f) |> ignore )

                    node.addEventListener("keydown", keydownHandler, false)
                    let t = createEmpty<RactiveTeardown>()
                    t.teardown <- fun () -> node.removeEventListener("keydown", keydownHandler, false)
                    t
            System.Func<HTMLElement,_,_>(plugin)
