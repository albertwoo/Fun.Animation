module Client.App.ReactSpringDemos.RenderPropsDemo_Trail

open System
open Fable.React
open Fable.React.Props
open Fun.ReactSpring


let render =
    FunctionComponent.Of(
        fun () ->
            let toggle = Hooks.useState false

            div </> [
                Classes [
                    Tw.``h-10``; Tw.``text-center``; Tw.``text-green-600``; Tw.``mt-04``
                    Tw.relative; Tw.``w-full``
                ]
                Style [ Height "200px" ]
                Children [
                    trail [
                        TrailRenderProp.Items [| Tw.``bg-purple-200``; Tw.``bg-purple-400``; Tw.``bg-purple-600``; Tw.``bg-purple-800`` |]
                        TrailRenderProp.From {| opacity = 0.; x = -100 |}
                        TrailRenderProp.To {|
                                                opacity = if toggle.current then 1. else 0.25
                                                x = if toggle.current then 0 else 100
                                           |}
                        TrailRenderProp.ChildrenByFn (Func<_, _, _>(fun item k ->
                            Func<_, _>(fun prop ->
                                [
                                    div </> [
                                        OnClick (fun _ -> toggle.update (not toggle.current))
                                        Classes [ item ]
                                        Style [
                                            Opacity prop.opacity
                                            Transform (sprintf "translate3d(%d%%,0,0)" prop.x)
                                            Height "20%"
                                        ]
                                        Classes [ Tw.relative; Tw.``w-01/02``; Tw.``cursor-pointer`` ]
                                    ]
                                ])
                            )
                        )
                    ]
                ]
            ]
    )