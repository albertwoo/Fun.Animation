module Client.App.Views

open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props
open Fun.ReactSpring


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
          Animated.forEle "div" [
            Key data?key
            Classes [ Tw.``text-4xl``; Tw.``mx-auto`` ]
            Style [
              Transform data?props?transform
            ]
          ] [
            str (string data?key)
          ]
        )
      )
  )

let springDemo =
  FunctionComponent.Of(
    fun () ->
      let props =
        SpringHooks.useSpring
          {|
            from = {| opacity = 0; color = "red" |}
            ``to`` = [|
              {| opacity = 1; color = "#ffaaee" |}
              {| opacity = 0; color = "rgb(14,26,19)" |}
            |]
          |}
      Browser.Dom.console.error props
      Animated.forEle "div" [
        Style [
          Opacity props?opacity
          BackgroundColor props?color
        ]
      ] [
        str "useSpring"
      ]
  )

let app state dispatch =
    div </> [
      Classes [
        Tw.``sm:w-full``
        Tw.``md:w-03/04``
        Tw.``lg:w-02/03``
        Tw.``mx-auto``
      ]
      Children [
        //AnimeJsDemo.animeDiv()
        //transitionDemo()
        springDemo()
      ]
    ]
