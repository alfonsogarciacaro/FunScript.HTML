namespace FunScript.HTML

open System
open FunScript.TypeScript

[<AutoOpen>]
[<ReflectedDefinition>]
module RactiveExtensions =
    [<FunScript.JSEmit("for (var k in {0}) { {0}[k] = null } return {0}")>]
    let private reset(o: obj): obj = failwith "never"

    type RactiveState<'T> private (ractive: Ractive, ?keypath: string)  =
        let getData =
            match keypath with
            | Some k -> fun () -> unbox<'T>(ractive.get(k))
            | None   -> fun () -> unbox<'T>(ractive.get())

        new (old: RactiveState<'T>, data: 'T) =
            RactiveState.set(old.ractive, data, ?keypath=old.keypath)
            RactiveState<'T>(old.ractive, ?keypath = old.keypath)

        static member private set(ractive: Ractive, data: 'T, ?keypath: string) =
            match keypath with
            | Some k -> ractive.set(k, data)
            | None   -> ractive.set(data)

        static member private setAsync(ractive: Ractive, data: 'T, ?keypath: string) =
            Async.FromContinuations <| fun (cont, econt, ccont) ->
                let promise =
                    match keypath with
                    | Some k -> ractive.setCont(k, data)
                    | None   -> ractive.setCont(data)
                promise._then(fun _ -> box(cont())) |> ignore

        member st.ractive with get() = ractive
        member st.keypath with get() = keypath
        member st.data    with get() = getData()

        member st.scope(keypath: string) =
            RactiveState(st.ractive, keypath=keypath)

        static member init(ractive: Ractive, data: 'T) =
            RactiveState.set(ractive, data)
            RactiveState<'T>(ractive)

        /// If hasOutTransitions is set to true, the state will be reset first
        /// so be sure to scope it properly.
        static member mkAsync(old: RactiveState<'T>, data: 'T, ?hasOutTransitions: bool) = async {
            let hasOutTransitions = defaultArg hasOutTransitions false
            if hasOutTransitions
            then do! RactiveState.setAsync(old.ractive, reset old.data, ?keypath=old.keypath)
            
            do! RactiveState.setAsync(old.ractive, data, ?keypath=old.keypath)
            return RactiveState<'T>(old.ractive, ?keypath = old.keypath)
        }

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
        member x.CreateFast(elementSelector: string, templateSelector: string) =
            let options = createEmpty<RactiveNewOptions>()
            options.el <- elementSelector
            options.template <- templateSelector
            Globals.Ractive.Create(options)            

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
