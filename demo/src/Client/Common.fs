[<AutoOpen>]
module Common

open Zanaptak.TypedCssClasses

type Tw = CssClasses<"./public/css/tailwind-generated.css", Naming.Verbatim>
type Fa = CssClasses<"./public/css/font-awesome-v5-10-2.min.css", Naming.Verbatim>


let toJsArray arr = arr |> Seq.map box |> Seq.toArray
