module Fun.AnimeJs

open Fable.Core
open Fable.Core.JsInterop
open Fable.Core.Reflection
open Fable.React
open Fable.React.Props


[<Emit("""
let obj = $0;
let keys = Object.keys(obj);
let arrays = [];
for (var i=0; i<keys.length; i++) {
  arrays[i] = [ keys[i], obj[keys[i]] ];
}
return arrays;
""")>]
let getObjKeyValues (obj: obj):  (string * obj) list = jsNative

type AnimeProp() =
  static member targets(css: string) = "targets" ==> css
  static member targets(nodes: Browser.Types.Element []) = "targets" ==> nodes
  static member targets(obj: obj) = "targets" ==> obj
  static member targets(objs: obj []) = "targets" ==> objs

  static member cssProps(cssprops: CSSProp list) =
    cssprops
    |> keyValueList CaseRules.LowerFirst
    |> getObjKeyValues

  static member cssProp(prop: CSSProp) = [ prop ] |> AnimeProp.cssProps |> Seq.head

  static member domAttrs(attrs: IHTMLProp list) =
    attrs
    |> keyValueList CaseRules.LowerFirst
    |> getObjKeyValues

  static member domAttr(attr) = [attr] |> AnimeProp.domAttrs |> Seq.head

  static member svgAttrs (attrs: SVGAttr list) =
    attrs
    |> keyValueList CaseRules.LowerFirst
    |> getObjKeyValues
  static member svgAttr(attr) = [attr] |> AnimeProp.svgAttrs |> Seq.head

  static member loop(x: int) = "loop" ==> x

  static member delay(x: int) = "delay" ==> x
  static member delay(f: Browser.Types.Element -> unit) = "delay" ==> f
  static member delay(f: Browser.Types.Element -> int -> unit) = "delay" ==> f
  static member delay(f: Browser.Types.Element -> int -> int -> unit) = "delay" ==> f

  static member direction(dir: string) = "direction" ==> dir

module Anime =

  let animejs obj: unit = importDefault "animejs"

  let anime config = config |> createObj |> animejs


  let inline anime2<'T> (config: 'T) =
    let rec recordToObj ty obj =
      if Microsoft.FSharp.Reflection.FSharpType.IsRecord(ty) then
        Microsoft.FSharp.Reflection.FSharpType.GetRecordFields(ty)
        |> Seq.fold
          (fun state propInfo ->
            let v = recordToObj propInfo.PropertyType (Microsoft.FSharp.Reflection.FSharpValue.GetRecordField(obj, propInfo))
            match state with
            | [] -> [ propInfo.Name, v ]
            | _  -> [ propInfo.Name, v ]@state
          )
          []
        |> createObj
      else
        box obj
    recordToObj typeof<'T> config |> animejs