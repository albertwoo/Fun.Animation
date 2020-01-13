module Fun.ReactSpring.Bindings

open Fable.Core
open Fable.Core.JsInterop


type ISpring<'T> =
    [<Emit("$0[0]")>]
    abstract current: 'T
    [<Emit("$0[1]($1)")>]
    abstract update: 'T -> unit
    [<Emit("$0[2]()")>]
    abstract stop: unit -> unit
    
type ISprings<'T> =
    [<Emit("$0[0]")>]
    abstract current: 'T []
    [<Emit("$0[1]($1)")>]
    abstract update: (Key -> 'T) -> unit
    [<Emit("$0[2]()")>]
    abstract stop: unit -> unit


type ITrail<'T> =
    [<Emit("$0[0]")>]
    abstract current: 'T[]
    [<Emit("$0[1]($1)")>]
    abstract update: 'T -> unit
    [<Emit("$0[2]()")>]
    abstract stop: unit -> unit   

type ITransition<'Item, 'Option> =
    { item: 'Item
      props: 'Option
      key: int }


type ISpringHooks =
    abstract useSpring: 'Option -> obj

    [<Emit("$0.useSpring($1)")>]
    abstract useSpringLazy: (unit -> 'Option) -> ISpring<obj>

    abstract useSprings: int -> 'Option [] -> obj

    [<Emit("$0.useSprings($1, $2)")>]
    abstract useSpringsLazy: int * (int -> 'Option) -> ISprings<obj>

    abstract useTrail: int -> 'Option -> obj

    [<Emit("$0.useTrail($1, $2)")>]
    abstract useTrailLazy: int * (unit -> 'Option) -> ITrail<obj>

    abstract useChain: obj[] * obj [] -> unit

    [<Emit("$0.useTransition($1, $2, $3)")>]
    abstract useTransition: 'Item[] * ('Item -> 'Key) * 'Option -> ITransition<'Item, 'Option> []


[<RequireQualifiedAccess>]
module DummyData =
    let singleValue<'T> initValue =
        {
            new ISpring<'T> with
                member _.current = initValue
                member _.update _ = ()
                member _.stop () = ()
        }

    let mutipleValues<'T> =
        {
            new ISprings<obj> with
                member _.current = [||] |> unbox
                member _.update _ = ()
                member _.stop () = ()
        }

    let trail =
        {
            new ITrail<obj> with
                member _.current = [||] |> unbox
                member _.update _ = ()
                member _.stop () = ()
        }


[<Import("*", "react-spring")>]
let SpringHooks: ISpringHooks =
    {
        new ISpringHooks with
            member _.useSpring _ = obj()
            member _.useSpringLazy _ = DummyData.singleValue (obj())
            member _.useSprings _ _ = obj()
            member _.useSpringsLazy (_, _) = DummyData.mutipleValues 
            member _.useTrail _ _ = obj()
            member _.useTrailLazy (_, _) = DummyData.trail
            member _.useChain (_, _) = ()
            member _.useTransition (_, _, _) = [||]
    }


let inline interpolate (prop, map) =
    prop?interpolate(map)
