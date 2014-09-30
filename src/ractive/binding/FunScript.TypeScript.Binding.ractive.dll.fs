
namespace FunScript.TypeScript
type Globals = interface end

namespace FunScript.TypeScript
type AnonymousType431 = interface end

namespace FunScript.TypeScript
type AnonymousType432 = interface end

namespace FunScript.TypeScript
type AnonymousType433 = interface end

namespace FunScript.TypeScript
type AnonymousType434 = interface end

namespace FunScript.TypeScript
type AnonymousType435 = interface end

namespace FunScript.TypeScript
type AnonymousType436 = interface end

namespace FunScript.TypeScript
type AnonymousType437 = interface end

namespace FunScript.TypeScript
type AnonymousType438 = interface end

namespace FunScript.TypeScript
type AnonymousType439 = interface end

namespace FunScript.TypeScript
type Ractive = interface end

namespace FunScript.TypeScript
type RactiveAdaptorPlugin =
        inherit FunScript.TypeScript.Object

namespace FunScript.TypeScript
type RactiveAdaptorPlugins = interface end

namespace FunScript.TypeScript
type RactiveAnimateOptions = interface end

namespace FunScript.TypeScript
type RactiveComponentPlugins = interface end

namespace FunScript.TypeScript
type RactiveDecoratorPlugin = interface end

namespace FunScript.TypeScript
type RactiveDecoratorPlugins = interface end

namespace FunScript.TypeScript
type RactiveEvent = interface end

namespace FunScript.TypeScript
type RactiveEventPlugin =
        inherit FunScript.TypeScript.Function

namespace FunScript.TypeScript
type RactiveEventPlugins = interface end

namespace FunScript.TypeScript
type RactiveNewOptions = interface end

namespace FunScript.TypeScript
type RactiveObserve = interface end

namespace FunScript.TypeScript
type RactiveObserveOptions = interface end

namespace FunScript.TypeScript
type RactiveParseOptions = interface end

namespace FunScript.TypeScript
type RactivePromise =
        inherit FunScript.TypeScript.Object

namespace FunScript.TypeScript
type RactiveSanitizeOptions = interface end

namespace FunScript.TypeScript
type RactiveStatic = interface end

namespace FunScript.TypeScript
type RactiveTransition = interface end

namespace FunScript.TypeScript
type RactiveTransitionAnimateOptions = interface end

namespace FunScript.TypeScript
type RactiveTransitionPlugin = interface end

namespace FunScript.TypeScript
type RactiveTransitionPlugins = interface end

namespace FunScript.TypeScript
type RactiveAnimationPromise =
        inherit FunScript.TypeScript.RactivePromise

namespace FunScript.TypeScript
type RactiveComponentPlugin =
        inherit FunScript.TypeScript.RactiveStatic

namespace FunScript.TypeScript
type RactiveExtendOptions =
        inherit FunScript.TypeScript.RactiveNewOptions

namespace FunScript.TypeScript
type RactiveTransitionPluginDelegate = delegate of FunScript.TypeScript.RactiveTransition * FunScript.TypeScript.Object -> unit

namespace FunScript.TypeScript
type RactiveDefaultsOptions =
        inherit FunScript.TypeScript.RactiveExtendOptions


namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_ractive_0 =


    type Globals with 

            [<FunScript.JSEmitInline("(window.Ractive)"); CompiledName("Ractive")>]
            static member Ractive with get() : FunScript.TypeScript.RactiveStatic = failwith "never" and set (v : FunScript.TypeScript.RactiveStatic) : unit = failwith "never"

namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_ractive_1 =


    type FunScript.TypeScript.AnonymousType431 with 

            [<FunScript.JSEmitInline("({0}.update)"); CompiledName("update3")>]
            member __.update with get() : System.Func<array<obj>, unit> = failwith "never" and set (v : System.Func<array<obj>, unit>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.teardown)"); CompiledName("teardown")>]
            member __.teardown with get() : System.Func<unit> = failwith "never" and set (v : System.Func<unit>) : unit = failwith "never"

namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_ractive_2 =


    type FunScript.TypeScript.AnonymousType432 with 

            [<FunScript.JSEmitInline("({0}[{1}])"); CompiledName("Item44")>]
            member __.Item with get(i : string) : obj = failwith "never" and set (i : string) (v : obj) : unit = failwith "never"

namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_ractive_3 =


    type FunScript.TypeScript.AnonymousType433 with 

            [<FunScript.JSEmitInline("({0}[{1}])"); CompiledName("Item45")>]
            member __.Item with get(i : string) : obj = failwith "never" and set (i : string) (v : obj) : unit = failwith "never"

namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_ractive_4 =


    type FunScript.TypeScript.AnonymousType434 with 

            [<FunScript.JSEmitInline("({0}[{1}])"); CompiledName("Item46")>]
            member __.Item with get(i : string) : System.Func<float, float> = failwith "never" and set (i : string) (v : System.Func<float, float>) : unit = failwith "never"

namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_ractive_5 =


    type FunScript.TypeScript.AnonymousType435 with 

            [<FunScript.JSEmitInline("({0}[{1}])"); CompiledName("Item47")>]
            member __.Item with get(i : string) : obj = failwith "never" and set (i : string) (v : obj) : unit = failwith "never"

namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_ractive_6 =


    type FunScript.TypeScript.AnonymousType436 with 

            [<FunScript.JSEmitInline("({0}.live)"); CompiledName("live")>]
            member __.live with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"

namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_ractive_7 =


    type FunScript.TypeScript.AnonymousType437 with 

            [<FunScript.JSEmitInline("({0}.live)"); CompiledName("live1")>]
            member __.live with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"

namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_ractive_8 =


    type FunScript.TypeScript.AnonymousType438 with 

            [<FunScript.JSEmitInline("({0}.compare)"); CompiledName("compare1")>]
            member __.compare with get() : obj = failwith "never" and set (v : obj) : unit = failwith "never"

namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_ractive_9 =


    type FunScript.TypeScript.AnonymousType439 with 

            [<FunScript.JSEmitInline("({0}[{1}])"); CompiledName("Item48")>]
            member __.Item with get(i : string) : System.Func<FunScript.TypeScript.RactiveEvent, array<obj>, unit> = failwith "never" and set (i : string) (v : System.Func<FunScript.TypeScript.RactiveEvent, array<obj>, unit>) : unit = failwith "never"

namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_ractive_10 =


    type FunScript.TypeScript.Ractive with 

            [<FunScript.JSEmitInline("({0}.add({1}, {?2}))"); CompiledName("add7")>]
            member __.add(keypath : string, ?number : float) : FunScript.TypeScript.RactivePromise = failwith "never"
            [<FunScript.JSEmitInline("({0}.add = {1})"); CompiledName("add7Aux")>]
            member __.``add <-``(func : System.Func<string, float, FunScript.TypeScript.RactivePromise>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.animate({1}, {2}, {?3}))"); CompiledName("animate1")>]
            member __.animate(keypath : string, value : obj, ?options : FunScript.TypeScript.RactiveAnimateOptions) : FunScript.TypeScript.RactiveAnimationPromise = failwith "never"
            [<FunScript.JSEmitInline("({0}.animate = {1})"); CompiledName("animate1Aux")>]
            member __.``animate <-``(func : System.Func<string, obj, FunScript.TypeScript.RactiveAnimateOptions, FunScript.TypeScript.RactiveAnimationPromise>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.animate({1}, {?2}))"); CompiledName("animate2")>]
            member __.animate(map : FunScript.TypeScript.Object, ?options : FunScript.TypeScript.RactiveAnimateOptions) : FunScript.TypeScript.RactiveAnimationPromise = failwith "never"
            [<FunScript.JSEmitInline("({0}.animate = {1})"); CompiledName("animate2Aux")>]
            member __.``animate <-``(func : System.Func<FunScript.TypeScript.Object, FunScript.TypeScript.RactiveAnimateOptions, FunScript.TypeScript.RactiveAnimationPromise>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.detach())"); CompiledName("detach2")>]
            member __.detach() : FunScript.TypeScript.DocumentFragment = failwith "never"
            [<FunScript.JSEmitInline("({0}.detach = {1})"); CompiledName("detach2Aux")>]
            member __.``detach <-``(func : System.Func<FunScript.TypeScript.DocumentFragment>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.find({1}))"); CompiledName("find")>]
            member __.find(selector : string) : FunScript.TypeScript.HTMLElement = failwith "never"
            [<FunScript.JSEmitInline("({0}.find = {1})"); CompiledName("findAux")>]
            member __.``find <-``(func : System.Func<string, FunScript.TypeScript.HTMLElement>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.findAll({1}, {?2}))"); CompiledName("findAll")>]
            member __.findAll(selector : string, ?options : FunScript.TypeScript.AnonymousType436) : array<FunScript.TypeScript.HTMLElement> = failwith "never"
            [<FunScript.JSEmitInline("({0}.findAll = {1})"); CompiledName("findAllAux")>]
            member __.``findAll <-``(func : System.Func<string, FunScript.TypeScript.AnonymousType436, array<FunScript.TypeScript.HTMLElement>>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.findAllComponents({1}, {?2}))"); CompiledName("findAllComponents")>]
            member __.findAllComponents(name : string, ?options : FunScript.TypeScript.AnonymousType437) : array<FunScript.TypeScript.Ractive> = failwith "never"
            [<FunScript.JSEmitInline("({0}.findAllComponents = {1})"); CompiledName("findAllComponentsAux")>]
            member __.``findAllComponents <-``(func : System.Func<string, FunScript.TypeScript.AnonymousType437, array<FunScript.TypeScript.Ractive>>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.findComponent({?1}))"); CompiledName("findComponent")>]
            member __.findComponent(?name : string) : FunScript.TypeScript.Ractive = failwith "never"
            [<FunScript.JSEmitInline("({0}.findComponent = {1})"); CompiledName("findComponentAux")>]
            member __.``findComponent <-``(func : System.Func<string, FunScript.TypeScript.Ractive>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.fire({1}))"); CompiledName("fire")>]
            member __.fire(eventName : string) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.fire = {1})"); CompiledName("fireAux")>]
            member __.``fire <-``(func : System.Func<string, unit>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.fire({1}, {2...}))"); CompiledName("fire1")>]
            member __.fireOverload2(eventName : string, [<System.ParamArray>] args : array<obj>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.get({1}))"); CompiledName("get13")>]
            member __.get(keypath : string) : obj = failwith "never"
            [<FunScript.JSEmitInline("({0}.get = {1})"); CompiledName("get13Aux")>]
            member __.``get <-``(func : System.Func<string, obj>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.get())"); CompiledName("get14")>]
            member __.get() : FunScript.TypeScript.Object = failwith "never"
            [<FunScript.JSEmitInline("({0}.get = {1})"); CompiledName("get14Aux")>]
            member __.``get <-``(func : System.Func<FunScript.TypeScript.Object>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.insert({1}, {?2}))"); CompiledName("insert")>]
            member __.insert(target : obj, ?anchor : obj) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.insert = {1})"); CompiledName("insertAux")>]
            member __.``insert <-``(func : System.Func<obj, obj, unit>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.merge({1}, {2}, {?3}))"); CompiledName("merge")>]
            member __.merge(keypath : string, value : array<obj>, ?options : FunScript.TypeScript.AnonymousType438) : FunScript.TypeScript.RactivePromise = failwith "never"
            [<FunScript.JSEmitInline("({0}.merge = {1})"); CompiledName("mergeAux")>]
            member __.``merge <-``(func : System.Func<string, array<obj>, FunScript.TypeScript.AnonymousType438, FunScript.TypeScript.RactivePromise>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.observe({1}, {2}, {?3}))"); CompiledName("observe1")>]
            member __.observe(keypath : string, callback : System.Func<obj, obj, string, unit>, ?options : FunScript.TypeScript.RactiveObserveOptions) : FunScript.TypeScript.RactiveObserve = failwith "never"
            [<FunScript.JSEmitInline("({0}.observe = {1})"); CompiledName("observe1Aux")>]
            member __.``observe <-``(func : System.Func<string, System.Func<obj, obj, string, unit>, FunScript.TypeScript.RactiveObserveOptions, FunScript.TypeScript.RactiveObserve>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.observe({1}, {?2}))"); CompiledName("observe2")>]
            member __.observe(map : FunScript.TypeScript.Object, ?options : FunScript.TypeScript.RactiveObserveOptions) : FunScript.TypeScript.RactiveObserve = failwith "never"
            [<FunScript.JSEmitInline("({0}.observe = {1})"); CompiledName("observe2Aux")>]
            member __.``observe <-``(func : System.Func<FunScript.TypeScript.Object, FunScript.TypeScript.RactiveObserveOptions, FunScript.TypeScript.RactiveObserve>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.off({?1}, {?2}))"); CompiledName("off")>]
            member __.off(?eventName : string, ?handler : System.Func<unit>) : FunScript.TypeScript.Ractive = failwith "never"
            [<FunScript.JSEmitInline("({0}.off = {1})"); CompiledName("offAux")>]
            member __.``off <-``(func : System.Func<string, System.Func<unit>, FunScript.TypeScript.Ractive>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.on({1}, {2}))"); CompiledName("on")>]
            member __.on(eventName : string, handler : System.Func<FunScript.TypeScript.RactiveEvent, obj, unit>) : FunScript.TypeScript.RactiveObserve = failwith "never"
            [<FunScript.JSEmitInline("({0}.on = {1})"); CompiledName("onAux")>]
            member __.``on <-``(func : System.Func<string, System.Func<FunScript.TypeScript.RactiveEvent, obj, unit>, FunScript.TypeScript.RactiveObserve>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.on({1}))"); CompiledName("on1")>]
            member __.on(map : FunScript.TypeScript.AnonymousType439) : FunScript.TypeScript.RactiveObserve = failwith "never"
            [<FunScript.JSEmitInline("({0}.on = {1})"); CompiledName("on1Aux")>]
            member __.``on <-``(func : System.Func<FunScript.TypeScript.AnonymousType439, FunScript.TypeScript.RactiveObserve>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.pop({1}))"); CompiledName("pop2")>]
            member __.pop(keypath : string) : FunScript.TypeScript.RactivePromise = failwith "never"
            [<FunScript.JSEmitInline("({0}.pop = {1})"); CompiledName("pop2Aux")>]
            member __.``pop <-``(func : System.Func<string, FunScript.TypeScript.RactivePromise>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.push({1}, {2}))"); CompiledName("push4")>]
            member __.push(keypath : string, value : obj) : FunScript.TypeScript.RactivePromise = failwith "never"
            [<FunScript.JSEmitInline("({0}.push = {1})"); CompiledName("push4Aux")>]
            member __.``push <-``(func : System.Func<string, obj, FunScript.TypeScript.RactivePromise>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.render({1}))"); CompiledName("render")>]
            member __.render(target : obj) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.render = {1})"); CompiledName("renderAux")>]
            member __.``render <-``(func : System.Func<obj, unit>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.reset({?1}))"); CompiledName("reset1")>]
            member __.reset(?data : FunScript.TypeScript.Object) : FunScript.TypeScript.RactivePromise = failwith "never"
            [<FunScript.JSEmitInline("({0}.reset = {1})"); CompiledName("reset1Aux")>]
            member __.``reset <-``(func : System.Func<FunScript.TypeScript.Object, FunScript.TypeScript.RactivePromise>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.resetTemplate())"); CompiledName("resetTemplate")>]
            member __.resetTemplate() : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.resetTemplate = {1})"); CompiledName("resetTemplateAux")>]
            member __.``resetTemplate <-``(func : System.Func<unit>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.set({1}, {2}))"); CompiledName("set27")>]
            member __.set(keypath : string, value : obj) : FunScript.TypeScript.RactivePromise = failwith "never"
            [<FunScript.JSEmitInline("({0}.set = {1})"); CompiledName("set27Aux")>]
            member __.``set <-``(func : System.Func<string, obj, FunScript.TypeScript.RactivePromise>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.set({1}))"); CompiledName("set28")>]
            member __.set(map : FunScript.TypeScript.Object) : FunScript.TypeScript.RactivePromise = failwith "never"
            [<FunScript.JSEmitInline("({0}.set = {1})"); CompiledName("set28Aux")>]
            member __.``set <-``(func : System.Func<FunScript.TypeScript.Object, FunScript.TypeScript.RactivePromise>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.shift({1}))"); CompiledName("shift2")>]
            member __.shift(keypath : string) : FunScript.TypeScript.RactivePromise = failwith "never"
            [<FunScript.JSEmitInline("({0}.shift = {1})"); CompiledName("shift2Aux")>]
            member __.``shift <-``(func : System.Func<string, FunScript.TypeScript.RactivePromise>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.splice({1}, {2}, {3}))"); CompiledName("splice6")>]
            member __.splice(keypath : string, index : float, removeCount : float) : FunScript.TypeScript.RactivePromise = failwith "never"
            [<FunScript.JSEmitInline("({0}.splice = {1})"); CompiledName("splice6Aux")>]
            member __.``splice <-``(func : System.Func<string, float, float, FunScript.TypeScript.RactivePromise>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.splice({1}, {2}, {3}, {4...}))"); CompiledName("splice7")>]
            member __.spliceOverload2(keypath : string, index : float, removeCount : float, [<System.ParamArray>] add : array<obj>) : FunScript.TypeScript.RactivePromise = failwith "never"
            [<FunScript.JSEmitInline("({0}.subtract({1}, {?2}))"); CompiledName("subtract")>]
            member __.subtract(keypath : string, ?number : float) : FunScript.TypeScript.RactivePromise = failwith "never"
            [<FunScript.JSEmitInline("({0}.subtract = {1})"); CompiledName("subtractAux")>]
            member __.``subtract <-``(func : System.Func<string, float, FunScript.TypeScript.RactivePromise>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.teardown())"); CompiledName("teardown1")>]
            member __.teardown() : FunScript.TypeScript.RactivePromise = failwith "never"
            [<FunScript.JSEmitInline("({0}.teardown = {1})"); CompiledName("teardown1Aux")>]
            member __.``teardown <-``(func : System.Func<FunScript.TypeScript.RactivePromise>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.toggle({1}))"); CompiledName("toggle1")>]
            member __.toggle(keypath : string) : FunScript.TypeScript.RactivePromise = failwith "never"
            [<FunScript.JSEmitInline("({0}.toggle = {1})"); CompiledName("toggle1Aux")>]
            member __.``toggle <-``(func : System.Func<string, FunScript.TypeScript.RactivePromise>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.toHTML())"); CompiledName("toHTML")>]
            member __.toHTML() : string = failwith "never"
            [<FunScript.JSEmitInline("({0}.toHTML = {1})"); CompiledName("toHTMLAux")>]
            member __.``toHTML <-``(func : System.Func<string>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.unshift({1}, {2}))"); CompiledName("unshift4")>]
            member __.unshift(keypath : string, value : obj) : FunScript.TypeScript.RactivePromise = failwith "never"
            [<FunScript.JSEmitInline("({0}.unshift = {1})"); CompiledName("unshift4Aux")>]
            member __.``unshift <-``(func : System.Func<string, obj, FunScript.TypeScript.RactivePromise>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.update({?1}))"); CompiledName("update4")>]
            member __.update(?keypath : string) : FunScript.TypeScript.RactivePromise = failwith "never"
            [<FunScript.JSEmitInline("({0}.update = {1})"); CompiledName("update4Aux")>]
            member __.``update <-``(func : System.Func<string, FunScript.TypeScript.RactivePromise>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.updateModel({?1}, {?2}))"); CompiledName("updateModel")>]
            member __.updateModel(?keypath : string, ?cascade : bool) : FunScript.TypeScript.RactivePromise = failwith "never"
            [<FunScript.JSEmitInline("({0}.updateModel = {1})"); CompiledName("updateModelAux")>]
            member __.``updateModel <-``(func : System.Func<string, bool, FunScript.TypeScript.RactivePromise>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.nodes)"); CompiledName("nodes")>]
            member __.nodes with get() : FunScript.TypeScript.Object = failwith "never" and set (v : FunScript.TypeScript.Object) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.partials)"); CompiledName("partials")>]
            member __.partials with get() : FunScript.TypeScript.Object = failwith "never" and set (v : FunScript.TypeScript.Object) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.transitions)"); CompiledName("transitions")>]
            member __.transitions with get() : FunScript.TypeScript.Object = failwith "never" and set (v : FunScript.TypeScript.Object) : unit = failwith "never"

namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_ractive_11 =


    type FunScript.TypeScript.RactiveAdaptorPlugins with 

            [<FunScript.JSEmitInline("({0}[{1}])"); CompiledName("Item49")>]
            member __.Item with get(i : string) : FunScript.TypeScript.RactiveAdaptorPlugin = failwith "never" and set (i : string) (v : FunScript.TypeScript.RactiveAdaptorPlugin) : unit = failwith "never"

namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_ractive_12 =


    type FunScript.TypeScript.RactiveAnimateOptions with 

            [<FunScript.JSEmitInline("({0}.duration)"); CompiledName("duration3")>]
            member __.duration with get() : float = failwith "never" and set (v : float) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.easing)"); CompiledName("easing")>]
            member __.easing with get() : obj = failwith "never" and set (v : obj) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.step)"); CompiledName("step1")>]
            member __.step with get() : System.Func<float, float, unit> = failwith "never" and set (v : System.Func<float, float, unit>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.complate)"); CompiledName("complate")>]
            member __.complate with get() : System.Func<float, float, unit> = failwith "never" and set (v : System.Func<float, float, unit>) : unit = failwith "never"

namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_ractive_13 =


    type FunScript.TypeScript.RactiveAnimationPromise with 

            [<FunScript.JSEmitInline("({0}.stop())"); CompiledName("stop3")>]
            member __.stop() : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.stop = {1})"); CompiledName("stop3Aux")>]
            member __.``stop <-``(func : System.Func<unit>) : unit = failwith "never"

namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_ractive_14 =


    type FunScript.TypeScript.RactiveComponentPlugins with 

            [<FunScript.JSEmitInline("({0}[{1}])"); CompiledName("Item50")>]
            member __.Item with get(i : string) : FunScript.TypeScript.RactiveComponentPlugin = failwith "never" and set (i : string) (v : FunScript.TypeScript.RactiveComponentPlugin) : unit = failwith "never"

namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_ractive_15 =


    type FunScript.TypeScript.RactiveDecoratorPlugin with 

            [<FunScript.JSEmitInline("({0}({1}))"); CompiledName("Invoke36")>]
            member __.Invoke(node : FunScript.TypeScript.HTMLElement) : FunScript.TypeScript.AnonymousType431 = failwith "never"
            [<FunScript.JSEmitInline("({0} = {1})"); CompiledName("Invoke36Aux")>]
            member __.``Invoke <-``(func : System.Func<FunScript.TypeScript.HTMLElement, FunScript.TypeScript.AnonymousType431>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}({1}, {2...}))"); CompiledName("Invoke37")>]
            member __.InvokeOverload2(node : FunScript.TypeScript.HTMLElement, [<System.ParamArray>] args : array<obj>) : FunScript.TypeScript.AnonymousType431 = failwith "never"

namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_ractive_16 =


    type FunScript.TypeScript.RactiveDecoratorPlugins with 

            [<FunScript.JSEmitInline("({0}[{1}])"); CompiledName("Item51")>]
            member __.Item with get(i : string) : FunScript.TypeScript.RactiveDecoratorPlugin = failwith "never" and set (i : string) (v : FunScript.TypeScript.RactiveDecoratorPlugin) : unit = failwith "never"

namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_ractive_17 =


    type FunScript.TypeScript.RactiveDefaultsOptions with 

            [<FunScript.JSEmitInline("({0}.debug)"); CompiledName("debug2")>]
            member __.debug with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"

namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_ractive_18 =


    type FunScript.TypeScript.RactiveEvent with 

            [<FunScript.JSEmitInline("({0}.context)"); CompiledName("context")>]
            member __.context with get() : obj = failwith "never" and set (v : obj) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.index)"); CompiledName("index4")>]
            member __.index with get() : FunScript.TypeScript.Object = failwith "never" and set (v : FunScript.TypeScript.Object) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.keypath)"); CompiledName("keypath")>]
            member __.keypath with get() : string = failwith "never" and set (v : string) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.node)"); CompiledName("node")>]
            member __.node with get() : FunScript.TypeScript.HTMLElement = failwith "never" and set (v : FunScript.TypeScript.HTMLElement) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.original)"); CompiledName("original")>]
            member __.original with get() : FunScript.TypeScript.Event = failwith "never" and set (v : FunScript.TypeScript.Event) : unit = failwith "never"

namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_ractive_19 =


    type FunScript.TypeScript.RactiveEventPlugins with 

            [<FunScript.JSEmitInline("({0}[{1}])"); CompiledName("Item52")>]
            member __.Item with get(i : string) : FunScript.TypeScript.RactiveEventPlugin = failwith "never" and set (i : string) (v : FunScript.TypeScript.RactiveEventPlugin) : unit = failwith "never"

namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_ractive_20 =


    type FunScript.TypeScript.RactiveExtendOptions with 

            [<FunScript.JSEmitInline("({0}.beforeInit)"); CompiledName("beforeInit")>]
            member __.beforeInit with get() : System.Func<FunScript.TypeScript.RactiveExtendOptions, unit> = failwith "never" and set (v : System.Func<FunScript.TypeScript.RactiveExtendOptions, unit>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.init)"); CompiledName("init")>]
            member __.init with get() : System.Func<FunScript.TypeScript.RactiveExtendOptions, unit> = failwith "never" and set (v : System.Func<FunScript.TypeScript.RactiveExtendOptions, unit>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.isolated)"); CompiledName("isolated")>]
            member __.isolated with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"

namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_ractive_21 =


    type FunScript.TypeScript.RactiveNewOptions with 

            [<FunScript.JSEmitInline("({0}.adapt)"); CompiledName("adapt")>]
            member __.adapt with get() : array<obj> = failwith "never" and set (v : array<obj>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.adaptors)"); CompiledName("adaptors")>]
            member __.adaptors with get() : FunScript.TypeScript.RactiveAdaptorPlugins = failwith "never" and set (v : FunScript.TypeScript.RactiveAdaptorPlugins) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.append)"); CompiledName("append2")>]
            member __.append with get() : obj = failwith "never" and set (v : obj) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.complete)"); CompiledName("complete2")>]
            member __.complete with get() : FunScript.TypeScript.Function = failwith "never" and set (v : FunScript.TypeScript.Function) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.components)"); CompiledName("components")>]
            member __.components with get() : FunScript.TypeScript.RactiveComponentPlugins = failwith "never" and set (v : FunScript.TypeScript.RactiveComponentPlugins) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.computed)"); CompiledName("computed")>]
            member __.computed with get() : FunScript.TypeScript.Object = failwith "never" and set (v : FunScript.TypeScript.Object) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.css)"); CompiledName("css")>]
            member __.css with get() : string = failwith "never" and set (v : string) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.data)"); CompiledName("data9")>]
            member __.data with get() : obj = failwith "never" and set (v : obj) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.decorators)"); CompiledName("decorators")>]
            member __.decorators with get() : FunScript.TypeScript.RactiveDecoratorPlugins = failwith "never" and set (v : FunScript.TypeScript.RactiveDecoratorPlugins) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.delimiters)"); CompiledName("delimiters")>]
            member __.delimiters with get() : array<string> = failwith "never" and set (v : array<string>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.easing)"); CompiledName("easing1")>]
            member __.easing with get() : obj = failwith "never" and set (v : obj) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.el)"); CompiledName("el")>]
            member __.el with get() : obj = failwith "never" and set (v : obj) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.events)"); CompiledName("events")>]
            member __.events with get() : FunScript.TypeScript.RactiveEventPlugins = failwith "never" and set (v : FunScript.TypeScript.RactiveEventPlugins) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.interpolators)"); CompiledName("interpolators")>]
            member __.interpolators with get() : FunScript.TypeScript.AnonymousType432 = failwith "never" and set (v : FunScript.TypeScript.AnonymousType432) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.partials)"); CompiledName("partials1")>]
            member __.partials with get() : FunScript.TypeScript.AnonymousType433 = failwith "never" and set (v : FunScript.TypeScript.AnonymousType433) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.sanitize)"); CompiledName("sanitize")>]
            member __.sanitize with get() : obj = failwith "never" and set (v : obj) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.staticDelimiters)"); CompiledName("staticDelimiters")>]
            member __.staticDelimiters with get() : array<string> = failwith "never" and set (v : array<string>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.staticTripleDelimiters)"); CompiledName("staticTripleDelimiters")>]
            member __.staticTripleDelimiters with get() : array<string> = failwith "never" and set (v : array<string>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.template)"); CompiledName("template")>]
            member __.template with get() : obj = failwith "never" and set (v : obj) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.transitions)"); CompiledName("transitions1")>]
            member __.transitions with get() : FunScript.TypeScript.RactiveTransitionPlugins = failwith "never" and set (v : FunScript.TypeScript.RactiveTransitionPlugins) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.tripleDelimiters)"); CompiledName("tripleDelimiters")>]
            member __.tripleDelimiters with get() : array<string> = failwith "never" and set (v : array<string>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.lazy)"); CompiledName("_lazy")>]
            member __._lazy with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.magic)"); CompiledName("magic")>]
            member __.magic with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.modifyArrays)"); CompiledName("modifyArrays")>]
            member __.modifyArrays with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.noCSSTransform)"); CompiledName("noCSSTransform")>]
            member __.noCSSTransform with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.noIntro)"); CompiledName("noIntro")>]
            member __.noIntro with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.preserveWhitespace)"); CompiledName("preserveWhitespace")>]
            member __.preserveWhitespace with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.stripComments)"); CompiledName("stripComments")>]
            member __.stripComments with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.twoway)"); CompiledName("twoway")>]
            member __.twoway with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"

namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_ractive_22 =


    type FunScript.TypeScript.RactiveObserve with 

            [<FunScript.JSEmitInline("({0}.cancel())"); CompiledName("cancel")>]
            member __.cancel() : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.cancel = {1})"); CompiledName("cancelAux")>]
            member __.``cancel <-``(func : System.Func<unit>) : unit = failwith "never"

namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_ractive_23 =


    type FunScript.TypeScript.RactiveObserveOptions with 

            [<FunScript.JSEmitInline("({0}.context)"); CompiledName("context1")>]
            member __.context with get() : obj = failwith "never" and set (v : obj) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.debug)"); CompiledName("debug3")>]
            member __.debug with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.defer)"); CompiledName("defer1")>]
            member __.defer with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.init)"); CompiledName("init1")>]
            member __.init with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"

namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_ractive_24 =


    type FunScript.TypeScript.RactiveParseOptions with 

            [<FunScript.JSEmitInline("({0}.preserveWhitespace)"); CompiledName("preserveWhitespace1")>]
            member __.preserveWhitespace with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.sanitize)"); CompiledName("sanitize1")>]
            member __.sanitize with get() : obj = failwith "never" and set (v : obj) : unit = failwith "never"

namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_ractive_25 =


    type FunScript.TypeScript.RactiveSanitizeOptions with 

            [<FunScript.JSEmitInline("({0}.elements)"); CompiledName("elements1")>]
            member __.elements with get() : array<string> = failwith "never" and set (v : array<string>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.eventAttributes)"); CompiledName("eventAttributes")>]
            member __.eventAttributes with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"

namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_ractive_26 =


    type FunScript.TypeScript.RactiveStatic with 

            [<FunScript.JSEmitInline("(new {0}({1}))"); CompiledName("Create463")>]
            member __.Create(options : FunScript.TypeScript.RactiveNewOptions) : FunScript.TypeScript.Ractive = failwith "never"
            [<FunScript.JSEmitInline("(new {0} = {1})"); CompiledName("Create463Aux")>]
            member __.``Create <-``(func : System.Func<FunScript.TypeScript.RactiveNewOptions, FunScript.TypeScript.Ractive>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.extend({1}))"); CompiledName("extend")>]
            member __.extend(options : FunScript.TypeScript.RactiveExtendOptions) : FunScript.TypeScript.RactiveStatic = failwith "never"
            [<FunScript.JSEmitInline("({0}.extend = {1})"); CompiledName("extendAux")>]
            member __.``extend <-``(func : System.Func<FunScript.TypeScript.RactiveExtendOptions, FunScript.TypeScript.RactiveStatic>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.parse({1}, {?2}))"); CompiledName("parse2")>]
            member __.parse(template : string, ?options : FunScript.TypeScript.RactiveParseOptions) : obj = failwith "never"
            [<FunScript.JSEmitInline("({0}.parse = {1})"); CompiledName("parse2Aux")>]
            member __.``parse <-``(func : System.Func<string, FunScript.TypeScript.RactiveParseOptions, obj>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.adaptors)"); CompiledName("adaptors1")>]
            member __.adaptors with get() : FunScript.TypeScript.RactiveAdaptorPlugins = failwith "never" and set (v : FunScript.TypeScript.RactiveAdaptorPlugins) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.components)"); CompiledName("components1")>]
            member __.components with get() : FunScript.TypeScript.RactiveComponentPlugins = failwith "never" and set (v : FunScript.TypeScript.RactiveComponentPlugins) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.defaults)"); CompiledName("defaults")>]
            member __.defaults with get() : FunScript.TypeScript.RactiveDefaultsOptions = failwith "never" and set (v : FunScript.TypeScript.RactiveDefaultsOptions) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.decorators)"); CompiledName("decorators1")>]
            member __.decorators with get() : FunScript.TypeScript.RactiveDecoratorPlugins = failwith "never" and set (v : FunScript.TypeScript.RactiveDecoratorPlugins) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.easing)"); CompiledName("easing2")>]
            member __.easing with get() : FunScript.TypeScript.AnonymousType434 = failwith "never" and set (v : FunScript.TypeScript.AnonymousType434) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.events)"); CompiledName("events1")>]
            member __.events with get() : FunScript.TypeScript.RactiveEventPlugins = failwith "never" and set (v : FunScript.TypeScript.RactiveEventPlugins) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.partials)"); CompiledName("partials2")>]
            member __.partials with get() : FunScript.TypeScript.AnonymousType435 = failwith "never" and set (v : FunScript.TypeScript.AnonymousType435) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.Promise)"); CompiledName("Promise")>]
            member __.Promise with get() : FunScript.TypeScript.RactivePromise = failwith "never" and set (v : FunScript.TypeScript.RactivePromise) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.transitions)"); CompiledName("transitions2")>]
            member __.transitions with get() : FunScript.TypeScript.RactiveTransitionPlugins = failwith "never" and set (v : FunScript.TypeScript.RactiveTransitionPlugins) : unit = failwith "never"

namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_ractive_27 =


    type FunScript.TypeScript.RactiveTransition with 

            [<FunScript.JSEmitInline("({0}.isIntro)"); CompiledName("isIntro")>]
            member __.isIntro with get() : bool = failwith "never" and set (v : bool) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.name)"); CompiledName("name35")>]
            member __.name with get() : string = failwith "never" and set (v : string) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.node)"); CompiledName("node1")>]
            member __.node with get() : FunScript.TypeScript.HTMLElement = failwith "never" and set (v : FunScript.TypeScript.HTMLElement) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.animateStyle({1}, {2}, {3}, {4}))"); CompiledName("animateStyle")>]
            member __.animateStyle(prop : string, value : obj, options : FunScript.TypeScript.RactiveTransitionAnimateOptions, complete : FunScript.TypeScript.Function) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.animateStyle = {1})"); CompiledName("animateStyleAux")>]
            member __.``animateStyle <-``(func : System.Func<string, obj, FunScript.TypeScript.RactiveTransitionAnimateOptions, FunScript.TypeScript.Function, unit>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.animateStyle({1}, {2}, {3}))"); CompiledName("animateStyle1")>]
            member __.animateStyle(props : FunScript.TypeScript.Object, options : FunScript.TypeScript.RactiveTransitionAnimateOptions, complete : FunScript.TypeScript.Function) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.animateStyle = {1})"); CompiledName("animateStyle1Aux")>]
            member __.``animateStyle <-``(func : System.Func<FunScript.TypeScript.Object, FunScript.TypeScript.RactiveTransitionAnimateOptions, FunScript.TypeScript.Function, unit>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.complete({?1}))"); CompiledName("complete3")>]
            member __.complete(?noReset : bool) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.complete = {1})"); CompiledName("complete3Aux")>]
            member __.``complete <-``(func : System.Func<bool, unit>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.getStyle({1}))"); CompiledName("getStyle")>]
            member __.getStyle(prop : string) : string = failwith "never"
            [<FunScript.JSEmitInline("({0}.getStyle = {1})"); CompiledName("getStyleAux")>]
            member __.``getStyle <-``(func : System.Func<string, string>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.getStyle({1}))"); CompiledName("getStyle1")>]
            member __.getStyle(props : array<string>) : FunScript.TypeScript.Object = failwith "never"
            [<FunScript.JSEmitInline("({0}.getStyle = {1})"); CompiledName("getStyle1Aux")>]
            member __.``getStyle <-``(func : System.Func<array<string>, FunScript.TypeScript.Object>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.processParams({1}, {?2}))"); CompiledName("processParams")>]
            member __.processParams(_params : obj, ?defaults : FunScript.TypeScript.Object) : FunScript.TypeScript.Object = failwith "never"
            [<FunScript.JSEmitInline("({0}.processParams = {1})"); CompiledName("processParamsAux")>]
            member __.``processParams <-``(func : System.Func<obj, FunScript.TypeScript.Object, FunScript.TypeScript.Object>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.resetStyle())"); CompiledName("resetStyle")>]
            member __.resetStyle() : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.resetStyle = {1})"); CompiledName("resetStyleAux")>]
            member __.``resetStyle <-``(func : System.Func<unit>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.setStyle({1}, {2}))"); CompiledName("setStyle")>]
            member __.setStyle(prop : string, value : obj) : FunScript.TypeScript.RactiveTransition = failwith "never"
            [<FunScript.JSEmitInline("({0}.setStyle = {1})"); CompiledName("setStyleAux")>]
            member __.``setStyle <-``(func : System.Func<string, obj, FunScript.TypeScript.RactiveTransition>) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.setStyle({1}))"); CompiledName("setStyle1")>]
            member __.setStyle(props : FunScript.TypeScript.Object) : FunScript.TypeScript.RactiveTransition = failwith "never"
            [<FunScript.JSEmitInline("({0}.setStyle = {1})"); CompiledName("setStyle1Aux")>]
            member __.``setStyle <-``(func : System.Func<FunScript.TypeScript.Object, FunScript.TypeScript.RactiveTransition>) : unit = failwith "never"

namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_ractive_28 =


    type FunScript.TypeScript.RactiveTransitionAnimateOptions with 

            [<FunScript.JSEmitInline("({0}.duration)"); CompiledName("duration4")>]
            member __.duration with get() : float = failwith "never" and set (v : float) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.easing)"); CompiledName("easing3")>]
            member __.easing with get() : string = failwith "never" and set (v : string) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0}.delay)"); CompiledName("delay")>]
            member __.delay with get() : float = failwith "never" and set (v : float) : unit = failwith "never"

namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_ractive_29 =


    type FunScript.TypeScript.RactiveTransitionPlugin with 

            [<FunScript.JSEmitInline("({0}({1}, {2}))"); CompiledName("Invoke38")>]
            member __.Invoke(t : FunScript.TypeScript.RactiveTransition, _params : FunScript.TypeScript.Object) : unit = failwith "never"
            [<FunScript.JSEmitInline("({0} = {1})"); CompiledName("Invoke38Aux")>]
            member __.``Invoke <-``(func : System.Func<FunScript.TypeScript.RactiveTransition, FunScript.TypeScript.Object, unit>) : unit = failwith "never"

namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_ractive_30 =


    type FunScript.TypeScript.RactiveTransitionPlugins with 

            [<FunScript.JSEmitInline("({0}[{1}])"); CompiledName("Item53")>]
            member __.Item with get(i : string) : FunScript.TypeScript.RactiveTransitionPluginDelegate = failwith "never" and set (i : string) (v : FunScript.TypeScript.RactiveTransitionPluginDelegate) : unit = failwith "never"
