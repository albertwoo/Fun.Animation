module Client.App.ReactSpringDemos.SpringsDemo

open Fable.React
open Fable.React.Props
open Fun.ReactSpring
open Fun.ReactGesture


let pages = [
  "https://images.pexels.com/photos/62689/pexels-photo-62689.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260"
  "https://images.pexels.com/photos/296878/pexels-photo-296878.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260"
  "https://images.pexels.com/photos/1509428/pexels-photo-1509428.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260"
  "https://images.pexels.com/photos/351265/pexels-photo-351265.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260"
]


let main =
    FunctionComponent.Of(
        fun () ->
            let calc y index i =
                {|
                    width = if i = index then 100. + y else 100.
                |}

            let springs =
                SpringHooks.useSprings(
                    pages.Length,
                    (fun i -> [
                        SpringProp.To (calc 0. i -1)
                        SpringProp.Config SpringConfigs.Slow
                    ])
               )

            let gestureAttrs =
                GestureHooks.useDrag (fun state ->
                    let index = state.args.[0] |> unbox<int>
                    if state.down then springs.update (calc state.movement.x index)
                )

            div </> [
                Children (
                    springs.current
                    |> Seq.mapi (fun index data ->
                        Animated.div </> [
                            Children [
                                Animated.div </> [
                                    Classes [ Tw.``cursor-pointer`` ]
                                    Style [
                                        Height 200
                                        Width data.width
                                        BackgroundImage (sprintf "url('%s')" pages.[index])
                                    ]
                                    yield! toHTMLProps(gestureAttrs.bind index)
                                ]
                            ]
                        ]
                    )
                    |> Seq.toList
                )
            ]
      )