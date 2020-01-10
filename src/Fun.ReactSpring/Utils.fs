module Fun.ReactSpring.Utils

open Fable.Core
open Fable.Core.JsInterop
open Fable.React


let animatedEles: obj = import "animated" "react-spring"


[<Emit("$0['$1']")>]
let generateTy obj ty: string = jsNative


let inline animatedEle ty props childs =
  let ty =
    #if FABLE_COMPILER
    generateTy animatedEles ty
    #else
    ty
    #endif
  domEl ty props childs
