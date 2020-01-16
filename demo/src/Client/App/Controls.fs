module Client.App.Controls

open Fable.React
open Fable.React.Props


let landingPage =
    div </> [
        Style [
            Background "linear-gradient(to right, rgba(0, 0, 0, 0.1) 1px, transparent 1px) 0% 0% / 40px 40px, linear-gradient(rgba(0, 0, 0, 0.1) 1px, transparent 1px) rgb(54, 54, 69)"
        ]
        Classes [ Tw.``p-16``; Tw.``text-center`` ]
        Children [
            h1 </> [
                Classes [ Tw.``text-3xl``; Tw.``font-semibold``; Tw.``text-white`` ]
                Children [
                    span </> [ Text "react-spring for" ]
                    span </> [
                        Text " fable"
                        Classes [ Tw.``text-purple-400``; Tw.``font-bold`` ]
                    ]
                ]
            ]
            p </> [
                Text "bring your components to life with simple spring animation primitives"
                Classes [ Tw.``text-gray-400``; Tw.``mb-04`` ]
            ]
            a </> [
                Text ">> Check the react-spring official site"
                Href "https://www.react-spring.io/"
                Classes [ Tw.``text-purple-400``; Tw.``hover:text-purple-300`` ]
            ]
        ]
    ]


[<RequireQualifiedAccess>]
type DemoCardProp =
    | View of ReactElement
    | DescriptionView of ReactElement
    | ContainerCss of string list


let demoCard props =
    let view = props |> UnionProps.tryLast (function DemoCardProp.View x -> Some x | _ -> None) |> Option.defaultValue emptyView
    let description = props |> UnionProps.tryLast (function DemoCardProp.DescriptionView x -> Some x | _ -> None) |> Option.defaultValue emptyView
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
                    //Tw.``bg-gray-100``
                    //Tw.``shadow-md``
                    //Tw.``hover:shadow-lg``
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
                    view

                    div </> [
                        Classes [
                            Tw.``m-04``
                        ]
                        Children [
                           description
                        ]
                    ]
                ]
            ]
        ]
    ]
