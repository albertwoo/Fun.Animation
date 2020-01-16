module Client.App.ReactSpringDemos.SpringsDemo

open Fable.React
open Fable.React.Props
open Fun.ReactSpring


let pages = [
  "https://images.pexels.com/photos/62689/pexels-photo-62689.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260"
  "https://images.pexels.com/photos/296878/pexels-photo-296878.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260"
  "https://images.pexels.com/photos/1509428/pexels-photo-1509428.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260"
  "https://images.pexels.com/photos/351265/pexels-photo-351265.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260"
]



let main =
    FunctionComponent.Of(
        fun () ->
            let calc index i =
                {|
                    width = if i = index then 200. else 100.
                |}

            let springs =
                SpringHooks.useSprings(
                    pages.Length,
                    (fun i -> [
                        Property.To (calc i -1)
                        Property.Config Configs.Slow
                    ])
               )

            div </> [
                Children (
                    springs.current
                    |> Seq.mapi (fun index data ->
                        Animated.div </> [
                            Children [
                                Animated.div </> [
                                    Style [
                                        Height 200
                                        Width data.width
                                        BackgroundImage (sprintf "url(%s)" pages.[index])
                                    ]
                                    OnMouseEnter (fun _ -> springs.update(calc index))
                                    OnMouseLeave (fun _ -> springs.update(calc index))
                                ]
                            ]
                        ]
                    )
                    |> Seq.toList
                )
            ]
      )