module Client.App.ReactSpringDemos.TransitionDemo

open Fable.React
open Fable.React.Props
open Fun.ReactSpring


let main =
    FunctionComponent.Of (
      fun () ->
        let source = Hooks.useState 0
        let transitions =
            SpringHooks.useTransition(
                [| source.current |],
                id,
                [
                    Property.From  {| opacity = 0; transform = "translate3d(100%,0,0)" |}
                    Property.Enter {| opacity = 1; transform = "translate3d(0%,0,0)" |}
                    Property.Leave {| opacity = 0; transform = "translate3d(-50%,0,0)" |}
                ]
            )

        div </> [
            OnClick (fun _ -> source.update((source.current + 1) % 3))
            Classes [ Tw.``overflow-hidden``; Tw.flex; Tw.``items-center``; Tw.``justify-center`` ]
            Children (
                transitions
                |> Seq.map (fun data ->
                    Animated.div </> [
                        Key (string data.key)
                        Classes [
                            Tw.``cursor-pointer``
                            Tw.absolute
                            Tw.``w-64``
                            Tw.``h-64``
                            Tw.``text-6xl``
                            Tw.``text-center``
                            Tw.``text-white``
                            Tw.``select-none``
                            Tw.flex
                            Tw.rounded
                            Tw.``items-center``
                            Tw.``justify-center``
                            if data.item = 0 then
                                Tw.``bg-pink-400``
                            elif data.item = 1 then
                                Tw.``bg-green-400``
                            else
                                Tw.``bg-purple-400``
                        ]
                        Style [
                            TextShadow "0px 2px 40px #00000020, 0px 2px 5px #00000030"
                            WillChange "transform, opacity"
                            Opacity data.props.opacity
                            Transform data.props.transform
                        ]
                        Text (string data.item)
                    ]
                )
                |> Seq.toList
            )
        ]
    )