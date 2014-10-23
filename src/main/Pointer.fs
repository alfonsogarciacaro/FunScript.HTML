namespace FunScript.TypeScript

// TypeScript lib.d.ts doesn't include Touch interfaces so I add them here

type Touch = interface end
type TouchList = interface end
type TouchEvent = inherit UIEvent
//type TouchEventStatic = interface end

namespace FunScript.TypeScript

[<AutoOpen>]
module TypeExtensions_touch =
    type FunScript.TypeScript.Touch with 
        /// The X coordinate of the touch point relative to the left edge of the browser viewport, not including any scroll offset.
        [<FunScript.JSEmitInline("({0}.clientX)"); CompiledName("clientX4")>]
        member __.clientX with get() : float = failwith "never"

        /// The Y coordinate of the touch point relative to the top edge of the browser viewport, not including any scroll offset.
        [<FunScript.JSEmitInline("({0}.clientY)"); CompiledName("clientY4")>]
        member __.clientY with get() : float = failwith "never"

        /// The amount of pressure being applied to the surface by the user, as a float between 0.0 (no pressure) and 1.0 (maximum pressure). Hardware-dependent.
        [<FunScript.JSEmitInline("({0}.force)"); CompiledName("force")>]
        member __.force with get() : float = failwith "never"

        /// A unique identifier for this Touch object. A given touch (say, by a finger) will have the same identifier for the duration of its movement around the surface. This lets you ensure that you're tracking the same touch all the time. 
        [<FunScript.JSEmitInline("({0}.identifier)"); CompiledName("identifier")>]
        member __.identifier with get() : float = failwith "never"

        /// The X coordinate of the touch point relative to the left edge of the document. Unlike clientX, this value includes the horizontal scroll offset, if any.
        [<FunScript.JSEmitInline("({0}.pageX)"); CompiledName("pageX1")>]
        member __.pageX with get() : float = failwith "never"

        /// The Y coordinate of the touch point relative to the top of the document. Unlike clientY, this value includes the vertical scroll offset, if any.
        [<FunScript.JSEmitInline("({0}.pageY)"); CompiledName("pageY1")>]
        member __.pageY with get() : float = failwith "never"

        /// The X radius of the ellipse that most closely circumscribes the area of contact with the screen. The value is in pixels of the same scale as screenX. Hardware-dependent.
        [<FunScript.JSEmitInline("({0}.radiusX)"); CompiledName("radiusX1")>]
        member __.radiusX with get() : float = failwith "never"

        /// The Y radius of the ellipse that most closely circumscribes the area of contact with the screen. The value is in pixels of the same scale as screenY. Hardware-dependent.
        [<FunScript.JSEmitInline("({0}.radiusY)"); CompiledName("radiusY1")>]
        member __.radiusY with get() : float = failwith "never"

        /// The angle (in degrees) that the ellipse described by radiusX and radiusY must be rotated, clockwise, to most accurately cover the area of contact between the user and the surface. Hardware-dependent.
        [<FunScript.JSEmitInline("({0}.rotationAngle)"); CompiledName("rotationAngle")>]
        member __.rotationAngle with get() : float = failwith "never"

        /// The X coordinate of the touch point relative to the left edge of the screen. 
        [<FunScript.JSEmitInline("({0}.screenX)"); CompiledName("screenX6")>]
        member __.screenX with get() : float = failwith "never"

        /// The Y coordinate of the touch point relative to the top edge of the screen.
        [<FunScript.JSEmitInline("({0}.screenY)"); CompiledName("screenY6")>]
        member __.screenY with get() : float = failwith "never"

        /// The Element on which the touch point started when it was first placed on the surface, even if the touch point has since moved outside the interactive area of that element or even been removed from the document.
        [<FunScript.JSEmitInline("({0}.target)"); CompiledName("target12")>]
        member __.target with get() : FunScript.TypeScript.EventTarget = failwith "never"

    type FunScript.TypeScript.TouchEvent with 
        /// A TouchList of all the Touch objects representing all current points of contact with the surface, regardless of target or changed status.    
        [<FunScript.JSEmitInline("({0}.touches)"); CompiledName("touches")>]
        member __.touches with get() : FunScript.TypeScript.TouchList = failwith "never"

        /// A TouchList of all the Touch objects that are both currently in contact with the touch surface and were also started on the same element that is the target of the event.
        [<FunScript.JSEmitInline("({0}.targetTouches)"); CompiledName("targetTouches")>]
        member __.targetTouches with get() : FunScript.TypeScript.TouchList = failwith "never"

        /// A TouchList of all the Touch objects representing individual points of contact whose states changed between the previous touch event and this one.
        [<FunScript.JSEmitInline("({0}.changedTouches)"); CompiledName("changedTouches")>]
        member __.changedTouches with get() : FunScript.TypeScript.TouchList = failwith "never"

        [<FunScript.JSEmitInline("({0}.altKey)"); CompiledName("altKey4")>]
        member __.altKey with get() : bool = failwith "never"
        [<FunScript.JSEmitInline("({0}.metaKey)"); CompiledName("metaKey3")>]
        member __.metaKey with get() : bool = failwith "never"
        [<FunScript.JSEmitInline("({0}.ctrlKey)"); CompiledName("ctrlKey4")>]
        member __.ctrlKey with get() : bool = failwith "never"
        [<FunScript.JSEmitInline("({0}.shiftKey)"); CompiledName("shiftKey4")>]
        member __.shiftKey with get() : bool = failwith "never"

    type FunScript.TypeScript.TouchList with
        [<FunScript.JSEmitInline("({0}.length)"); CompiledName("length52")>]
        member __.Length with get() : int = failwith "never"

        [<FunScript.JSEmitInline("({0}.item({1}))"); CompiledName("item33")>]
        member __.Item with get(index : int) : FunScript.TypeScript.Touch = failwith "never"

        /// Returns the first Touch item in the list whose identifier matches a specified value.
        [<FunScript.JSEmitInline("({0}.identifiedTouch({1}))"); CompiledName("identifiedTouch")>]
        member __.identifiedTouch(identifier : float) : FunScript.TypeScript.Touch = failwith "never"

