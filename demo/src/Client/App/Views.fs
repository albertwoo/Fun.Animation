module Client.App.Views

open Fable.Core
open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props



//let transitionDemo =
//  FunctionComponent.Of (
//    fun () ->
//      let source = Hooks.useState [||]
//      let transitions =
//        SpringHooks.useTransition(
//          source.current,
//          id,
//          {|
//            from  = {| transform = "translate3d(0,-40px,0)" |}
//            enter = {| transform = "translate3d(0,0px,0)" |}
//            leave = {| transform = "translate3d(0,-40px,0)" |}
//          |}
//        )

//      let updater = Hooks.useRef (fun () -> ())

//      Hooks.useEffect(
//        (fun () ->
//          updater.current <- fun () ->
//            let newItems =
//              [
//                yield! source.current |> Seq.toList
//                source.current.Length + 1
//              ]
//              |> Seq.toArray
//            source.update(newItems)

//          Browser.Dom.window.setInterval(updater.current,1000) |> ignore)
//        ,[||]
//      )

//      div [] (
//        transitions
//        |> Seq.map (fun data ->
//          Browser.Dom.console.error data
//          Animated.div </> [
//            Key data?key
//            Classes [ Tw.``text-4xl``; Tw.``mx-auto`` ]
//            Style [
//              Transform data?props?transform
//            ]
//            Text (string data?key)
//          ]
//        )
//      )
//  )



//let pages = [
//  "https://images.pexels.com/photos/62689/pexels-photo-62689.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260"
//  "https://images.pexels.com/photos/296878/pexels-photo-296878.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260"
//  "https://images.pexels.com/photos/1509428/pexels-photo-1509428.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260"
//  "https://images.pexels.com/photos/351265/pexels-photo-351265.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260"
//  "https://images.pexels.com/photos/924675/pexels-photo-924675.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260"
//]
//let springsDemo =
//  FunctionComponent.Of(
//    fun () ->
//      let index = Hooks.useRef 0
//      let springs =
//        SpringHooks.useSpringsLazy(
//          pages.Length,
//          (fun i ->
//            {|
//              x = float i * Browser.Dom.window.innerWidth
//              sc = 1
//              display = "block"
//              config = {| mass = 5; tension = 350; friction = 40 |}
//            |}
//          )
//        )
//      div </> [
//        Children (
//          springs.current
//          |> Seq.mapi (fun i data ->
//            Browser.Dom.console.error data
//            Animated.div </> [
//              Children [
//                Animated.div </> [
//                  Style [
//                    Height 200
//                    Width 1024
//                    Display (unbox data?display)
//                    Transform data?transform
//                    BackgroundImage (sprintf "url(%s)" pages.[i])
//                  ]
//                ]
//              ]
//            ]
//          )
//          |> Seq.toList
//        )
//      ]
//  )


//let trailDemo =
//  FunctionComponent.Of (
//    fun () ->
//      let fast = {| tension = 1200; friction = 40 |}
//      let slow = {| mass = 10; tension = 200; friction = 50 |}
//      let trans x y = sprintf "translate3d(%dpx,%dpx,0) translate3d(-50%%,-50%%,0)" x y

//      let trail =
//        SpringHooks.useTrailLazy(
//          3,
//          fun () ->
//            {|
//              xy = [0.; 0.] |> toJsArray
//              config = fun i ->
//                if i = 0 then box fast else box slow
//            |}
//        )

//      div </> [
//        Children [
//          svg [
//            Style [
//              Position PositionOptions.Absolute
//              Width 0
//              Height 0
//              Display DisplayOptions.None
//            ]
//          ] [
//             svgEl "filter" [
//              Id "goo"
//            ] [
//              svgEl "feGaussianBlur" [
//                SVGAttr.Custom("in", "SourceGraphic")
//                SVGAttr.Custom("result", "blur")
//                SVGAttr.Custom("stdDeviation", "10")
//              ] []
//              svgEl "feColorMatrix" [
//                SVGAttr.Custom("in", "blur")
//                SVGAttr.Custom("values", "1 0 0 0 0 0 1 0 0 0 0 0 1 0 0 0 0 0 30 -7")
//              ] []
//            ]
//          ]

