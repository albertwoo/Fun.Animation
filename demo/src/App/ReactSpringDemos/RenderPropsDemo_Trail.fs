module Client.App.ReactSpringDemos.RenderPropsDemo_Trail

open Fable.React
open Fable.React.Props
open Feliz
open Fun.ReactSpring

type trailProp<'Item, 'Option> = TrailRenderProp<'Item, 'Option>


[<ReactComponent>]
let render () =
    let toggle = Hooks.useState false
    let colors = [| Tw.``bg-purple-200``; Tw.``bg-purple-400``; Tw.``bg-purple-600``; Tw.``bg-purple-800`` |]
    let toState =
        {| opacity = if toggle.current then 1. else 0.5
           x = if toggle.current then 0 else 100 |}
    div </> [
        Classes [
            Tw.``h-10``; Tw.``text-center``; Tw.``text-green-600``; Tw.``mt-04``
            Tw.relative; Tw.``w-full``
        ]
        Style [ Height "200px" ]
        Children [
            trail [
                trailProp.Items colors
                trailProp.From {| opacity = 0.; x = -100 |}
                trailProp.To toState
                trailProp.ChildrenByFn (fun item k prop -> [
                    div </> [
                        Key (string k)
                        OnClick (fun _ -> toggle.update (not toggle.current))
                        Classes [ item; Tw.relative; Tw.``w-01/02``; Tw.``cursor-pointer`` ]
                        Style [
                            WillChange "transform,opacity"
                            Opacity prop.opacity
                            Transform (sprintf "translate3d(%d%%,0,0)" prop.x)
                            Height "20%"
                        ]
                    ]
                ])
            ]
        ]
    ]
