[<RequireQualifiedAccess>]
module Fun.ReactSpring.Animated

open Fable.Core
open Fable.Core.JsInterop
open Fable.React


let animated: obj = import "animated" "react-spring"

[<Emit("$0['$1']")>]
let generateTy obj ty: string = jsNative


let inline forEle ty props childs =
  let ty =
    #if FABLE_COMPILER
    generateTy animated ty
    #else
    ty
    #endif
  domEl ty props childs
