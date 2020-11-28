module Client.App.ReactSpringDemos.RenderPropsDemo_Transition

open System
open Fable.React
open Fable.React.Props
open Feliz
open Fun.ReactSpring

type tranProp<'Item, 'Prop> = TransitionRenderProp<'Item, 'Prop> 


[<ReactComponent>]
let render () =
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
                tranProp.Items [|1..count.current%5|]
                tranProp.From  {| transform = "translate3d(0,-20px,0)"; opacity = 0. |}
                tranProp.Enter {| transform = "translate3d(0,0px,0)"; opacity = 1. |}
                tranProp.Leave {| transform = "translate3d(0,20px,0)"; opacity = 0. |}
                tranProp.ChildrenByFn (fun item k prop -> [
                    span </> [
                        Key (string k)
                        Style [
                            WillChange "transform,opacity"
                            Transform prop.transform
                            Opacity prop.opacity
                        ]
                        Classes [ Tw.``text-2xl``; Tw.``font-bold``; Tw.``inline-block`` ]
                        Text (string item)
                    ]
                ])
            ]
        ]
    ]
