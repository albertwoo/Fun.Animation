module Client.App.Views

open Fable.Core
open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props
open Fun.ReactSpring


let toJsArray arr = arr |> Seq.map box |> Seq.toArray


let transitionDemo =
  FunctionComponent.Of (
    fun () ->
      let source = Hooks.useState [||]
      let transitions =
        SpringHooks.useTransition(
          source.current,
          id,
          {|
            from  = {| transform = "translate3d(0,-40px,0)" |}
            enter = {| transform = "translate3d(0,0px,0)" |}
            leave = {| transform = "translate3d(0,-40px,0)" |}
          |}
        )

      let updater = Hooks.useRef (fun () -> ())

      Hooks.useEffect(
        (fun () ->
          updater.current <- fun () ->
            let newItems =
              [
                yield! source.current |> Seq.toList
                source.current.Length + 1
              ]
              |> Seq.toArray
            source.update(newItems)

          Browser.Dom.window.setInterval(updater.current,1000) |> ignore)
        ,[||]
      )

      div [] (
        transitions
        |> Seq.map (fun data ->
          Browser.Dom.console.error data
          Animated.div </> [
            Key data?key
            Classes [ Tw.``text-4xl``; Tw.``mx-auto`` ]
            Style [
              Transform data?props?transform
            ]
            Text (string data?key)
          ]
        )
      )
  )

let springDemo =
  FunctionComponent.Of(
    fun () ->
      let spring =
        SpringHooks.useSpringLazy(fun () ->
          {|
            xys = [|0.; 0.; 1.|] |> toJsArray
            config = {| mass = 5; tension = 350; friction = 40 |}
          |}
        )

      let calc x y =
        [|
          -(y - Browser.Dom.window.innerHeight / 2.) / 20.
          (x - Browser.Dom.window.innerWidth / 2.) / 20.
          1.1
        |]
        |> toJsArray
        
      let tran x y s =
        sprintf "perspective(600px) rotateX(%ddeg) rotateY(%ddeg) scale(%A)" x y s
      
      Animated.div </> [
        Classes [ Tw.``text-center`` ]
        Style [
          Width 200
          Height 200
          BackgroundColor "green"
          Transform (spring.current?xys?interpolate(tran))
        ]
        OnMouseMove (fun x ->
          spring.update{| xys = calc x.clientX x.clientY |}
        )
        OnMouseLeave (fun _ ->
          spring.update{| xys = toJsArray [0; 0; 1] |}
        )
        Text "useSpring"
      ]
  )


let pages = [
  "https://images.pexels.com/photos/62689/pexels-photo-62689.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260"
  "https://images.pexels.com/photos/296878/pexels-photo-296878.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260"
  "https://images.pexels.com/photos/1509428/pexels-photo-1509428.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260"
  "https://images.pexels.com/photos/351265/pexels-photo-351265.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260"
  "https://images.pexels.com/photos/924675/pexels-photo-924675.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260"
]
let springsDemo =
  FunctionComponent.Of(
    fun () ->
      let index = Hooks.useRef 0
      let springs =
        SpringHooks.useSpringsLazy(
          pages.Length,
          (fun i ->
            {|
              x = float i * Browser.Dom.window.innerWidth
              sc = 1
              display = "block"
              config = {| mass = 5; tension = 350; friction = 40 |}
            |}
          )
        )
      div </> [
        Children (
          springs.current
          |> Seq.mapi (fun i data ->
            Browser.Dom.console.error data
            Animated.div </> [
              Children [
                Animated.div </> [
                  Style [
                    Height 200
                    Width 1024
                    Display (unbox data?display)
                    Transform data?transform
                    BackgroundImage (sprintf "url(%s)" pages.[i])
                  ]
                ]
              ]
            ]
          )
          |> Seq.toList
        )
      ]
  )


let trailDemo =
  FunctionComponent.Of (
    fun () ->
      let fast = {| tension = 1200; friction = 40 |}
      let slow = {| mass = 10; tension = 200; friction = 50 |}
      let trans x y = sprintf "translate3d(%dpx,%dpx,0) translate3d(-50%%,-50%%,0)" x y

      let trail =
        SpringHooks.useTrailLazy(
          3,
          fun () ->
            {|
              xy = [0.; 0.] |> toJsArray
              config = fun i -> if i = 0 then box fast else box slow
            |}
        )

      div </> [
        Children [
          //svg [
          //  Style [
          //    Position PositionOptions.Absolute
          //    Width 0
          //    Height 0
          //  ]
          //] [
          //   svgEl "filter" [
          //    Id "goo"
          //  ] [
          //    svgEl "feGaussianBlur" [
          //      SVGAttr.Custom("in", "SourceGraphic")
          //      SVGAttr.Custom("result", "blur")
          //      SVGAttr.Custom("stdDeviation", "30")
          //    ] []
          //    svgEl "feColorMatrix" [
          //      SVGAttr.Custom("in", "blur")
          //      SVGAttr.Custom("values", "1 0 0 0 0 0 1 0 0 0 0 0 1 0 0 0 0 0 30 -7")
          //    ] []
          //  ]
          //]

          Animated.div </> [
            Style [
              Height 300
              Width 400
              Position PositionOptions.Absolute
              BackgroundColor "lightgray"
            ]
            OnMouseMove (fun x -> trail.update {| xy = toJsArray [ x.clientX, x.clientY ] |})
            Children (
              trail.current
              |> Seq.mapi (fun i data ->
                Animated.div </> [
                  Key (string i)
                  Style [
                    Width 60
                    Height 60
                    BorderRadius "50%"
                    BackgroundColor "lightcoral"
                    Position PositionOptions.Absolute
                    WillChange "transform"
                    Opacity 0.5
                    Transform data?xy?interpolate?(trans)
                  ]
                ]
              )
              |> Seq.toList
            )
          ]
        ]
      ]
  )

let app state dispatch =
    div </> [
      Classes [
      ]
      Children [
        //AnimeJsDemo.animeDiv()
        //transitionDemo()
        springDemo()
        //springsDemo()
        trailDemo()
      ]
    ]
