namespace Fun.ReactSpring

open Fable.Core
open Fable.Core.JsInterop


type SpringKey = int


[<RequireQualifiedAccess>]
type SpringConfig =
  | Mass of float
  | Tension of float
  | Friction of float
  | Clamp of bool
  | Precision of float
  | [<CompiledName("velocity")>] InitialVelocity of float
  | Duration of float
  | Easing of (float -> float)


[<RequireQualifiedAccess>]
module SpringConfigs =
    let Default =
        [
            SpringConfig.Mass 1.
            SpringConfig.Tension 170.
            SpringConfig.Friction 26.
        ]

    let Gentle =
        [
            SpringConfig.Mass 1.
            SpringConfig.Tension 120.
            SpringConfig.Friction 14.
        ]

    let Wobbly =
        [
            SpringConfig.Mass 1.
            SpringConfig.Tension 180.
            SpringConfig.Friction 12.
        ]

    let Stiff =
        [
            SpringConfig.Mass 1.
            SpringConfig.Tension 210.
            SpringConfig.Friction 20.
        ]

    let Slow =
        [
            SpringConfig.Mass 1.
            SpringConfig.Tension 280.
            SpringConfig.Friction 60.
        ]

    let Molasses =
        [
            SpringConfig.Mass 1.
            SpringConfig.Tension 280.
            SpringConfig.Friction 120.
        ]


[<RequireQualifiedAccess>]
type SpringProp<'Item, 'Option> =
    | Ref of obj
    | Initial of 'Option
    | [<CompiledName("initial")>] InitialByFn of ('Item -> 'Option)
    | From of 'Option
    | [<CompiledName("from")>] FromByFn of ('Item -> 'Option)
    | Enter of 'Option
    | [<CompiledName("enter")>] EnterByFn of ('Item -> 'Option)
    | Update of 'Option
    | [<CompiledName("update")>] UpdateByFn of ('Item -> 'Option)
    | Leave of 'Option
    | [<CompiledName("leave")>] LeaveByFn of ('Item -> 'Option)
    | Trail of int
    | To of 'Option
    | [<CompiledName("to")>] ToByFn of (SpringKey -> 'Option)
    | [<CompiledName("to")>] ToByArray of 'Option list
    | Delay of float
    | [<CompiledName("delay")>] DelayByFn of (SpringKey -> float)
    | Config of SpringConfig list
    | [<CompiledName("config")>] ConfigByFn of (SpringKey -> SpringConfig list)
    | Reverse of bool
    | OnStart of (SpringKey -> unit)
    | OnRest of (unit -> unit)
    | OnFrame of (unit -> unit)
    | [<Erase>] Custom of (string * obj)
    | Unique of bool
    | [<CompiledName("unique")>] UniqueByFn of ('Item -> bool)
    | Reset of bool
    | [<CompiledName("reset")>] ResetByFn of ('Item -> bool)
    | OnDestroyed of (unit -> unit)

    static member toObj props =
        props
        |> Seq.map (function 
            | SpringProp.Config x -> SpringProp.Custom ("config", keyValueList CaseRules.LowerFirst x)
            | SpringProp.ConfigByFn f -> SpringProp.Custom ("config", box(f >> keyValueList CaseRules.LowerFirst))
            | x -> x
        )
        |> keyValueList CaseRules.LowerFirst

    static member getDefaultOption props =
        props
        |> Seq.tryPick (function
            | SpringProp.Initial x
            | SpringProp.From x
            | SpringProp.To x
            | SpringProp.Enter x -> Some x
            | _ -> None)


[<RequireQualifiedAccess; StringEnum>]
type ExtrapolateProp =
    | Identity
    | Clamp
    | Extend

[<RequireQualifiedAccess>]
type InterpolationProp<'T> =
    | ExtrapolateLeft of ExtrapolateProp
    | ExtrapolateRight of ExtrapolateProp
    | Extrapolate of ExtrapolateProp
    | Range of float list
    | Output of float list
    | Map of ('T -> 'T)
