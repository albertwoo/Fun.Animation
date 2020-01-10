namespace Fun.ReactSpring

open Fable.Core
open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props


type ISpringHook<'T> =
  [<Emit("$0[0]")>]
  abstract current: 'T
  [<Emit("$0[1]($1)")>]
  abstract update: 'T -> unit
  [<Emit("$0[2]()")>]
  abstract stop: unit -> unit

type ISpringHooks =
  abstract useSpring: 'Option -> obj

  [<Emit("$0.useSpring($1)")>]
  abstract useSpringLazy: (unit -> 'Option) -> ISpringHook<obj>

  [<Emit("$0.useTransition($1, $2, $3)")>]
  abstract useTransition: 'Item[] * ('Item -> 'Key) * 'Option -> obj []

[<AutoOpen>]
module Hooks =
  [<Import("*", "react-spring")>]
  let SpringHooks: ISpringHooks =
    { new ISpringHooks with
        member _.useSpring _ = obj()
        member _.useSpringLazy _ =
          { new ISpringHook<obj> with
              member _.current = obj()
              member _.update _ = ()
              member _.stop () = () }
        member _.useTransition (_, _, _) = [||] }

