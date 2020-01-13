namespace Fun.ReactSpring


[<RequireQualifiedAccess>]
type ConfigProp =
  | Mass of float
  | Tension of float
  | Friction of float
  | Clamp of bool
  | Precision of float
  | [<CompiledName("velocity")>] InitialVelocity of float
  | Duration of float
  | Easing of (float -> float)


[<RequireQualifiedAccess>]
module Configs =
    let Default =
        [
            ConfigProp.Mass 1.
            ConfigProp.Tension 170.
            ConfigProp.Friction 26.
        ]

    let Gentle =
        [
            ConfigProp.Mass 1.
            ConfigProp.Tension 120.
            ConfigProp.Friction 14.
        ]

    let Wobbly =
        [
            ConfigProp.Mass 1.
            ConfigProp.Tension 180.
            ConfigProp.Friction 12.
        ]

    let Stiff =
        [
            ConfigProp.Mass 1.
            ConfigProp.Tension 210.
            ConfigProp.Friction 20.
        ]

    let Slow =
        [
            ConfigProp.Mass 1.
            ConfigProp.Tension 280.
            ConfigProp.Friction 60.
        ]

    let Molasses =
        [
            ConfigProp.Mass 1.
            ConfigProp.Tension 280.
            ConfigProp.Friction 120.
        ]

