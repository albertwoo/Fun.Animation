namespace Fun.ReactSpring

open Fable.Core.JsInterop


type AnimatedValue(v) =
    member inline _.map(map: 'From -> 'To) =
        v?interpolate(Utils.mapJsArgs(fun x ->
            FSharp.Reflection.FSharpValue.MakeTuple(x, typeof<'From>)
            |> unbox<'From>
            |> map
        ))


[<RequireQualifiedAccess>]
module Interpolation =
    let inline map (mapper: 'From -> 'To) (props: obj[]) =
        Bindings.interpolateArr(
            props,
            Utils.mapJsArgs(fun x ->
                FSharp.Reflection.FSharpValue.MakeTuple(x, typeof<'From>)
                |> unbox<'From>
                |> mapper
            )
        )
    

type SpringHooks() =
    static member inline useSpring (props: Property<'Item, 'Option> seq) =
        props
        |> Property<'Item, 'Option>.toObj
        |> Bindings.SpringHooks.useSpring |> unbox<'Option>
        
    static member inline useSpring (fn: unit -> Property<'Item, 'Option> list) =
        Bindings.SpringHooks.useSpringLazy (fun () -> fn() |> Property<'Item, 'Option>.toObj)
        |> unbox<Bindings.ISpring<'Option>>

    static member inline useSprings (num: int, props: (Property<'Item, 'Option> list) list) =
        props
        |> List.map Property<'Item, 'Option>.toObj
        |> unbox
        |> Bindings.SpringHooks.useSprings num
        |> unbox<Bindings.ISprings<'Option>>

    static member inline useSprings(num: int, fn: int -> Property<'Item, 'Option> list) =
        Bindings.SpringHooks.useSpringsLazy(num, fun k -> fn k |> Property<'Item, 'Option>.toObj)
        |> unbox<Bindings.ISprings<'Option>>

    static member inline useTrail(num: int, props: Property<'Item, 'Option> list) =
        props
        |> Property<'Item, 'Option>.toObj
        |> Bindings.SpringHooks.useTrail num
        |> unbox<'Option>

    static member inline useTrail(num: int, fn: unit -> Property<'Item, 'Option> list) =
        Bindings.SpringHooks.useTrailLazy(num, fun () -> fn () |> Property<'Item, 'Option>.toObj)
        |> unbox<Bindings.ITrail<'Option>>

    static member inline useChain(springRefs: Fable.React.IRefHook<_>[], timeSteps: float[], ?timeFrame: int) =
        Bindings.SpringHooks.useChain(unbox springRefs, unbox timeSteps, timeFrame |> Option.defaultValue 1000)

    static member inline useTransition(items: 'Item[], map: 'Item -> 'Key, props: Property<'Item, 'Option> list) =
        Bindings.SpringHooks.useTransition(items, map, props |> Property<'Item, 'Option>.toObj)
        |> unbox<Bindings.ITransition<'Item, 'Option>[]>
