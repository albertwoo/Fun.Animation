[<AutoOpen>]
module Fun.ReactSpring.RenderProps

open Fable.Core
open Fable.Core.JsInterop
open Fable.React
open Fable.React.Isomorphic


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
    | [<Erase>] Custom of (string * obj)
    
    static member toObj props =
        props
        |> Seq.map (function 
            | SpringRenderProp.Config x -> SpringRenderProp.Custom ("config", keyValueList CaseRules.LowerFirst x)
            | SpringRenderProp.ConfigByFn f -> SpringRenderProp.Custom ("config", box(f >> keyValueList CaseRules.LowerFirst))
            | x -> x
        )
        |> keyValueList CaseRules.LowerFirst
    
    static member getDefaultOption props =
        props
        |> Seq.tryPick (function
            | SpringRenderProp.From x
            | SpringRenderProp.To x
            | SpringRenderProp.After x -> Some x
            | _ -> None)


[<RequireQualifiedAccess>]
type TrailRenderProp<'Item, 'Option> =
    | From of 'Option
    | To of 'Option
    | Keys of obj[]
    | [<CompiledName("keys")>] KeyByFn of ('Item -> int)
    | Items of 'Item[]
    | ChildrenByFn of ('Item -> SpringKey -> 'Option -> ReactElement list)
    | Reverse of bool
    | [<Erase>] Custom of string * obj

    static member toObj props =
        props
        |> Seq.map (function
            | TrailRenderProp.ChildrenByFn f -> TrailRenderProp.Custom("children", Utils.mapJsArgs (fun (args: obj[]) -> fun prop -> f (args.[0] |> unbox<'Item>) (args.[1] |> unbox) prop))
            | x -> x
        )
        |> keyValueList CaseRules.LowerFirst


[<RequireQualifiedAccess>]
type TransitionRenderProp<'Item, 'Option> =
    | Items of 'Item[]
    | ChildrenByFn of ('Item -> SpringKey -> 'Option -> ReactElement list)
    | Keys of obj[]
    | OnDestroyed of (unit -> unit)
    | Config of SpringConfig list
    | [<CompiledName("config")>] ConfigByFn of ('Item -> SpringKey -> 'Option -> SpringConfig list)
    | Reset of bool
    | Unique of bool
    // Trailing delay in ms
    | Trail of float
    | Update of 'Option
    | [<CompiledName("update")>] UpdateByFn of ('Item -> 'Option)
    | Leave of 'Option
    | [<CompiledName("leave")>] LeaveByFn of ('Item -> 'Option)
    | Enter of 'Option
    | [<CompiledName("enter")>] EnterByFn of ('Item -> 'Option)
    | From of 'Option
    | [<CompiledName("from")>] FromByFn of ('Item -> 'Option)
    | Initial of 'Option
    | [<CompiledName("initial")>] InitialByFn of ('Item -> 'Option)
    | [<Erase>] Custom of string * obj
    
    static member toObj props =
        props
        |> Seq.map (function 
            | TransitionRenderProp.Config x -> TransitionRenderProp.Custom ("config", keyValueList CaseRules.LowerFirst x)
            | TransitionRenderProp.ConfigByFn f -> TransitionRenderProp.Custom ("config", Utils.mapJsArgs (fun (args: obj[]) -> fun prop -> f (args.[0] |> unbox<'Item>) (args.[1] |> unbox) prop |> keyValueList CaseRules.LowerFirst))
            | TransitionRenderProp.ChildrenByFn f -> TransitionRenderProp.Custom("children", Utils.mapJsArgs (fun (args: obj[]) -> fun prop -> f (args.[0] |> unbox<'Item>) (args.[1] |> unbox) prop))
            | x -> x
        )
        |> keyValueList CaseRules.LowerFirst
        


let inline spring<'Option> (props: SpringRenderProp<'Option> list) =
    isomorphicExec
        (fun () -> ReactBindings.React.createElement(Bindings.Spring, props |> SpringRenderProp<'Option>.toObj, []))
        (fun () ->
            props
            |> SpringRenderProp<'Option>.getDefaultOption
            |> Option.bind (fun op ->
                props
                |> List.choose (function SpringRenderProp.ChildrenByFn x -> Some x | _ -> None)
                |> List.tryLast
                |> Option.map (fun f -> fragment [] (f op))
            )
            |> Option.defaultValue (str "spring place holder")
        )
        ()


let inline trail<'Item, 'Option> (props: TrailRenderProp<'Item, 'Option> list) =
    isomorphicExec
        (fun () -> ReactBindings.React.createElement(Bindings.Trail, props |> TrailRenderProp<'Item, 'Option>.toObj, []))
        (fun () ->
            props
            |> List.choose (function TrailRenderProp.Items x -> Some x | _ -> None)
            |> List.tryLast
            |> Option.bind Seq.tryHead
            |> Option.bind (fun item ->
                props
                |> List.choose (function TrailRenderProp.ChildrenByFn x -> Some x | _ -> None)
                |> List.tryLast
                |> Option.map (fun f ->
                    fragment [] (
                        props
                        |> Seq.tryPick (function
                            | TrailRenderProp.From x -> Some x
                            | TrailRenderProp.To x -> Some x
                            | _ -> None
                        )
                        |> Option.map (fun prop -> f item 0 prop)
                        |> Option.defaultValue []
                    )
                )
            )
            |> Option.defaultValue (str "trail place holder")
        )
        ()


let inline transition<'Item, 'Option> (props: TransitionRenderProp<'Item, 'Option> list) =
    isomorphicExec
        (fun () -> ReactBindings.React.createElement(Bindings.Transition, props |> TransitionRenderProp<'Item, 'Option>.toObj, []))
        (fun () ->
            props
            |> List.choose (function TransitionRenderProp.Items x -> Some x | _ -> None)
            |> List.tryLast
            |> Option.bind Seq.tryHead
            |> Option.bind (fun item ->
                props
                |> List.choose (function TransitionRenderProp.ChildrenByFn x -> Some x | _ -> None)
                |> List.tryLast
                |> Option.map (fun f ->
                    fragment [] (
                        props
                        |> Seq.tryPick (function
                            | TransitionRenderProp.From x -> Some x
                            | TransitionRenderProp.FromByFn f -> Some(f item)
                            | TransitionRenderProp.Enter x -> Some x
                            | TransitionRenderProp.EnterByFn f -> Some(f item)
                            | _ -> None
                        )
                        |> Option.map (fun prop -> f item 0 prop)
                        |> Option.defaultValue []
                    )
                )
            )
            |> Option.defaultValue (str "transition place holder")
        )
        ()
