module Client.App.ReactSpringDemos.SpringDemo

open Fable.React
open Fable.React.Props
open Fun.ReactSpring


let main =
    FunctionComponent.Of(
        fun () ->
            let spring =
                SpringHooks.useSpring(fun () -> [
                    SpringProp.To {| xys = [| 0.; 0.; 1. |] |}
                    SpringProp.Config SpringConfigs.Slow
                ])

            let calc x y =
                [|
                    -(y - Browser.Dom.window.innerHeight / 2.) / 20.
                    (x - Browser.Dom.window.innerWidth / 2.) / 20.
                    1.1
                |]
        
            div </> [
                Classes [ Tw.flex; Tw.``items-center``; Tw.``justify-center`` ]
                Children [
                    Animated.div </> [
                        Classes [ Tw.``w-64``; Tw.``h-64``; Tw.rounded; Tw.``shadow-md``; Tw.``hover:shadow-lg``; Tw.``bg-green-300`` ]
                        Style [
                            WillChange "transform"
                            Transform (AnimatedValue(spring.current.xys).map (fun (x, y, s) ->
                                sprintf "perspective(600px) rotateX(%ddeg) rotateY(%ddeg) scale(%A)" x y s
                            ))
                        ]
                        OnMouseMove (fun x ->
                            spring.update {| xys = calc x.clientX x.clientY |}
                        )
                        OnMouseLeave (fun _ ->
                            spring.update {| xys = [| 0.; 0.; 1. |] |}
                        )
                    ]
                ]
            ]
        )
