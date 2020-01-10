module Client.App.Views

open Fable.Core
open Fable.Core.JsInterop
open Fable.Core.Reflection
open Fable.React
open Fable.React.Props
//type Demo =
//  | Targets of int
//  | Targets of string





let app state dispatch =
    div </> [
      Classes [
        Tw.``sm:w-full``
        Tw.``md:w-03/04``
        Tw.``lg:w-02/03``
        Tw.``mx-auto``
      ]
      Children [
        AnimeJsDemo.animeDiv()
      ]
    ]
