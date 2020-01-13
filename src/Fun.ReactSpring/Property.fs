namespace Fun.ReactSpring

open Fable.Core
open Fable.Core.JsInterop


type private Key = int


[<RequireQualifiedAccess>]
type Property<'T> =
    | From of 'T
    | To of 'T
    | [<CompiledName("to")>] ToByFn of (Key -> 'T)
    | [<CompiledName("to")>] ToByArray of 'T list
    | Delay of float
    | DelayByFn of (Key -> float)
    | Config of ConfigProp list
    | ConfigByFn of (Key -> ConfigProp list)
    | Reset of bool
    | Reverse of bool
    | OnStart of (Key -> unit)
    | OnRest of (unit -> unit)
    | OnFrame of (unit -> unit)
    | Custom of (string * obj)

    static member toObj props =
        props
        |> Seq.map (function
            | Property.Config x     -> Property.Custom ("config", keyValueList CaseRules.LowerFirst x)
            | Property.ConfigByFn f -> Property.Custom ("config", box(f >> keyValueList CaseRules.LowerFirst))
            | x -> x
        )
        |> keyValueList CaseRules.LowerFirst


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
