# This projects contains animation related liraries for fable 

[Demo site](https://albertwoo.github.io/Fun.Animation/)

1. React-Spring binding for fable
2. React-UseGesture binding for fable


## How to use

1. Add nuget package `Fun.ReactSpring`, `Fun.ReactGesture`
2. Use `femto` to resolve related npm packages
3. `open Fun.ReactSpring` or `open Fun.ReactGesture` 
    ```fsharp
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
                springs.update (calc(newOrder, state.down, originalIndex, curIndex, y))
                if (not state.down) then order.current <- newOrder
        )
    ```

    more samples can be found under `demo/Client/ReactSpringDemos` folder

## This is still under dev

## Road map:
  
   1. Refactor api
   2. Bug fix
   3. Server side rendering partial support  