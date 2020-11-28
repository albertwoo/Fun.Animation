module Client.Startup

open Elmish
open Elmish.React
open Elmish.HMR
open Fable.Core.JsInterop

importAll "@babel/polyfill"
importAll "core-js"

importAssetes()

Program.mkProgram App.States.init App.States.update App.Views.app
|> Program.withReactSynchronous "root"
|> Program.run