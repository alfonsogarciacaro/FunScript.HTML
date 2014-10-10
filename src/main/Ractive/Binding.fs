namespace FunScript.TypeScript

type RactiveParsedTemplate = interface end
type RactiveStatic = interface end
type RactiveNewOptions = interface end
type RactiveAdaptor = interface end
type RactiveAnimateOptions = interface end
type RactiveEvent = interface end
type RactiveFindOptions = interface end
type RactiveMergeOptions = interface end
type RactiveObserve = interface end
type RactiveObserveOptions = interface end
type RactiveParseOptions = interface end
type RactivePromise = interface end
type RactivePromiseStatic = interface end
type RactiveSanitizeOptions = interface end
type RactiveTeardown = interface end
type RactiveTransition = interface end
type RactiveTransitionAnimateOptions = interface end

type Ractive =
    inherit RactiveNewOptions

type RactiveExtendOptions =
    inherit RactiveNewOptions

type RactiveAnimationPromise = 
    inherit RactivePromise

namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_ractive =
    open FunScript.TypeScript
    open System.Collections.Generic

    type Globals with
        [<FunScript.JSEmitInline("(window.Ractive)"); CompiledName("Ractive")>]
        static member Ractive with get() : RactiveStatic = failwith "never" and set (v : RactiveStatic) : unit = failwith "never"

    type Ractive with
        [<FunScript.JSEmitInline("({0}.find({1}))"); CompiledName("find")>]
        member __.find(selector : string) : HTMLElement = failwith "never"
        [<FunScript.JSEmitInline("({0}.findComponent({?1}))"); CompiledName("findComponent")>]
        member __.findComponent(?name : string) : Ractive = failwith "never"
        [<FunScript.JSEmitInline("({0}.findAll({1}, {?2}))"); CompiledName("findAll")>]
        member __.findAll(selector : string, ?options : RactiveFindOptions) : array<HTMLElement> = failwith "never"
        [<FunScript.JSEmitInline("({0}.findAllComponents({1}, {?2}))"); CompiledName("findAllComponents")>]
        member __.findAllComponents(name : string, ?options : RactiveFindOptions) : array<Ractive> = failwith "never"
        [<FunScript.JSEmitInline("({0}.observe({1}, {2}, {?3}))"); CompiledName("observe1")>]
        member __.observe(keypath : string, callback : System.Func<obj, obj, string, unit>, ?options : RactiveObserveOptions) : RactiveObserve = failwith "never"
        [<FunScript.JSEmitInline("({0}.on({1}, {2}))"); CompiledName("on")>]
        member __.on(eventName : string, handler : System.Func<RactiveEvent, obj, unit>) : RactiveObserve = failwith "never"
        [<FunScript.JSEmitInline("({0}.off({?1}, {?2}))"); CompiledName("off")>]
        member __.off(?eventName : string, ?handler : System.Func<RactiveEvent, obj, unit>) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.fire({1}, {2}))"); CompiledName("fire")>]
        member __.fire(eventName : string, arg : obj) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.get({1}))"); CompiledName("get13")>]
        member __.get(keypath : string) : obj = failwith "never"
        [<FunScript.JSEmitInline("({0}.get())"); CompiledName("get14")>]
        member __.get() : obj = failwith "never"

        // NOTE: It's a bit ignoring to append "|> ignore" most of the times, so I'm creating two blocks of mutator methods.
        // The first block doesn't return anything while the second "*Cont()" returns a promise.
        [<FunScript.JSEmitInline("({0}.set({1}, {2}))"); CompiledName("set27")>]
        member __.set(keypath : string, value : obj) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.set({1}))"); CompiledName("set28")>]
        member __.set(map : obj) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.reset({?1}))"); CompiledName("reset1")>]
        member __.reset(?data : obj) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.toggle({1}))"); CompiledName("toggle1")>]
        member __.toggle(keypath : string) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.add({1}, {?2}))"); CompiledName("add7")>]
        member __.add(keypath : string, ?number : float) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.update({?1}))"); CompiledName("update3")>]
        member __.update(?keypath : string) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.updateModel({?1}, {?2}))"); CompiledName("updateModel")>]
        member __.updateModel(?keypath : string, ?cascade : bool) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.animate({1}, {2}, {?3}))"); CompiledName("animate1")>]
        member __.animate(keypath : string, value : obj, ?options : RactiveAnimateOptions) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.animate({1}, {?2}))"); CompiledName("animate2")>]
        member __.animate(map : obj, ?options : RactiveAnimateOptions) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.pop({1}))"); CompiledName("pop2")>]
        member __.pop(keypath : string) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.push({1}, {2}))"); CompiledName("push4")>]
        member __.push(keypath : string, value : obj) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.shift({1}))"); CompiledName("shift2")>]
        member __.shift(keypath : string) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.unshift({1}, {2}))"); CompiledName("unshift4")>]
        member __.unshift(keypath : string, value : obj) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.splice({1}, {2}, {3}))"); CompiledName("splice6")>]
        member __.splice(keypath : string, index : float, removeCount : float) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.splice({1}, {2}, {3}, {4...}))"); CompiledName("splice7")>]
        member __.spliceOverload2(keypath : string, index : float, removeCount : float, [<System.ParamArray>] args : array<obj>) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.subtract({1}, {?2}))"); CompiledName("subtract")>]
        member __.subtract(keypath : string, ?number : float) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.merge({1}, {2}, {?3}))"); CompiledName("merge")>]
        member __.merge(keypath : string, value : array<obj>, ?options : RactiveMergeOptions) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.teardown())"); CompiledName("teardown")>]
        member __.teardown() : unit = failwith "never"

        [<FunScript.JSEmitInline("({0}.set({1}, {2}))"); CompiledName("set27Cont")>]
        member __.setCont(keypath : string, value : obj) : RactivePromise = failwith "never"
        [<FunScript.JSEmitInline("({0}.set({1}))"); CompiledName("set28Cont")>]
        member __.setCont(map : obj) : RactivePromise = failwith "never"
        [<FunScript.JSEmitInline("({0}.reset({?1}))"); CompiledName("reset1Cont")>]
        member __.resetCont(?data : obj) : RactivePromise = failwith "never"
        [<FunScript.JSEmitInline("({0}.toggle({1}))"); CompiledName("toggle1Cont")>]
        member __.toggleCont(keypath : string) : RactivePromise = failwith "never"
        [<FunScript.JSEmitInline("({0}.add({1}, {?2}))"); CompiledName("add7Cont")>]
        member __.addCont(keypath : string, ?number : float) : RactivePromise = failwith "never"
        [<FunScript.JSEmitInline("({0}.update({?1}))"); CompiledName("update3Cont")>]
        member __.updateCont(?keypath : string) : RactivePromise = failwith "never"
        [<FunScript.JSEmitInline("({0}.updateModel({?1}, {?2}))"); CompiledName("updateModelCont")>]
        member __.updateModelCont(?keypath : string, ?cascade : bool) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.animate({1}, {2}, {?3}))"); CompiledName("animate1Cont")>]
        member __.animateCont(keypath : string, value : obj, ?options : RactiveAnimateOptions) : RactiveAnimationPromise = failwith "never"
        [<FunScript.JSEmitInline("({0}.animate({1}, {?2}))"); CompiledName("animate2Cont")>]
        member __.animateCont(map : obj, ?options : RactiveAnimateOptions) : RactiveAnimationPromise = failwith "never"
        [<FunScript.JSEmitInline("({0}.pop({1}))"); CompiledName("pop2Cont")>]
        member __.popCont(keypath : string) : RactivePromise = failwith "never"
        [<FunScript.JSEmitInline("({0}.push({1}, {2}))"); CompiledName("push4Cont")>]
        member __.pushCont(keypath : string, value : obj) : RactivePromise = failwith "never"
        [<FunScript.JSEmitInline("({0}.shift({1}))"); CompiledName("shift2Cont")>]
        member __.shiftCont(keypath : string) : RactivePromise = failwith "never"
        [<FunScript.JSEmitInline("({0}.unshift({1}, {2}))"); CompiledName("unshift4Cont")>]
        member __.unshiftCont(keypath : string, value : obj) : RactivePromise = failwith "never"
        [<FunScript.JSEmitInline("({0}.splice({1}, {2}, {3}))"); CompiledName("splice6Cont")>]
        member __.spliceCont(keypath : string, index : float, removeCount : float) : RactivePromise = failwith "never"
        [<FunScript.JSEmitInline("({0}.splice({1}, {2}, {3}, {4...}))"); CompiledName("splice7Cont")>]
        member __.spliceOverload2Cont(keypath : string, index : float, removeCount : float, [<System.ParamArray>] args : array<obj>) : RactivePromise = failwith "never"
        [<FunScript.JSEmitInline("({0}.subtract({1}, {?2}))"); CompiledName("subtractCont")>]
        member __.subtractCont(keypath : string, ?number : float) : RactivePromise = failwith "never"
        [<FunScript.JSEmitInline("({0}.merge({1}, {2}, {?3}))"); CompiledName("mergeCont")>]
        member __.mergeCont(keypath : string, value : array<obj>, ?options : RactiveMergeOptions) : RactivePromise = failwith "never"
        [<FunScript.JSEmitInline("({0}.teardown())"); CompiledName("teardownCont")>]
        member __.teardownCont() : RactivePromise = failwith "never"    
    
        [<FunScript.JSEmitInline("({0}.insert({1}, {?2}))"); CompiledName("insert")>]
        member __.insert(target : obj, ?anchor : obj) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.detach())"); CompiledName("detach2")>]
        member __.detach() : DocumentFragment = failwith "never"
        [<FunScript.JSEmitInline("({0}.toHTML())"); CompiledName("toHTML")>]
        member __.toHTML() : string = failwith "never"
        [<FunScript.JSEmitInline("({0}.nodes)"); CompiledName("nodes")>]
        member __.nodes with get() : Dictionary<string, HTMLElement> = failwith "never" and set (v : Dictionary<string, HTMLElement>) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.partials)"); CompiledName("partials")>]
        member __.partials with get() : Dictionary<string, string> = failwith "never" and set (v : Dictionary<string, string>) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.transitions)"); CompiledName("transitions")>]
        member __.transitions with get() : Dictionary<string, System.Func<RactiveTransition, obj, unit>> = failwith "never" and set (v : Dictionary<string, System.Func<RactiveTransition, obj, unit>>) : unit = failwith "never"

    type RactiveAdaptor with
        [<FunScript.JSEmitInline("({0}.filter)"); CompiledName("filter6")>]
        member __.filter with get() : System.Func<obj, string, Ractive, bool> = failwith "never" and set (v : System.Func<obj, string, Ractive, bool>) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.wrap)"); CompiledName("wrap1")>]
        member __.wrap with get() : System.Func<Ractive, obj, string, System.Func<obj, obj>, unit> = failwith "never" and set (v : System.Func<Ractive, obj, string, System.Func<obj, obj>, unit>) : unit = failwith "never"

    type RactiveAnimateOptions with
        [<FunScript.JSEmitInline("({0}.duration)"); CompiledName("duration3")>]
        member __.duration with get() : float = failwith "never" and set (v : float) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.easing)"); CompiledName("easing")>]
        member __.easing with get() : obj = failwith "never" and set (v : obj) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.step)"); CompiledName("step1")>]
        member __.step with get() : System.Func<float, float, unit> = failwith "never" and set (v : System.Func<float, float, unit>) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.complate)"); CompiledName("complate")>]
        member __.complate with get() : System.Func<float, float, unit> = failwith "never" and set (v : System.Func<float, float, unit>) : unit = failwith "never"

    type RactiveAnimationPromise with
        [<FunScript.JSEmitInline("({0}.stop())"); CompiledName("stop3")>]
        member __.stop() : unit = failwith "never"

    type RactiveEvent with
        [<FunScript.JSEmitInline("({0}.context)"); CompiledName("context")>]
        member __.context with get() : obj = failwith "never" and set (v : obj) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.index)"); CompiledName("index4")>]
        member __.index with get() : Dictionary<string, int> = failwith "never" and set (v : Dictionary<string, int>) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.keypath)"); CompiledName("keypath")>]
        member __.keypath with get() : string = failwith "never" and set (v : string) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.node)"); CompiledName("node")>]
        member __.node with get() : HTMLElement = failwith "never" and set (v : HTMLElement) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.original)"); CompiledName("original")>]
        member __.original with get() : Event = failwith "never" and set (v : Event) : unit = failwith "never"

    type RactiveExtendOptions with
        [<FunScript.JSEmitInline("({0}.beforeInit)"); CompiledName("beforeInit")>]
        member __.beforeInit with get() : System.Func<RactiveExtendOptions, unit> = failwith "never" and set (v : System.Func<RactiveExtendOptions, unit>) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.init)"); CompiledName("init")>]
        member __.init with get() : System.Func<RactiveExtendOptions, unit> = failwith "never" and set (v : System.Func<RactiveExtendOptions, unit>) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.isolated)"); CompiledName("isolated")>]
        member __.isolated with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"

    type RactiveFindOptions with
        [<FunScript.JSEmitInline("({0}.live)"); CompiledName("live")>]
        member __.live with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"

    type RactiveMergeOptions with
        [<FunScript.JSEmitInline("({0}.compare)"); CompiledName("compare1")>]
        member __.compare with get() : obj = failwith "never" and set (v : obj) : unit = failwith "never"

    type RactiveNewOptions with
        [<FunScript.JSEmitInline("({0}.adapt)"); CompiledName("adapt")>]
        member __.adapt with get() : array<RactiveAdaptor> = failwith "never" and set (v : array<RactiveAdaptor>) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.adaptors)"); CompiledName("adaptors")>]
        member __.adaptors with get() : Dictionary<string, RactiveAdaptor> = failwith "never" and set (v : Dictionary<string, RactiveAdaptor>) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.complete)"); CompiledName("complete2")>]
        member __.complete with get() : System.Func<unit> = failwith "never" and set (v : System.Func<unit>) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.components)"); CompiledName("components")>]
        member __.components with get() : Dictionary<string, RactiveStatic> = failwith "never" and set (v : Dictionary<string, RactiveStatic>) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.computed)"); CompiledName("computed")>]
        member __.computed with get() : Dictionary<string, string> = failwith "never" and set (v : Dictionary<string, string>) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.css)"); CompiledName("css")>]
        member __.css with get() : string = failwith "never" and set (v : string) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.data)"); CompiledName("data9")>]
        member __.data with get() : obj = failwith "never" and set (v : obj) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.decorators)"); CompiledName("decorators")>]
        member __.decorators with get() : Dictionary<string, System.Func<HTMLElement, obj, RactiveTeardown>> = failwith "never" and set (v : Dictionary<string, System.Func<HTMLElement, obj, RactiveTeardown>>) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.el)"); CompiledName("el")>]
        member __.el with get() : string = failwith "never" and set (v : string) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.events)"); CompiledName("events")>]
        member __.events with get() : Dictionary<string, System.Func<HTMLElement, System.Func<RactiveEvent, obj>, RactiveTeardown>> = failwith "never" and set (v : Dictionary<string, System.Func<HTMLElement, System.Func<RactiveEvent, obj>, RactiveTeardown>>) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.partials)"); CompiledName("partials1")>]
        member __.partials with get() : Dictionary<string, string> = failwith "never" and set (v : Dictionary<string, string>) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.sanitize)"); CompiledName("sanitize")>]
        member __.sanitize with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.template)"); CompiledName("template")>]
        member __.template with get() : string = failwith "never" and set (v : string) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.transitions)"); CompiledName("transitions1")>]
        member __.transitions with get() : Dictionary<string, System.Func<RactiveTransition, obj, unit>> = failwith "never" and set (v : Dictionary<string, System.Func<RactiveTransition, obj, unit>>) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.transitionsEnabled)"); CompiledName("transitionsEnabled")>]
        member __.transitionsEnabled with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.delimiters)"); CompiledName("delimiters")>]
        member __.delimiters with get() : array<string> = failwith "never" and set (v : array<string>) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.tripleDelimiters)"); CompiledName("tripleDelimiters")>]
        member __.tripleDelimiters with get() : array<string> = failwith "never" and set (v : array<string>) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.staticDelimiters)"); CompiledName("staticDelimiters")>]
        member __.staticDelimiters with get() : array<string> = failwith "never" and set (v : array<string>) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.staticTripleDelimiters)"); CompiledName("staticTripleDelimiters")>]
        member __.staticTripleDelimiters with get() : array<string> = failwith "never" and set (v : array<string>) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.append)"); CompiledName("append2")>]
        member __.append with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.debug)"); CompiledName("debug2")>]
        member __.debug with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.lazy)"); CompiledName("_lazy")>]
        member __._lazy with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.magic)"); CompiledName("magic")>]
        member __.magic with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.modifyArrays)"); CompiledName("modifyArrays")>]
        member __.modifyArrays with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.noCssTransform)"); CompiledName("noCssTransform")>]
        member __.noCssTransform with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.noIntro)"); CompiledName("noIntro")>]
        member __.noIntro with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.preserveWhitespace)"); CompiledName("preserveWhitespace")>]
        member __.preserveWhitespace with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.stripComments)"); CompiledName("stripComments")>]
        member __.stripComments with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.twoway)"); CompiledName("twoway")>]
        member __.twoway with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"

    type RactiveObserve with
        [<FunScript.JSEmitInline("({0}.cancel())"); CompiledName("cancel")>]
        member __.cancel() : unit = failwith "never"

    type RactiveObserveOptions with
        [<FunScript.JSEmitInline("({0}.context)"); CompiledName("context1")>]
        member __.context with get() : obj = failwith "never" and set (v : obj) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.debug)"); CompiledName("debug3")>]
        member __.debug with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.defer)"); CompiledName("defer1")>]
        member __.defer with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.init)"); CompiledName("init1")>]
        member __.init with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"

    type RactiveParseOptions with
        [<FunScript.JSEmitInline("({0}.preserveWhitespace)"); CompiledName("preserveWhitespace1")>]
        member __.preserveWhitespace with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.sanitize)"); CompiledName("sanitize1")>]
        member __.sanitize with get() : obj = failwith "never" and set (v : obj) : unit = failwith "never"

    type RactivePromise with
        [<FunScript.JSEmitInline("({0}.then({1}, {?2}))"); CompiledName("_then")>]
        member __._then(onFullfilled : System.Func<obj, obj>, ?onRejected : System.Func<string, obj>) : RactivePromise = failwith "never"
        [<FunScript.JSEmitInline("({0}.catch({1}))"); CompiledName("_catch")>]
        member __._catch(onRejected : System.Func<string, obj>) : RactivePromise = failwith "never"

    type RactivePromiseStatic with
        [<FunScript.JSEmitInline("({0}.all({1}))"); CompiledName("all2")>]
        member __.all(iterable : array<RactivePromise>) : RactivePromise = failwith "never"
        [<FunScript.JSEmitInline("({0}.resolve({1}))"); CompiledName("resolve")>]
        member __.resolve(value : obj) : RactivePromise = failwith "never"
        [<FunScript.JSEmitInline("({0}.reject({1}))"); CompiledName("reject")>]
        member __.reject(reason : string) : RactivePromise = failwith "never"

    type RactiveSanitizeOptions with
        [<FunScript.JSEmitInline("({0}.elements)"); CompiledName("elements1")>]
        member __.elements with get() : array<string> = failwith "never" and set (v : array<string>) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.eventAttributes)"); CompiledName("eventAttributes")>]
        member __.eventAttributes with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"

    type RactiveStatic with
        [<FunScript.JSEmitInline("(new {0}({1}))"); CompiledName("Create463")>]
        member __.Create(options : RactiveNewOptions) : Ractive = failwith "never"
        [<FunScript.JSEmitInline("({0}.extend({1}))"); CompiledName("extend")>]
        member __.extend(options : RactiveExtendOptions) : RactiveStatic = failwith "never"
        [<FunScript.JSEmitInline("({0}.parse({1}, {?2}))"); CompiledName("parse2")>]
        member __.parse(template : string, ?options : RactiveParseOptions) : RactiveParsedTemplate = failwith "never"
        [<FunScript.JSEmitInline("({0}.adaptors)"); CompiledName("adaptors1")>]
        member __.adaptors with get() : Dictionary<string, RactiveAdaptor> = failwith "never" and set (v : Dictionary<string, RactiveAdaptor>) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.components)"); CompiledName("components1")>]
        member __.components with get() : Dictionary<string, RactiveStatic> = failwith "never" and set (v : Dictionary<string, RactiveStatic>) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.defaults)"); CompiledName("defaults")>]
        member __.defaults with get() : RactiveNewOptions = failwith "never" and set (v : RactiveNewOptions) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.decorators)"); CompiledName("decorators1")>]
        member __.decorators with get() : Dictionary<string, System.Func<HTMLElement, obj, RactiveTeardown>> = failwith "never" and set (v : Dictionary<string, System.Func<HTMLElement, obj, RactiveTeardown>>) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.easing)"); CompiledName("easing1")>]
        member __.easing with get() : Dictionary<string, System.Func<float, float>> = failwith "never" and set (v : Dictionary<string, System.Func<float, float>>) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.events)"); CompiledName("events1")>]
        member __.events with get() : Dictionary<string, System.Func<HTMLElement, System.Func<RactiveEvent, obj>, RactiveTeardown>> = failwith "never" and set (v : Dictionary<string, System.Func<HTMLElement, System.Func<RactiveEvent, obj>, RactiveTeardown>>) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.partials)"); CompiledName("partials2")>]
        member __.partials with get() : Dictionary<string, RactiveParsedTemplate> = failwith "never" and set (v : Dictionary<string, RactiveParsedTemplate>) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.Promise)"); CompiledName("Promise")>]
        member __.Promise with get() : RactivePromiseStatic = failwith "never" and set (v : RactivePromiseStatic) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.transitions)"); CompiledName("transitions2")>]
        member __.transitions with get() : Dictionary<string, System.Func<RactiveTransition, obj, unit>> = failwith "never" and set (v : Dictionary<string, System.Func<RactiveTransition, obj, unit>>) : unit = failwith "never"

    type RactiveTeardown with
        [<FunScript.JSEmitInline("({0}.teardown)"); CompiledName("teardown1")>]
        member __.teardown with get() : System.Func<unit> = failwith "never" and set (v : System.Func<unit>) : unit = failwith "never"

    type RactiveTransition with
        [<FunScript.JSEmitInline("({0}.isIntro)"); CompiledName("isIntro")>]
        member __.isIntro with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.name)"); CompiledName("name35")>]
        member __.name with get() : string = failwith "never" and set (v : string) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.node)"); CompiledName("node1")>]
        member __.node with get() : HTMLElement = failwith "never" and set (v : HTMLElement) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.animateStyle({1}, {2}, {3}, {4}))"); CompiledName("animateStyle")>]
        member __.animateStyle(prop : string, value : obj, options : RactiveTransitionAnimateOptions, complete : System.Func<unit>) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.animateStyle({1}, {2}, {3}))"); CompiledName("animateStyle1")>]
        member __.animateStyle(props : obj, options : RactiveTransitionAnimateOptions, complete : System.Func<unit>) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.complete({?1}))"); CompiledName("complete3")>]
        member __.complete(?noReset : bool) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.getStyle({1}))"); CompiledName("getStyle")>]
        member __.getStyle(prop : string) : string = failwith "never"
        [<FunScript.JSEmitInline("({0}.getStyle({1}))"); CompiledName("getStyle1")>]
        member __.getStyle(props : array<string>) : obj = failwith "never"
        [<FunScript.JSEmitInline("({0}.processParams({1}, {?2}))"); CompiledName("processParams")>]
        member __.processParams(_params : obj, ?defaults : obj) : obj = failwith "never"
        [<FunScript.JSEmitInline("({0}.resetStyle())"); CompiledName("resetStyle")>]
        member __.resetStyle() : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.setStyle({1}, {2}))"); CompiledName("setStyle")>]
        member __.setStyle(prop : string, value : obj) : RactiveTransition = failwith "never"
        [<FunScript.JSEmitInline("({0}.setStyle({1}))"); CompiledName("setStyle1")>]
        member __.setStyle(props : obj) : RactiveTransition = failwith "never"

    type RactiveTransitionAnimateOptions with
        [<FunScript.JSEmitInline("({0}.duration)"); CompiledName("duration4")>]
        member __.duration with get() : float = failwith "never" and set (v : float) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.easing)"); CompiledName("easing2")>]
        member __.easing with get() : string = failwith "never" and set (v : string) : unit = failwith "never"
        [<FunScript.JSEmitInline("({0}.delay)"); CompiledName("delay")>]
        member __.delay with get() : float = failwith "never" and set (v : float) : unit = failwith "never"
