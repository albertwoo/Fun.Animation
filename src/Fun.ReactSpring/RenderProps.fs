[<AutoOpen>]
module Fun.ReactSpring.RenderProps

open Fable.Core
open Fable.Core.JsInterop
open Fable.React


[<RequireQualifiedAccess>]
type SpringRenderProp<'Option> =
    | From of 'Option
    | To of 'Option
    | After of 'Option
    | Delay of float
    | Immediate of bool
    | [<CompiledName("immediate")>] ImmediateByFn of (int -> bool)
    | Config of SpringConfig list
    | [<CompiledName("config")>] ConfigByFn of (SpringKey -> SpringConfig list)
    | Native of bool
    | Reset of bool
    | Reverse of bool
    | Force of bool
    | OnStart of (SpringKey -> unit)
    | OnRest of (unit -> unit)
    | OnFrame of (unit -> unit)
    | [<CompiledName("children")>] ChildrenByFn of ('Option -> ReactElement list)
    | Custom of (string * obj)
    
    static member toObj props =
        props
        |> Seq.map (function 
            | SpringRenderProp.Config x -> SpringRenderProp.Custom ("config", keyValueList CaseRules.LowerFirst x)
            | SpringRenderProp.ConfigByFn f -> SpringRenderProp.Custom ("config", box(f >> keyValueList CaseRules.LowerFirst))
            | x -> x
        )
        |> keyValueList CaseRules.LowerFirst


let inline spring<'Option> (props: SpringRenderProp<'Option> list) =
    ReactBindings.React.createElement(Bindings.Spring, props |> SpringRenderProp<'Option>.toObj, [])
