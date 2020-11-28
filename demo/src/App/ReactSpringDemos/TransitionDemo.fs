module Client.App.ReactSpringDemos.TransitionDemo

open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props
open Feliz
open Fun.ReactSpring
open Fun.ReactGesture


[<ReactComponent>]
let main () =
    let source = Hooks.useState 0

    let transitions =
        SpringHooks.useTransition(
            [| source.current |],
            id,
            [
                SpringProp.From  {| opacity = 0; transform = "translate3d(100%,0,0)" |}
                SpringProp.Enter {| opacity = 1; transform = "translate3d(0%,0,0)" |}
                SpringProp.Leave {| opacity = 0; transform = "translate3d(-50%,0,0)" |}
            ]
        )

    let dragAttrs =
        GestureHooks.useWheel (fun state ->
            if state.delta.y > 10. then source.update((source.current + 1) % 3)
            elif state.delta.y < -10. then source.update((source.current - 1) % 3)
        )

    div </> [
        OnClick (fun _ -> source.update((source.current + 1) % 3))
        OnMouseEnter (fun _ -> Browser.Dom.document?body?style?overflow <- "hidden")
        OnMouseLeave (fun _ -> Browser.Dom.document?body?style?overflow <- "auto")
        Classes [ Tw.``overflow-hidden``; Tw.relative ]
        Style [ Height 200; Width 300 ]
        yield! toHTMLProps(dragAttrs.bind 0)
        Children (
            transitions
            |> Seq.mapi (fun index data ->
                Animated.div </> [
                    Key (string data.key)
                    Classes [
                        Tw.absolute
                        Tw.``cursor-pointer``
                        Tw.``text-6xl``
                        Tw.``text-center``
                        Tw.``text-white``
                        Tw.``select-none``
                        Tw.rounded
                        if data.item = 0 then
                            Tw.``bg-pink-400``
                        elif data.item = 1 then
                            Tw.``bg-green-400``
                        else
                            Tw.``bg-purple-400``
                    ]
                    Style [
                        Width 300
                        Height 200
                        LineHeight "200px"
                        TextShadow "0px 2px 40px #00000020, 0px 2px 5px #00000030"
                        WillChange "transform, opacity"
                        Opacity data.props.opacity
                        Transform data.props.transform
                    ]
                    HTMLPropExtra.Text (string data.item)
                ]
            )
            |> Seq.toList
        )
    ]
