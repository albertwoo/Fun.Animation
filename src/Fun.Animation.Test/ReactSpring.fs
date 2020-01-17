module Tests.ReactSpring

open System
open Xunit
open Fun.ReactSpring


[<Fact>]
let ``SSR: AnimatedValue`` () =
    Assert.Equal("Int: 1", AnimatedValue(1).map (sprintf "Int: %d"))
    let data = {| x = 1; y = "2" |}
    Assert.Equal(data, AnimatedValue(data).map id)


[<Fact>]
let ``SSR: Interpolation.map`` () =
    Assert.Equal((1, "2"), Interpolation.map id (1, "2"))


[<Fact>]
let ``SSR: useSpring`` () =
    Assert.Equal({| x = 1 |}, SpringHooks.useSpring [ Property.To {| x = 1 |} ])
    Assert.Equal({| x = 1 |}, SpringHooks.useSpring(fun () -> [ Property.To {| x = 1 |} ]).current)

[<Fact>]
let ``SSR: useSprings`` () =
    Assert.Equal([{| x = 1 |}], SpringHooks.useSprings(1, [ [ Property.To {| x = 1 |} ] ]).current)
    Assert.Equal([{| x = 0 |}; {| x = 1 |}], SpringHooks.useSprings(2, fun i -> [ Property.To {| x = i |} ]).current)

[<Fact>]
let ``SSR: useTrail`` () =
    Assert.Equal({| x = 1 |}, SpringHooks.useTrail(1, [ Property.To {| x = 1 |} ]))
    Assert.Equal([{| x = 1 |}; {| x = 1 |}], SpringHooks.useTrail(2, fun () -> [ Property.To {| x = 1 |} ]).current)

[<Fact>]
let ``SSR: useChain`` () =
    Assert.Equal((), SpringHooks.useChain([||], [||]))

[<Fact>]
let ``SSR: useTransition`` () =
    let actual = SpringHooks.useTransition([| 1 |], id, [ Property.To {| x = 1 |} ]) |> Array.map (fun x -> x.key)
    Assert.Equal([ 1 ], actual)

