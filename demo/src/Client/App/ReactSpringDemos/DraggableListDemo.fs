module Client.App.ReactSpringDemos.DraggableListDemo

open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props
open Fun.ReactSpring


let render =
    FunctionComponent.Of (
        fun () ->
            let fn (order: int[], down, originalIndex, curIndex, y) index =
                if down && index = originalIndex then
                    {|
                        y = curIndex * 100 + y
                        scale = 1.1
                        zIndex = "1"
                        shadow = 15
                        //immediate = fun n -> n = "y" || n = "zIndex"
                    |}
                else
                    {|
                        y = (order |> Seq.findIndex ((=) index)) * 100
                        scale = 1.
                        zIndex = "0"
                        shadow = 1
                        //immediate = fun _ -> false
                    |}

            let isDown = Hooks.useState false
            let point = Hooks.useState None
            let items = Hooks.useState [|0..5|]
            let order = Hooks.useRef(items.current |> Seq.mapi (fun i _ -> i) |> Seq.toArray)
            let springs =
                SpringHooks.useSprings(
                    items.current.Length,
                    fun i -> [
                        Property.To (fn (order.current, false, 0, 0, 0) i)
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
                                    Classes [ Tw.``cursor-pointer``; Tw.``text-4xl``; Tw.rounded; Tw.``m-04``; Tw.``text-center``; Tw.``bg-purple-300`` ]
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
                                    OnMouseDown (fun e ->
                                        isDown.update (not isDown.current)
                                        point.update (Some (e.clientX, e.clientY))
                                    )
                                    OnMouseUp (fun _ ->
                                        isDown.update false
                                        point.update None
                                    )
                                    OnMouseMove (fun e ->
                                        match point.current with
                                        | None -> ()
                                        | Some (x', y') ->
                                            let y = e.clientY - y'
                                            let curIndex = order.current |> Seq.findIndex ((=) i)
                                            let newOrder = order.current |> Seq.rev |> Seq.toArray
                                            springs.update (fn (newOrder, true, i, curIndex, int y))
                                        point.update (Some (e.clientX, e.clientY))
                                    )
                                ]
                            )
                            |> Seq.toList
                        )
                    ]
                ]
            ]
    )