//          Animated.div </> [
//            Style [
//              Height 300
//              Width 400
//              Position PositionOptions.Absolute
//              BackgroundColor "lightgray"
//              Filter "url('#goo')"
//              OverflowStyle OverflowOptions.Hidden
//            ]
//            OnMouseMove (fun x -> trail.update {| xy = toJsArray [ x.clientX; x.clientY ] |})
//            Children (
//              trail.current
//              |> Seq.mapi (fun i data ->
//                Animated.div </> [
//                  Key (string i)
//                  Style [
//                    Width 120
//                    Height 120
//                    BorderRadius "50%"
//                    BackgroundColor "lightcoral"
//                    Position PositionOptions.Absolute
//                    WillChange "transform"
//                    Opacity 0.5
//                    BoxShadow "10px 10px 5px 0px rgba(0, 0, 0, 0.75)"
//                    Transform (data?xy?interpolate(trans))
//                  ]
//                ]
//              )
//              |> Seq.toList
//            )
//          ]
//        ]
//      ]
//  )


//let chainDemo =
//    FunctionComponent.Of (
//        fun () ->
//            let isOpen = Hooks.useState false

//            let springRef = Hooks.useRef null
//            let spring =
//                SpringHooks.useSpring
//                    {|
//                        ref = springRef
//                        config = SpringConfigs.stiff
//                        from = {| size = "20%"; background = "hotpink" |}
//                        ``to`` =
//                            {|
//                                size = if isOpen.current then "100%" else "20%"
//                                background = if isOpen.current then "white"  else "hotpink"
//                            |}
//                    |}

//            let transRef = Hooks.useRef null
//            let trans =
//                SpringHooks.useTransition(
//                    ((if isOpen.current then [|1..12|] else [||])|> toJsArray),
//                    id,
//                    {|
//                        ref = transRef
//                        unique = true
//                        trail = 400. / 12.
//                        from  = {| opacity = 0; transform = "scale(0)" |}
//                        enter = {| opacity = 1; transform = "scale(1)" |}
//                        leave = {| opacity = 0; transform = "scale(0)" |}
//                    |}
//                )

//            SpringHooks.useChain(
//                (if isOpen.current then [|springRef; transRef|] else [|transRef; springRef|]),
//                ([|0.; if isOpen.current then 0.1 else 0.5|])
//            )

//            div </> [
//                Classes [
//                    Tw.``w-full``
//                    Tw.``h-screen``
//                    Tw.``overflow-hidden``
//                    Tw.``select-none``
//                    Tw.``bg-blue-100``
//                    Tw.flex
//                    Tw.``items-center``
//                    Tw.``justify-center``
//                ]
//                Children [
//                    Animated.div </> [
//                        Style [
//                            Position PositionOptions.Relative
//                            Display DisplayOptions.Grid
//                            GridTemplateColumns "repeat(4, minmax(100px, 1fr))"
//                            GridGap "25px"
//                            Padding "25px"
//                            BorderRadius "5px"
//                            Cursor "pointer"
//                            BoxShadow "0px 10px 10px -5px rgba(0, 0, 0, 0.05)"
//                            WillChange "width, height"
//                            Width spring?size
//                            Height spring?size
//                            BackgroundColor spring?background
//                        ]
//                        OnClick (fun _ -> isOpen.update(not isOpen.current))
//                        Children (
//                            trans
//                            |> Seq.map (fun data ->
//                                Animated.div </> [
//                                    Key data?key
//                                    Style [
//                                        Width "100%"
//                                        Height "100%"
//                                        BackgroundColor "white"
//                                        BorderRadius "5px"
//                                        WillChange "transform, opacity"
//                                        Opacity data?props?opacity
//                                        Transform data?props?transform
//                                        BackgroundColor (sprintf "rgb(%d,%d,%d)" (100 + data?item * 10) (200 + data?item * 10) (150 + data?item * 10))
//                                    ]
//                                ] 
//                            )
//                            |> Seq.toList
//                        )
//                    ]
//                ]
//            ]
//    )

let app state dispatch =
    //AnimeJsDemo.animeDiv()
    //transitionDemo()
    //springDemo()
    //springsDemo()
    //trailDemo()
    //ReactSpringDemos.SpringDemo.render()
    //ReactSpringDemos.SpringsDemo.render()
    ReactSpringDemos.TrailDemo.render()
