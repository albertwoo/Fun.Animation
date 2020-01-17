module Client.App.ReactSpringDemos.TrailDemo

open Fable.React
open Fable.React.Props
open Fun.ReactSpring


let main =
  FunctionComponent.Of (
    fun () ->
        let trail =
            SpringHooks.useTrail(
                3,
                fun () -> [
                    SpringProp.To {| xy = [| 0.; 0. |] |}
                    SpringProp.ConfigByFn (fun k ->
                        Browser.Dom.console.error k
                        [
                            if k = 0 then
                                SpringConfig.Tension 5000.
                                SpringConfig.Friction 40.
                            else
                                SpringConfig.Mass 100.
                                SpringConfig.Tension 200.
                                SpringConfig.Friction 200.
                        ])
                 ]
            )
        
        div </> [
            Style [
                Height 300
                Width 400
                Position PositionOptions.Relative
            ]
            Children [
                svg [
                  Style [
                    Position PositionOptions.Absolute
                    Width 0
                    Height 0
                    Display DisplayOptions.None
                  ]
                ] [
                   svgEl "filter" [
                    Id "goo"
                  ] [
                    svgEl "feGaussianBlur" [
                      SVGAttr.Custom("in", "SourceGraphic")
                      SVGAttr.Custom("result", "blur")
                      SVGAttr.Custom("stdDeviation", "10")
                    ] []
                    svgEl "feColorMatrix" [
                      SVGAttr.Custom("in", "blur")
                      SVGAttr.Custom("values", "1 0 0 0 0 0 1 0 0 0 0 0 1 0 0 0 0 0 30 -7")
                    ] []
                  ]
                ]

                Animated.div </> [
                    Style [
                        Height 300
                        Width 400
                        BackgroundColor "lightgray"
                        Filter "url('#goo')"
                        OverflowStyle OverflowOptions.Hidden
                    ]
                    OnMouseMove (fun x -> trail.update {| xy = [| x.clientX; x.clientY |] |})
                    Children (
                        trail.current
                        |> Seq.mapi (fun i data ->
                            Animated.div </> [
                                Key (string i)
                                Style [
                                    Width 120
                                    Height 120
                                    BorderRadius "50%"
                                    BackgroundColor "lightcoral"
                                    Position PositionOptions.Absolute
                                    WillChange "transform"
                                    Opacity 0.5
                                    BoxShadow "10px 10px 5px 0px rgba(0, 0, 0, 0.75)"
                                    Transform (AnimatedValue(data.xy).map (fun (x, y) -> sprintf "translate3d(%dpx,%dpx,0) translate3d(-50%%,-50%%,0)" x y))
                                ]
                            ]
                        )
                        |> Seq.toList
                    )
              ]
            ]
        ]
  )