//        [<FunScript.JSEmitInline("({0}.length)"); CompiledName("length52")>]
//        member __.length with get() : float = failwith "never"
//        [<FunScript.JSEmitInline("({0}.item({1}))"); CompiledName("item33")>]
//        member __.item(index : float) : FunScript.TypeScript.Touch = failwith "never"

//    type FunScript.TypeScript.Global with 
//        [<FunScript.JSEmitInline("(window.TouchEvent)"); CompiledName("TouchEvent")>]
//        static member TouchEvent with get() : FunScript.TypeScript.TouchEventStatic = failwith "never" and set (v : FunScript.TypeScript.TouchEventStatic) : unit = failwith "never"

//    type FunScript.TypeScript.TouchEventStatic with 
//        [<FunScript.JSEmitInline("({0}.prototype)"); CompiledName("prototype428")>]
//        member __.prototype with get() : FunScript.TypeScript.TouchEvent = failwith "never" and set (v : FunScript.TypeScript.TouchEvent) : unit = failwith "never"
//        [<FunScript.JSEmitInline("(new {0}())"); CompiledName("Create473")>]
//        member __.Create() : FunScript.TypeScript.TouchEvent = failwith "never"

    type FunScript.TypeScript.HTMLElement with 
        [<FunScript.JSEmitInline("({0}.ontouchstart)"); CompiledName("ontouchstart")>]
        member __.ontouchstart with get() : System.Func<FunScript.TypeScript.TouchEvent, obj> = failwith "never"
        [<FunScript.JSEmitInline("({0}.ontouchmove)"); CompiledName("ontouchmove")>]
        member __.ontouchmove with get() : System.Func<FunScript.TypeScript.TouchEvent, obj> = failwith "never"
        [<FunScript.JSEmitInline("({0}.ontouchend)"); CompiledName("ontouchend")>]
        member __.ontouchend with get() : System.Func<FunScript.TypeScript.TouchEvent, obj> = failwith "never"
        [<FunScript.JSEmitInline("({0}.ontouchcancel)"); CompiledName("ontouchcancel")>]
        member __.ontouchcancel with get() : System.Func<FunScript.TypeScript.TouchEvent, obj> = failwith "never"

namespace FunScript.HTML

module PointerLiterals =
    let [<Literal>] touchstart  = "touchstart"
    let [<Literal>] touchmove   = "touchmove"
    let [<Literal>] touchend    = "touchend"
    let [<Literal>] touchcancel = "touchcancel"

    let [<Literal>] mousedown  = "mousedown"
    let [<Literal>] mouseenter = "mouseenter"
    let [<Literal>] mouseleave = "mouseleave"
    let [<Literal>] mousemove  = "mousemove"
    let [<Literal>] mouseout   = "mouseout"
    let [<Literal>] mouseover  = "mouseover"
    let [<Literal>] mouseeup   = "mouseup"

