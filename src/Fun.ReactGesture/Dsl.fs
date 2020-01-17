[<AutoOpen>]
module Fun.ReactGesture.Dsl

open Fable.Core
open Fable.Core.JsInterop
open Fable.React


[<RequireQualifiedAccess; Erase>]
type DomTarget =
    | Node of Browser.Types.Node
    | Ref of IRefHook<obj>

[<RequireQualifiedAccess; Erase>]
type Initial =
    | Vector of float[]
    | VectorByFn of (unit -> float[])

[<RequireQualifiedAccess; StringEnum>]
type Xy =
    | X
    | Y

[<RequireQualifiedAccess; StringEnum>]
type Bound =
    | Top
    | Bottom
    | Left
    | Right

[<RequireQualifiedAccess>]
type MinMax =
    | Min of float
    | Max of float


type IGestureProp =
    interface end


[<RequireQualifiedAccess>]
type GestureProp =
    | DomTarget of DomTarget
    | [<CompiledName("eventOptions.capture")>] Capture of bool
    | [<CompiledName("eventOptions.passive")>] Passive of bool
    | [<CompiledName("eventOptions.pointer")>] Pointer of bool
    | Window of Browser.Types.Node
    | Enabled of bool
    | Initial of Initial
    | Threshold of float
    | Rubberband of bool
    | [<Erase>] Custom of string * obj
    interface IGestureProp

type XyConfigProp =
    | Axis of Xy
    | LockDirection of bool
    interface IGestureProp
    static member Bounds (bounds: Bound seq) = GestureProp.Custom ("bounds", keyValueList CaseRules.LowerFirst bounds)

type PinchConfigProp =
    interface IGestureProp
    static member DistanceBounds (bounds: MinMax seq) = GestureProp.Custom("distanceBounds", keyValueList CaseRules.LowerFirst bounds)
    static member AngleBounds (bounds: MinMax seq) = GestureProp.Custom("angleBounds", keyValueList CaseRules.LowerFirst bounds)


[<RequireQualifiedAccess>]
type GestureEvent =
    | OnDrag of (Bindings.IGestureState -> unit)
    | OnDragStart of (Bindings.IGestureState -> unit)
    | OnDragEnd of (Bindings.IGestureState -> unit)
    | OnPinch of (Bindings.IGestureState -> unit)
    | OnPinchStart of (Bindings.IGestureState -> unit)
    | OnPinchEnd of (Bindings.IGestureState -> unit)
    | OnScroll of (Bindings.IGestureState -> unit)
    | OnScrollStart of (Bindings.IGestureState -> unit)
    | OnScrollEnd of (Bindings.IGestureState -> unit)
    | OnMove of (Bindings.IGestureState -> unit)
    | OnMoveStart of (Bindings.IGestureState -> unit)
    | OnMoveEnd of (Bindings.IGestureState -> unit)
    | OnWheel of (Bindings.IGestureState -> unit)
    | OnWheelStart of (Bindings.IGestureState -> unit)
    | OnWheelEnd of (Bindings.IGestureState -> unit)
    | OnHover of (Bindings.IGestureState -> unit)


type GestureHooks() =
    static member inline useGesture(gestures: GestureEvent seq, ?props: IGestureProp seq) = Bindings.ssrGesture (fun () -> Bindings.useGesture (keyValueList CaseRules.LowerFirst gestures |> unbox) (Bindings.fromOptionToJsObj props |> unbox))
    static member inline useDrag(onEvent, ?props: IGestureProp seq)   = Bindings.ssrGesture (fun () -> Bindings.useDrag onEvent (Bindings.fromOptionToJsObj props |> unbox))
    static member inline useMove(onEvent, ?props: IGestureProp seq)   = Bindings.ssrGesture (fun () -> Bindings.useMove onEvent (Bindings.fromOptionToJsObj props |> unbox))
    //static member inline useHover(onEvent, ?props: IGestureProp seq)  = Bindings.ssrGesture (fun () -> Bindings.useHover onEvent (Bindings.fromOptionToJsObj props |> unbox))
    static member inline useScroll(onEvent, ?props: IGestureProp seq) = Bindings.ssrGesture (fun () -> Bindings.useScroll onEvent (Bindings.fromOptionToJsObj props |> unbox))
    static member inline useWheel(onEvent, ?props: IGestureProp seq)  = Bindings.ssrGesture (fun () -> Bindings.useWheel onEvent (Bindings.fromOptionToJsObj props |> unbox))
    static member inline usePinch(onEvent, ?props: IGestureProp seq)  = Bindings.ssrGesture (fun () -> Bindings.usePinch onEvent (Bindings.fromOptionToJsObj props |> unbox))
