module Client.App.AnimeJsDemo

open Fable.Core
open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props
open Fun.AnimeJs


let config =
  {|
    targets = "#anime-1"
    translateX =
      {|
        value = "*=2.5" // 100px * 2.5 = '250px'
        duration = 1000
      |}
    width =
      {|
        value = "-=30px" // 28 - 20 = '8px'
        duration = 1800
        easing = "easeInOutSine"
      |}
    height =
      {|
        value = "-=30px" // 28 - 20 = '8px'
        duration = 1800
        easing = "easeInOutSine"
      |}
    borderRadius =
      {|
        value = 25
        duration = 1800
      |}
    rotate =
      {|
        value = "+=2turn" // 0 + 2 = '2turn'
        duration = 1800
        easing = "easeInOutSine"
        delay = 1000
      |}
    direction = "alternate"
  |}


let animeDiv =
  FunctionComponent.Of(
    fun () ->
      let room() =
        Anime.anime2
          {|
            targets = "#anime-1"
            translateX = fun el ->
              el?getAttribute("data-x")
            translateY = fun (el, i) ->
              50 + (-50 * i)
            scale = fun (el, i, l) ->
              (l - i) + 0.25
            rotate = fun _ -> Anime.animejs?random(-360, 360)
            borderRadius = fun _ -> [| "50%"; Anime.animejs?random(10, 35) + "%" |]
            duration = fun _ -> Anime.animejs?random(1200, 1800)
            delay = fun _ -> Anime.animejs?random(0, 400)
            direction = "alternate"
            loop = true
          |}
      let normal() =
        Anime.anime2
          {|
            targets = "#anime-1"
            duration = 500
            width = 50
            height = 50
          |}
      Hooks.useEffect(
        (fun () ->
          //Anime.anime2 config
          //room()
          Anime.anime [
            AnimeProp.targets ("#anime-1")
            AnimeProp.cssProp (Height 100)
            AnimeProp.loop 3
            AnimeProp.delay 1000
            AnimeProp.direction "alternate"
          ]
        )
        ,[||]
      )
      div </> [
        Id "anime-1"
        Style [
          Width "50px"
          Height "50px"
          BackgroundColor "purple"
          Cursor "pointer"
        ]
        Children [
          svg [
            SVGAttr.Points ""
          ] []
        ]
        //OnMouseEnter (fun _ -> room())
        //OnMouseLeave (fun _ -> normal())
      ]
  )