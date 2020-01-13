namespace Fun.ReactSpring

open Fable.Core
open Fable.Core.JsInterop


type private Key = int


[<RequireQualifiedAccess>]
type Property<'Item, 'Option> =
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
    | [<CompiledName("to")>] ToByFn of (Key -> 'Option)
    | [<CompiledName("to")>] ToByArray of 'Option list
    | Delay of float
    | DelayByFn of (Key -> float)
    | Config of ConfigProp list
    | ConfigByFn of (Key -> ConfigProp list)
    | Reverse of bool
    | OnStart of (Key -> unit)
    | OnRest of (unit -> unit)
    | OnFrame of (unit -> unit)
    | Custom of (string * obj)
    | Unique of bool
    | [<CompiledName("unique")>] UniqueByFn of ('Item -> bool)
    | Reset of bool
    | [<CompiledName("reset")>] ResetByFn of ('Item -> bool)
    | OnDestroyed of (unit -> unit)

    static member toObj props =
        props
        |> Seq.map (function 
            | Property.Config x -> Property.Custom ("config", keyValueList CaseRules.LowerFirst x)
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
