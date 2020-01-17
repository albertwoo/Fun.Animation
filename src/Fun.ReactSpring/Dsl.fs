namespace Fun.ReactSpring

open Fable.Core.JsInterop
open Fable.React.Isomorphic

// Wrap a single animated value to an object to provide interpolate functions
type AnimatedValue(v) =
    member _.value = v
    member inline this.map(mapper: 'From -> 'To) =
        let mapper x = x |> unbox<'From> |> mapper
        isomorphicExec
            (fun () -> this.value?interpolate(Utils.mapJsArgs mapper) |> unbox<'To>)
            (fun () -> this.value |> mapper)
            ()

[<RequireQualifiedAccess>]
module Interpolation =
    // Map tupled animated values
    let inline map (mapper: 'From -> 'To) (props: 'From) =
        isomorphicExec
            (fun () ->
                Bindings.interpolateArr(
                    FSharp.Reflection.FSharpValue.GetTupleFields props,
                    Utils.mapJsArgs(fun x ->
                        FSharp.Reflection.FSharpValue.MakeTuple(x, typeof<'From>)
                        |> unbox<'From>
                        |> mapper
                    )
                )
            )
            (fun () -> props |> mapper)
            ()
    

type SpringHooks() =
    static member useSpring (props: SpringProp<'Item, 'Option> seq) =
        isomorphicExec
            (fun () ->
                props
                |> SpringProp<'Item, 'Option>.toObj
                |> Bindings.SpringHooks.useSpring |> unbox<'Option>
            )
            (fun () -> props |> SpringProp<'Item, 'Option>.getDefaultOption |> Option.get)
            ()
        
    static member useSpring (fn: unit -> SpringProp<'Item, 'Option> list) =
        isomorphicExec
            (fun () ->
                Bindings.SpringHooks.useSpringLazy (fun () -> fn() |> SpringProp<'Item, 'Option>.toObj)
                |> unbox<Bindings.ISpring<'Option>>
            )
            (fun () ->
                { new Bindings.ISpring<'Option> with
                    member _.current = fn() |> SpringProp<'Item, 'Option>.getDefaultOption |> Option.get
                    member _.update _ = ()
                    member _.stop () = () }
            )
            ()

    static member useSprings (num: int, props: (SpringProp<'Item, 'Option> list) list) =
        isomorphicExec
            (fun () ->
                props
                |> List.map SpringProp<'Item, 'Option>.toObj
                |> unbox
                |> Bindings.SpringHooks.useSprings num
                |> unbox<Bindings.ISprings<'Option>>
            )
            (fun () ->
                { new Bindings.ISprings<'Option> with
                    member _.current = props |> Seq.map (SpringProp<'Item, 'Option>.getDefaultOption >> Option.get) |> Seq.toArray
                    member _.update _ = ()
                    member _.stop () = () }
            )
            ()

    static member useSprings(num: int, fn: int -> SpringProp<'Item, 'Option> list) =
        isomorphicExec
            (fun () ->
                Bindings.SpringHooks.useSpringsLazy(num, fun k -> fn k |> SpringProp<'Item, 'Option>.toObj)
                |> unbox<Bindings.ISprings<'Option>>
            )
            (fun () ->
                { new Bindings.ISprings<'Option> with
                    member _.current = [| 0..num-1 |] |> Array.map (fn >> SpringProp<'Item, 'Option>.getDefaultOption >> Option.get)
                    member _.update _ = ()
                    member _.stop () = () }
            )
            ()

    static member useTrail(num: int, props: SpringProp<'Item, 'Option> list) =
        isomorphicExec
            (fun () ->
                props
                |> SpringProp<'Item, 'Option>.toObj
                |> Bindings.SpringHooks.useTrail num
                |> unbox<'Option>
            )
            (fun () -> props |> SpringProp<'Item, 'Option>.getDefaultOption |> Option.get)
            ()

    static member useTrail(num: int, fn: unit -> SpringProp<'Item, 'Option> list) =
        isomorphicExec
            (fun () ->
                Bindings.SpringHooks.useTrailLazy(num, fun () -> fn () |> SpringProp<'Item, 'Option>.toObj)
                |> unbox<Bindings.ITrail<'Option>>
            )
            (fun () ->
                { new Bindings.ITrail<'Option> with
                    member _.current =
                        let x = fn() |> SpringProp<'Item, 'Option>.getDefaultOption |> Option.get
                        [| 1..num |] |> Array.map (fun _ -> x)
                    member _.update _ = ()
                    member _.stop () = () }
            )
            ()

    static member useChain(springRefs: Fable.React.IRefHook<_>[], timeSteps: float[], ?timeFrame: int) =
        isomorphicExec
            (fun () -> Bindings.SpringHooks.useChain(unbox springRefs, unbox timeSteps, timeFrame |> Option.defaultValue 1000))
            (fun () -> ())
            ()

    static member useTransition(items: 'Item[], map: 'Item -> int, props: SpringProp<'Item, 'Option> list) =
        isomorphicExec
            (fun () ->
                Bindings.SpringHooks.useTransition(items, map, props |> SpringProp<'Item, 'Option>.toObj)
                |> unbox<Bindings.ITransition<'Item, 'Option>[]>
            )
            (fun () ->
                items
                |> Array.map (fun it ->
                    let x: Bindings.ITransition<'Item, 'Option> =
                        {
                            item = it
                            props = props |> SpringProp<'Item, 'Option>.getDefaultOption |> Option.get
                            key = map it
                        }
                    x
                )
            )
            ()
