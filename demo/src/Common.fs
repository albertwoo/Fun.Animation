[<AutoOpen>]
module Common

open Fable.Core.JsInterop
open Zanaptak.TypedCssClasses


let [<Literal>] private twSource = "./www/css/tailwind-generated.css"
let [<Literal>] private faSource = "./www/css/font-awesome-v5-10-2.min.css"


type Tw = CssClasses<twSource, Naming.Verbatim>
type Fa = CssClasses<faSource, Naming.Verbatim>

importSideEffects twSource
importSideEffects faSource


let toJsArray arr = arr |> Seq.map box |> Seq.toArray


let importAssetes () =
    ()
    