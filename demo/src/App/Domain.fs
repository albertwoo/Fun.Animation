namespace rec Client.App

open System


type State =
  { ErrorInfo: string option
    IsLoading: bool }

type Msg =
  | OnError of string option
