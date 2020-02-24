module Client.App.ReactSpringDemos.RenderPropsDemo_Transition

open System
open Fable.React
open Fable.React.Props
open Fun.ReactSpring


let render =
    FunctionComponent.Of(
        fun() ->
            let count = Hooks.useState 1
            let updateItems _ = count.update(fun c -> c + 1)
            Hooks.useEffectDisposable(
                fun () ->
                    let interval = Browser.Dom.window.setInterval(updateItems, 1000)
                    { new IDisposable with
                        member _.Dispose() =
                            Browser.Dom.window.clearInterval interval
                    }
                ,[||]
            )
            div </> [
                Children [
                    transition [
                        TransitionRenderProp.Items [|1..count.current%5|]
                        TransitionRenderProp.From {| transform = "translate3d(0,-20px,0)"; opacity = 0. |}
                        TransitionRenderProp.Enter {| transform = "translate3d(0,0px,0)"; opacity = 1. |}
                        TransitionRenderProp.Leave {| transform = "translate3d(0,20px,0)"; opacity = 0. |}
                        TransitionRenderProp.Children (Func<_, _, _>(fun item k ->
                            Func<_, _>(fun prop -> [
                                span </> [
                                    Key (string k)
                                    Style [
                                        Transform prop.transform
                                        Opacity prop.opacity
                                    ]
                                    Classes [ Tw.``text-2xl``; Tw.``font-bold``; Tw.``inline-block`` ]
                                    Text (string item)
                                ]
                            ])
                        ))
                    ]
                ]
            ]
    )