[<AutoOpen; ReflectedDefinition>]
module Pointer =
    open FunScript.Core.Events
    open FunScript.TypeScript

    type TouchCase =
        /// A touch point is placed on the touch surface.
        | TouchStart of TouchEvent
        /// A touch point is moved along the touch surface.
        | TouchMove of TouchEvent
        /// A touch point is removed from the touch surface.
        | TouchEnd of TouchEvent
        /// A touch point has been disrupted in an implementation-specific manners (too many touch points for example).
        | TouchCancel of TouchEvent

    type MouseCase =
        /// A pointing device button (usually a mouse) is pressed on an element.
        | MouseDown of MouseEvent
        /// A pointing device is moved onto the element that has the listener attached.
        | MouseEnter of MouseEvent
        /// A pointing device is moved off the element that has the listener attached.
        | MouseLeave of MouseEvent
        /// A pointing device is moved over an element.
        | MouseMove of MouseEvent
        /// A pointing device is moved off the element that has the listener attached or off one of its children.
        | MouseOut of MouseEvent
        /// A pointing device is moved onto the element that has the listener attached or onto one of its children.
        | MouseOver of MouseEvent
        /// A pointing device button is released over an element.
        | MouseUp of MouseEvent

    type private TouchObservable(el: HTMLElement) =
        interface IObservable<TouchCase> with
            member x.Subscribe(observer) =
                let listener1 = unbox (fun (ev: TouchEvent) -> ev.preventDefault(); observer.OnNext(TouchStart ev))
                let listener2 = unbox (fun (ev: TouchEvent) -> ev.preventDefault(); observer.OnNext(TouchMove ev))
                let listener3 = unbox (fun (ev: TouchEvent) -> ev.preventDefault(); observer.OnNext(TouchCancel ev))
                let listener4 = unbox (fun (ev: TouchEvent) -> ev.preventDefault(); observer.OnNext(TouchEnd ev))
                el.addEventListener(PointerLiterals.touchstart,  listener1, false)
                el.addEventListener(PointerLiterals.touchmove,   listener2, false)
                el.addEventListener(PointerLiterals.touchend,    listener3, false)
                el.addEventListener(PointerLiterals.touchcancel, listener4, false)
                new ActionDisposable(fun () ->
                    el.removeEventListener(PointerLiterals.touchstart,  listener1, false)
                    el.removeEventListener(PointerLiterals.touchmove,   listener2, false)
                    el.removeEventListener(PointerLiterals.touchend,    listener3, false)
                    el.removeEventListener(PointerLiterals.touchcancel, listener4, false)
                ) :> System.IDisposable

    type private MouseObservable(el: HTMLElement) =
        interface IObservable<MouseCase> with
            member x.Subscribe(observer) =
                let listener1 = unbox (fun ev -> observer.OnNext(MouseDown ev))
                let listener2 = unbox (fun ev -> observer.OnNext(MouseEnter ev))
                let listener3 = unbox (fun ev -> observer.OnNext(MouseLeave ev))
                let listener4 = unbox (fun ev -> observer.OnNext(MouseMove ev))
                let listener5 = unbox (fun ev -> observer.OnNext(MouseOut ev))
                let listener6 = unbox (fun ev -> observer.OnNext(MouseOver ev))
                let listener7 = unbox (fun ev -> observer.OnNext(MouseUp ev))
                el.addEventListener(PointerLiterals.mousedown,  listener1, false)
                el.addEventListener(PointerLiterals.mouseenter, listener2, false)
                el.addEventListener(PointerLiterals.mouseleave, listener3, false)
                el.addEventListener(PointerLiterals.mousemove,  listener4, false)
                el.addEventListener(PointerLiterals.mouseout,   listener5, false)
                el.addEventListener(PointerLiterals.mouseover,  listener6, false)
                el.addEventListener(PointerLiterals.mouseeup,   listener7, false)
                new ActionDisposable(fun () ->
                    el.removeEventListener(PointerLiterals.mousedown,  listener1, false)
                    el.removeEventListener(PointerLiterals.mouseenter, listener2, false)
                    el.removeEventListener(PointerLiterals.mouseleave, listener3, false)
                    el.removeEventListener(PointerLiterals.mousemove,  listener4, false)
                    el.removeEventListener(PointerLiterals.mouseout,   listener5, false)
                    el.removeEventListener(PointerLiterals.mouseover,  listener6, false)
                    el.removeEventListener(PointerLiterals.mouseeup,   listener7, false)
                ) :> System.IDisposable

    type FunScript.TypeScript.HTMLElement with 
        /// A touch point is placed on the touch surface.
        member __.ontouchstartStream  with get() = HTMLEventStream<TouchEvent>(__, PointerLiterals.touchstart)

        /// A touch point is moved along the touch surface.
        member __.ontouchmoveStream   with get() = HTMLEventStream<TouchEvent>(__, PointerLiterals.touchmove)

        /// A touch point is removed from the touch surface.
        member __.ontouchendStream    with get() = HTMLEventStream<TouchEvent>(__, PointerLiterals.touchend)

        /// A touch point has been disrupted in an implementation-specific manners (too many touch points for example).
        member __.ontouchcancelStream with get() = HTMLEventStream<TouchEvent>(__, PointerLiterals.touchcancel)

        /// All touch event types merged in a single stream.
        member __.ontouchStream
            with get(): System.IObservable<TouchCase> = unbox(TouchObservable(__) :> IObservable<_>)

        /// All mouse event types merged in a single stream.
        member __.onmouseStream
            with get(): System.IObservable<MouseCase> = unbox(MouseObservable(__) :> IObservable<_>)
