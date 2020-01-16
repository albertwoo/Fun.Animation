module Client.App.Views

open Fable.React
open Fable.React.Props
open Client.App.ReactSpringDemos
open Controls


let app state dispatch =
    div </> [
        Children [
            landingPage

            demoCard [
                DemoCardProp.View (DraggableListDemo.main())
                DemoCardProp.DescriptionView DraggableListDemo.description
                DemoCardProp.ContainerCss [ Tw.``bg-purple-100`` ]
            ]

            demoCard [
                DemoCardProp.View (SpringDemo.main())
                DemoCardProp.ContainerCss [ Tw.``bg-green-100`` ]
            ]

            demoCard [
                DemoCardProp.View (SpringsDemo.main())
                DemoCardProp.ContainerCss [ Tw.``bg-gray-100`` ]
            ]

            demoCard [
                DemoCardProp.View (TransitionDemo.main())
                DemoCardProp.ContainerCss [ Tw.``bg-pink-100`` ]
            ]

            demoCard [
                DemoCardProp.View (ChainDemo.main())
                DemoCardProp.ContainerCss [ Tw.``bg-green-100`` ]
            ]
        ]
    ]
