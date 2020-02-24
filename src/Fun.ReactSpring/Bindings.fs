module Fun.ReactSpring.Bindings

open Fable.Core
open Fable.Core.JsInterop
open Fable.React


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
    abstract update: (SpringKey -> 'T) -> unit
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


let useSpring option = import "useSpring" "react-spring"

let useSpringLazy (fn: unit -> 'Option): ISpring<_> = import "useSpring" "react-spring"

let useSprings (num: int) (options: 'Option []) = import "useSprings" "react-spring"

let useSpringsLazy (num: int) (fn: int -> 'Option): ISprings<_> = import "useSprings" "react-spring"

let useTrail (num: int) option = import "useTrail" "react-spring"

let useTrailLazy (num: int) (fn: unit -> 'Option): ITrail<_> = import "useTrail" "react-spring"

let useChain (refs: obj[]) (timeSteps: float []) (timeFrame: int): unit = import "useChain" "react-spring"

let useTransition (items: 'Item[]) (map: 'Item -> 'Key) option: ITransition<'Item, 'Option> [] = import "useTransition" "react-spring"


let Spring: ReactElement = import "Spring" "react-spring/renderprops"
let Trail: ReactElement = import "Trail" "react-spring/renderprops"


let animatedEle: obj = import "animated" "react-spring"


let interpolate props map = import "interpolate" "react-spring"
