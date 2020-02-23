module Client.App.ReactSpringDemos.RenderPropsDemo_Spring

open Fable.React
open Fable.React.Props
open Fun.ReactSpring


let render () =
    div </> [
        Classes [ Tw.``h-10``; Tw.``text-center``; Tw.``text-green-600``; Tw.``font-bold``; Tw.``my-04`` ]
        Children [
            spring [
                SpringRenderProp.From {| opacity  = 0. |}
                SpringRenderProp.To {| opacity  = 1. |}
                SpringRenderProp.Delay 2000.
                SpringRenderProp.ChildrenByFn (fun op -> [
                    div </> [
                        Key "1"
                        Style [ Opacity op.opacity ]
                        Text "Spring Hello"
                    ]
                    div </> [
                        Key "2"
                        Style [ Opacity op.opacity ]
                        Text "Spring Hello"
                    ]
                ])
            ]

            svg [
                SVGAttr.Width "180"
                ViewBox "0 0 23 23"
                classes [ Tw.``mx-auto``; Tw.``my-04`` ]
            ] [
                g [
                   SVGAttr.Fill "#373737"
                   SVGAttr.Stroke "green"
                   SVGAttr.StrokeWidth "0.5"
                ] [
                    spring [
                        SpringRenderProp.From {| dash = 0. |}
                        SpringRenderProp.To {| dash = 30. |}
                        SpringRenderProp.Delay 3000.
                        SpringRenderProp.ChildrenByFn (fun op -> [
                            path [
                                Key "START-PATH"
                                SVGAttr.StrokeDasharray "1"
                                SVGAttr.Custom("strokeDashoffset", op.dash)
                                SVGAttr.D "M12 17.27L18.18 21l-1.64-7.03L22 9.24l-7.19-.61L12 2 9.19 8.63 2 9.24l5.46 4.73L5.82 21z"
                            ] []
                        ])
                    ]
                ]
            ]
        ]
    ]
