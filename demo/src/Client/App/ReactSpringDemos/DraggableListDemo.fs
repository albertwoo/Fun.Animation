module Client.App.ReactSpringDemos.DraggableListDemo

open System
open Fable.React
open Fable.React.Props
open Fun.ReactSpring
open Fun.ReactGesture


let clamp num min max =
    if num <= min then min
    elif num >= max then max
    else num

let swap x y (source: 'T[]) =
    let copy =  [ for i in source -> i ] |> Seq.toArray
    copy.[x] <- source.[y]
    copy.[y] <- source.[x]
    copy


let main =
    FunctionComponent.Of (
        fun () ->
            let height = 50
            let calc (order: int[], down, originalIndex, curIndex, y) index =
                if down && index = originalIndex then
                    {|
                        y = float (curIndex * height) + y
                        scale = 1.1
                        zIndex = "1"
                        shadow = 15
                    |}
                else
                    {|
                        y = float ((order |> Seq.findIndex ((=) index)) * height)
                        scale = 1.
                        zIndex = "0"
                        shadow = 1
                    |}

            let items = Hooks.useState [|0..5|]
            let order = Hooks.useRef(items.current |> Seq.mapi (fun i _ -> i) |> Seq.toArray)

            let springs =
                SpringHooks.useSprings(
                    items.current.Length,
                    fun i -> [
                        Property.To (calc (order.current, false, 0, 0, 0.) i)
                    ]
                )

            let bind =
                GestureHooks.useDrag(
                    fun state ->
                        let y = state.movement.y
                        let originalIndex = state.args.[0] |> unbox<int>
                        let curIndex = order.current |> Seq.findIndex ((=) originalIndex)
                        let curRow = clamp (int (Math.Round((float curIndex * float height + y) / float height))) 0 (items.current.Length - 1)
                        let newOrder = swap curIndex curRow order.current
                        springs.update (calc(newOrder, state.down, originalIndex, curIndex, y)) // Feed springs new style data, they'll animate the view without causing a single render
                        if (not state.down) then order.current <- newOrder
                )

            div </> [
                Style [
                    Height (height * items.current.Length)
                    Width 200
                ]
                Children [
                    div </> [
                        Classes [ Tw.``w-64``; Tw.relative ]
                        Children (
                            springs.current
                            |> Seq.mapi (fun i data ->
                                Animated.div </> [
                                    Key (string i)
                                    Text (string i)
                                    Classes [ Tw.``cursor-pointer``; Tw.rounded; Tw.``text-center``; Tw.``text-white``; Tw.``bg-purple-300``; Tw.``select-none`` ]
                                    Style [
                                        Position PositionOptions.Absolute
                                        Height (height - 10)
                                        LineHeight (sprintf "%dpx" (height - 10))
                                        Width 200
                                        ZIndex data.zIndex
                                        BoxShadow (AnimatedValue(data.shadow).map(fun x -> sprintf "rgba(0, 0, 0, 0.15) 0px %dpx %dpx 0px" x (x * 2)))
                                        Transform ((data.y, data.scale) |> Interpolation.map (fun (y, s) -> sprintf "translate3d(0,%fpx,0) scale(%f)" y s))
                                    ]
                                    yield! toHTMLProps(bind.bind(i))
                                ]
                            )
                            |> Seq.toList
                        )
                    ]
                ]
            ]
    )
