module Client.App.ReactSpringDemos.DraggableListDemo

open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props
open Fun.ReactSpring
open Fun.ReactGesture


let render =
    FunctionComponent.Of (
        fun () ->
            let fn (order: int[], down, originalIndex, curIndex, y) index =
                if down && index = originalIndex then
                    {|
                        y = curIndex * 10 + y
                        scale = 1.1
                        zIndex = "1"
                        shadow = 15
                        //immediate = fun n -> n = "y" || n = "zIndex"
                    |}
                else
                    {|
                        y = (order |> Seq.findIndex ((=) index)) * 10
                        scale = 1.
                        zIndex = "0"
                        shadow = 1
                        //immediate = fun _ -> false
                    |}

            let items = Hooks.useState [|0..5|]
            let order = Hooks.useRef(items.current |> Seq.mapi (fun i _ -> i) |> Seq.toArray)

            let springs =
                SpringHooks.useSprings(
                    items.current.Length,
                    fun i -> [
                        Property.To (fn (order.current, false, 0, 0, 0) i)
                    ]
                )
            let bind =
                GestureHooks.useDrag(
                    fun state ->
                        Browser.Dom.console.error state
                        ()
                    ,[
                        ConfigProp.Capture true
                    ]
                )

            div </> [
                Classes [ Tw.absolute; Tw.``w-full``; Tw.``h-full``; Tw.``overflow-hidden``; Tw.flex; Tw.``items-center``; Tw.``justify-center`` ]
                Children [
                    div </> [
                        Classes [ Tw.``w-64`` ]
                        Children (
                            springs.current
                            |> Seq.mapi (fun i data ->
                                Animated.div </> [
                                    Key (string i)
                                    Text (string i)
                                    Classes [ Tw.``cursor-pointer``; Tw.``text-4xl``; Tw.rounded; Tw.``m-04``; Tw.``text-center``; Tw.``bg-purple-300``; Tw.``select-none`` ]
                                    Style [
                                        ZIndex data.zIndex
                                        BoxShadow (
                                            data.shadow?interpolate (fun x ->
                                                sprintf "rgba(0, 0, 0, 0.15) 0px %dpx %dpx 0px" x (x * 2)
                                            )
                                        )
                                        Transform (
                                            data.y?interpolate (fun y ->
                                                sprintf "translate3d(0,%fpx,0) scale(%f)" y 1.1
                                            )
                                        )
                                    ]
                                    yield! toHTMLProps(bind.bind(i))
                                    //yield! toHTMLProps(useDrag (fun s -> Browser.Dom.console.error s) i)
                                ]
                            )
                            |> Seq.toList
                        )
                    ]
                ]
            ]
    )