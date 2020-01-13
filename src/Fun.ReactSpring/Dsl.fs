namespace Fun.ReactSpring


[<RequireQualifiedAccess>]
module Interpolation =
    let inline map m prop = Bindings.interpolate(prop, m)


type SpringHooks() =
    static member inline useSpring<'Option> (props: Property<'Option> seq) =
        props |> Property<'Option>.toObj |> Bindings.SpringHooks.useSpring |> unbox<'Option>
        
    static member inline useSpringLazy<'Option> (fn: unit -> Property<'Option> list) =
        Bindings.SpringHooks.useSpringLazy (fun () -> fn() |> Property<'Option>.toObj) |> unbox<Bindings.ISpring<'Option>>

