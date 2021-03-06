module Client.App.ReactSpringDemos.ChainDemo

open Fable.React
open Fable.React.Props
open Feliz
open Fun.ReactSpring


[<ReactComponent>]
let main () =
    let isOpen = Hooks.useState false

    let springRef = Hooks.useRef null
    let spring =
        SpringHooks.useSpring [
            SpringProp.Ref springRef
            SpringProp.Config SpringConfigs.Stiff
            SpringProp.From {| size = "20%"; background = "hotpink" |}
            SpringProp.To 
                {|
                    size = if isOpen.current then "100%" else "20%"
                    background = if isOpen.current then "white"  else "hotpink"
                |}
        ]

    let transRef = Hooks.useRef null
    let trans =
        SpringHooks.useTransition(
            (if isOpen.current then [|1..12|] else [||]),
            id,
            [
                SpringProp.Ref transRef
                SpringProp.Unique true
                SpringProp.Trail (400 / 12)
                SpringProp.From  {| opacity = 0; transform = "scale(0)" |}
                SpringProp.Enter {| opacity = 1; transform = "scale(1)" |}
                SpringProp.Leave {| opacity = 0; transform = "scale(0)" |}
            ]
        )

    SpringHooks.useChain(
        (if isOpen.current then [|springRef; transRef|] else [|transRef; springRef|]),
        [|0.; if isOpen.current then 0.1 else 0.5|]
    )

    div </> [
        Classes [
            Tw.``w-full``
            Tw.``h-64``
            Tw.``overflow-hidden``
            Tw.``select-none``
            Tw.flex
            Tw.``items-center``
            Tw.``justify-center``
        ]
        Children [
            Animated.div </> [
                Style [
                    Position PositionOptions.Relative
                    Display DisplayOptions.Grid
                    GridTemplateColumns "repeat(4, minmax(100px, 1fr))"
                    GridGap "25px"
                    Padding "25px"
                    BorderRadius "5px"
                    Cursor "pointer"
                    BoxShadow "0px 10px 10px -5px rgba(0, 0, 0, 0.05)"
                    WillChange "width, height"
                    Width spring.size
                    Height spring.size
                    BackgroundColor spring.background
                ]
                OnClick (fun _ -> isOpen.update(not isOpen.current))
                Children (
                    trans
                    |> Seq.mapi (fun i data ->
                        Animated.div </> [
                            Key (string i)
                            Style [
                                Width "100%"
                                Height "100%"
                                BackgroundColor "white"
                                BorderRadius "5px"
                                WillChange "transform, opacity"
                                Opacity data.props.opacity
                                Transform data.props.transform
                                BackgroundColor (sprintf "rgb(%d,%d,%d)" (100 + data.item * 10) (200 + data.item * 10) (150 + data.item * 10))
                            ]
                        ] 
                    )
                    |> Seq.toList
                )
            ]
        ]
    ]
