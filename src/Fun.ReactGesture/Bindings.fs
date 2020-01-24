module Fun.ReactGesture.Bindings

open Fable.Core
open Fable.Core.JsInterop
open Fable.React.Isomorphic


type IGesturePoint =
    [<Emit("$0[0]")>]
    abstract x: float
    [<Emit("$0[1]")>]
    abstract y: float

type IGestureState =
    // the source event
    abstract event: Browser.Types.Event
    // [x,y] values (pointer position or scroll offset)
    abstract xy: IGesturePoint
    // previous xy
    abstract previous: IGesturePoint
    // xy value when the gesture started
    abstract initial: IGesturePoint
    // last gesture offset (xy - initial)
    abstract movement: IGesturePoint
    // movement delta (movement - previous movement)
    abstract delta: IGesturePoint
    // offset since the first gesture
    abstract offset: IGesturePoint
    // offset when the last gesture started
    abstract lastOffset: IGesturePoint
    // momentum of the gesture per axis
    abstract vxvy: float
    // absolute velocity of the gesture
    abstract velocity: float
    // offset distance
    abstract distance: float
    // direction per axis
    abstract direction: IGesturePoint
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


type IPinchState =
    inherit IGestureState
    // [d,a] absolute distance and angle of the two pointers
    abstract da: IGesturePoint
    // momentum of the gesture of distance and rotation
    abstract vdva: IGesturePoint
    // coordinates of the center between the two touch event
    abstract origin: IGesturePoint


type IDragState =
    inherit IGestureState
    // [swipeX, swipeY] 0 if no swipe detected, -1 or 1 otherwise
    abstract swipe: IGesturePoint
    // is the drag assimilated to a tap
    abstract tap: bool


type IGestureAttrs =
    [<Emit("$0($1)")>]
    abstract bind: obj -> obj


let fakeGestureAttrs =
    { new IGestureAttrs with
        member _.bind _ = obj() }


let ssrGesture fn =
    isomorphicExec
        (fun () -> fn())
        (fun () -> fakeGestureAttrs)
        ()

    
let fromOptionToJsObj props =
    match props with
    | None -> box {||}
    | Some x -> keyValueList CaseRules.LowerFirst x |> box


let useGesture gestureEvents config: IGestureAttrs = import "useGesture" "react-use-gesture"

let useDrag     (onEvt: IGestureState -> unit) config: IGestureAttrs = import "useDrag" "react-use-gesture"
let useMove     (onEvt: IGestureState -> unit) config: IGestureAttrs = import "useMove" "react-use-gesture"
//let useHover    (onEvt: IGestureState -> unit) config: IGestureAttrs = import "useHover" "react-use-gesture"
let useScroll   (onEvt: IGestureState -> unit) config: IGestureAttrs = import "useScroll" "react-use-gesture"
let useWheel    (onEvt: IGestureState -> unit) config: IGestureAttrs = import "useWheel" "react-use-gesture"
let usePinch    (onEvt: IGestureState -> unit) config: IGestureAttrs = import "usePinch" "react-use-gesture"
