module Client.App.Views

open Fable.React
open Fable.React.Props
open Client.App.ReactSpringDemos
open Controls


let landingPage =
    div </> [
        Style [
            Background "linear-gradient(to right, rgba(0, 0, 0, 0.1) 1px, transparent 1px) 0% 0% / 40px 40px, linear-gradient(rgba(0, 0, 0, 0.1) 1px, transparent 1px) rgb(54, 54, 69)"
        ]
        Classes [ Tw.``p-16``; Tw.``text-center`` ]
        Children [
            h1 </> [
                Classes [ Tw.``text-3xl``; Tw.``font-semibold``; Tw.``text-white`` ]
                Children [
                    span </> [ Text "react-spring for" ]
                    span </> [
                        Text " fable"
                        Classes [ Tw.``text-purple-400``; Tw.``font-bold`` ]
                    ]
                ]
            ]
            p </> [
                Text "bring your components to life with simple spring animation primitives"
                Classes [ Tw.``text-gray-400``; Tw.``mb-04`` ]
            ]
            a </> [
                Text ">> Check the react-spring official site"
                Href "https://www.react-spring.io/"
                Classes [ Tw.``text-purple-400``; Tw.``hover:text-purple-300`` ]
            ]
            a </> [
                Href "https://github.com/albertwoo/Fun.Animation"
                Classes [ Tw.block; Tw.``mt-04`` ]
                Children [
                    span </> [
                        Classes [ Fa.fab; Fa.``fa-github``; Tw.``text-green-600`` ]
                    ]
                ]
            ]
        ]
    ]


let app state dispatch =
    div </> [
        Classes [ Tw.``font-sans`` ]
        Children [
            landingPage

            demoCard [
                DemoCardProp.View (DraggableListDemo.main())
                DemoCardProp.Title "useDrag & useSprings for drag list"
                DemoCardProp.Description "You can drag to reorder the item"
                DemoCardProp.ContainerCss [ Tw.``bg-purple-100`` ]
            ]

            demoCard [
                DemoCardProp.View (SpringDemo.main())
                DemoCardProp.Title "useSpring"
                DemoCardProp.Description "You can hover to check the animation"
                DemoCardProp.ContainerCss [ Tw.``bg-green-100`` ]
            ]

            demoCard [
                DemoCardProp.View (SpringsDemo.main())
                DemoCardProp.Title "useSprings"
                DemoCardProp.Description "You can drag image to make it longer"
                DemoCardProp.ContainerCss [ Tw.``bg-gray-100`` ]
            ]

            demoCard [
                DemoCardProp.View (TransitionDemo.main())
                DemoCardProp.Title "useTransition"
                DemoCardProp.Description "You can click or use mouse wheel to trigger the animation"
                DemoCardProp.ContainerCss [ Tw.``bg-pink-100`` ]
            ]

            demoCard [
                DemoCardProp.View (ChainDemo.main())
                DemoCardProp.Title "useTransition & useChain"
                DemoCardProp.Description "You can click to trigger the animation"
                DemoCardProp.ContainerCss [ Tw.``bg-green-100`` ]
            ]

            h2 </> [
                Text "RenderProps Demo"
                Classes [ Tw.``text-center``; Tw.``text-3xl``; Tw.``mt-10``; Tw.``mb-10``; Tw.``font-bold``; Tw.``text-green-600`` ]
            ]

            demoCard [
                DemoCardProp.View (RenderPropsDemo_Spring.render())
                DemoCardProp.Title "Spring"
                DemoCardProp.Description "Click star to trigger animation"
                DemoCardProp.ContainerCss [ Tw.``bg-green-100`` ]
            ]

            demoCard [
                DemoCardProp.View (RenderPropsDemo_Trail.render())
                DemoCardProp.Title "Trail"
                DemoCardProp.Description "Click rectangle to trigger animation"
                DemoCardProp.ContainerCss [ Tw.``bg-blue-100`` ]
            ]
        ]
    ]
