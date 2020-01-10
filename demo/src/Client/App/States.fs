module Client.App.States

open Elmish

let init() =
  { ErrorInfo = None
    IsLoading = false }
  , Cmd.none


let update msg state =
    match msg with
    | OnError e -> { state with ErrorInfo = e; IsLoading = false }, Cmd.none
