namespace Fun.ReactGesture.Bindings

open Fable.Core
open Fable.Core.JsInterop


type Point =
    [<Emit("$0[0]")>]
    abstract x: float
    [<Emit("$0[1]")>]
    abstract y: float

type State =
    // the source event
    abstract event: Browser.Types.Event
    // [x,y] values (pointer position or scroll offset)
    abstract xy: Point
    // previous xy
    abstract previous: Point
    // xy value when the gesture started
    abstract initial: Point
    // last gesture offset (xy - initial)
    abstract movement: Point
    // movement delta (movement - previous movement)
    abstract delta: Point
    // offset since the first gesture
    abstract offset: Point
    // offset when the last gesture started
    abstract lastOffset: Point
    // momentum of the gesture per axis
    abstract vxvy: float
    // absolute velocity of the gesture
    abstract velocity: float
    // offset distance
    abstract distance: float
    // direction per axis
    abstract direction: float
    // gesture start time
    abstract startTime: obj
    // gesture elapsed time
    abstract elapsedTime: obj
    // timestamp of the event
    abstract timeStamp: obj
    // true when it's the first event
    abstract first: bool
    // true when it's the last event
    abstract last: bool
    // true when the gesture is active
    abstract active: bool
    // value returned by your handler on its previous run
    abstract memo: obj
    // function you can call to interrupt some gestures
    abstract cancel: unit -> unit
    // whether the gesture was canceled (drag and pinch)
    abstract canceled: bool
    // true when a mouse button or touch is down
    abstract down: bool
    // number of buttons pressed
    abstract buttons: int
    // number of fingers touching the screen
    abstract touches: int
    // arguments you passed to bind
    abstract args: obj[]
    // true when control key is pressed
    abstract ctrlKey: bool
    // "      "  alt     "      "
    abstract altKey: bool
    // "      "  shift   "      "
    abstract shift: bool
    // "      "  meta    "      "
    abstract metaKey: string
    // is the component currently being dragged
    abstract dragging: bool
    // "              "              "  moved
    abstract moving: bool
    // "              "              "  scrolled
    abstract scrolling: bool
    // "              "              "  wheeled
    abstract wheeling: bool
    // "              "              "  pinched
    abstract pinching: bool


type PinchState =
    inherit State
    // [d,a] absolute distance and angle of the two pointers
    abstract da: Point
    // momentum of the gesture of distance and rotation
    abstract vdva: Point
    // coordinates of the center between the two touch event
    abstract origin: Point


type DragState =
    inherit State
    // [swipeX, swipeY] 0 if no swipe detected, -1 or 1 otherwise
    abstract swipe: Point
    // is the drag assimilated to a tap
    abstract tap: bool


type Bind =
    [<Emit("$0($1)")>]
    abstract bind: obj -> obj


type IConfig = obj


type IGesture =
    interface end



type IGestureHooks =
    abstract member useGesture: IGesture -> IConfig -> Bind
    abstract member useDrag:    (DragState -> unit) -> IConfig -> Bind
    abstract member useMove:    (State -> unit) -> IConfig -> Bind
    abstract member useHover:   (State -> unit) -> IConfig -> Bind
    abstract member useScroll:  (State -> unit) -> IConfig -> Bind
    abstract member useWheel:   (State -> unit) -> IConfig -> Bind
    abstract member usePinch:   (PinchState -> unit) -> IConfig -> Bind


module Hooks =
    let dummyBind =
        {
            new Bind with
                member _.bind _ = obj()
        }

    [<Import("*", "react-use-gesture")>]
    let GestureHooks =
        {
            new IGestureHooks with
                member _.useGesture _ _ = dummyBind
                member _.useDrag _ _ = dummyBind
                member _.useMove _ _ = dummyBind
                member _.useHover _ _ = dummyBind
                member _.useScroll _ _ = dummyBind
                member _.useWheel _ _ = dummyBind
                member _.usePinch _ _ = dummyBind
        }
