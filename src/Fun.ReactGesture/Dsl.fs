[<AutoOpen>]
module Fun.ReactGesture.Dsl

open Fable.Core
open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props


[<RequireQualifiedAccess; Erase>]
type DomTarget =
    | Node of Browser.Types.Node
    | Ref of IRefHook<obj>

[<RequireQualifiedAccess; Erase>]
type Initial =
    | Vector of float[]
    | VectorFn of (unit -> float[])


type IConfigProp =
    interface end


[<RequireQualifiedAccess>]
type ConfigProp =
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
    interface IConfigProp

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

type XyConfigProp() =
    static member Axis (xOry: Xy) = ConfigProp.Custom ("axis", xOry)
    static member LockDirection (b: bool) = ConfigProp.Custom ("lockDirection", b)
    static member Bounds (bounds: Bound seq) = ConfigProp.Custom ("bounds", keyValueList CaseRules.LowerFirst bounds)

[<RequireQualifiedAccess>]
type MinMax =
    | Min of float
    | Max of float

type PinchConfigProp() =
    static member DistanceBounds (bounds: MinMax seq) = ConfigProp.Custom("distanceBounds", keyValueList CaseRules.LowerFirst bounds)
    static member AngleBounds (bounds: MinMax seq) = ConfigProp.Custom("angleBounds", keyValueList CaseRules.LowerFirst bounds)


[<RequireQualifiedAccess>]
type Gesture =
    | OnDrag of (Bindings.State -> unit)
    | OnDragStart of (Bindings.State -> unit)
    | OnDragEnd of (Bindings.State -> unit)
    | OnPinch of (Bindings.State -> unit)
    | OnPinchStart of (Bindings.State -> unit)
    | OnPinchEnd of (Bindings.State -> unit)
    | OnScroll of (Bindings.State -> unit)
    | OnScrollStart of (Bindings.State -> unit)
    | OnScrollEnd of (Bindings.State -> unit)
    | OnMove of (Bindings.State -> unit)
    | OnMoveStart of (Bindings.State -> unit)
    | OnMoveEnd of (Bindings.State -> unit)
    | OnWheel of (Bindings.State -> unit)
    | OnWheelStart of (Bindings.State -> unit)
    | OnWheelEnd of (Bindings.State -> unit)
    | OnHover of (Bindings.State -> unit)


let toObj props =
    match props with
    | None -> box {||}
    | Some x -> keyValueList CaseRules.LowerFirst x |> box

[<Emit("
Object.keys($0).map(x => {
    return [x, $0[x]];
});
")>]
let jsObjKeyValues obj: (string * obj)[] = jsNative

let toHTMLProps obj = jsObjKeyValues obj |> Seq.map (HTMLAttr.Custom >> unbox<IHTMLProp>)


type GestureHooks() =
    static member inline useGesture(gestures: Gesture seq, ?props: IConfigProp seq) = Bindings.Hooks.GestureHooks.useGesture (keyValueList CaseRules.LowerFirst gestures |> unbox) (toObj props |> unbox)
    static member inline useDrag(fn, ?props: IConfigProp seq)       = Bindings.Hooks.GestureHooks.useDrag fn (toObj props |> unbox)
    static member inline useMove(fn, ?props: IConfigProp seq)       = Bindings.Hooks.GestureHooks.useMove fn (toObj props |> unbox)
    static member inline useHover(fn, ?props: IConfigProp seq)      = Bindings.Hooks.GestureHooks.useHover fn (toObj props |> unbox)
    static member inline useScroll(fn, ?props: IConfigProp seq)     = Bindings.Hooks.GestureHooks.useScroll fn (toObj props |> unbox)
    static member inline useWheel(fn, ?props: IConfigProp seq)      = Bindings.Hooks.GestureHooks.useWheel fn (toObj props |> unbox)
    static member inline usePinch(fn, ?props: IConfigProp seq)      = Bindings.Hooks.GestureHooks.usePinch fn (toObj props |> unbox)
