module Client.App.Controls

open Fable.React
open Fable.React.Props


[<RequireQualifiedAccess>]
type DemoCardProp =
    | View of ReactElement
    | Title of string
    | Description of string
    | ContainerCss of string list


let demoCard props =
    let view = props |> UnionProps.tryLast (function DemoCardProp.View x -> Some x | _ -> None) |> Option.defaultValue emptyView
    let title = props |> UnionProps.tryLast (function DemoCardProp.Title x -> Some x | _ -> None) |> Option.defaultValue ""
    let description = props |> UnionProps.tryLast (function DemoCardProp.Description x -> Some x | _ -> None) |> Option.defaultValue ""
    div </> [
        Classes [
            Tw.flex
            Tw.``items-center``
            Tw.``justify-center``
            yield! props |> UnionProps.concat (function DemoCardProp.ContainerCss x -> Some x | _ -> None)
        ]
        Children [
            div </> [
                Classes [
                    Tw.``overflow-hidden``
                    Tw.``w-03/04``
                    Tw.``mx-auto``
                    Tw.``p-10``
                    Tw.``m-10``
                    Tw.flex
                    Tw.``flex-col``
                    Tw.``flex-wrap``
                    Tw.``items-center``
                    Tw.``justify-center``
                    Tw.relative
                ]
                Children [
                    div </> [
                        Classes [
                            Tw.``m-04``
                            Tw.``text-center``
                        ]
                        Children [
                           h2 </> [
                               Text title
                               Classes [ Tw.``font-semibold`` ]
                           ]
                           p </> [
                               Text description
                               Classes [ Tw.``opacity-75`` ]
                           ]
                        ]
                    ]

                    view
                ]
            ]
        ]
    ]
