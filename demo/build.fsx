#r "nuget: Fake.Core.Process,5.20.0"
#r "nuget: Fake.IO.FileSystem,5.20.0"
#r "nuget: BlackFox.Fake.BuildTask,0.1.3"

open System.IO
open Fake.Core
open Fake.IO
open Fake.IO.Globbing.Operators
open Fake.IO.FileSystemOperators
open BlackFox.Fake


type Env = PROD | DEV

let clientProjectDir = "./src"


fsi.CommandLineArgs
|> Array.skip 1
|> BuildTask.setupContextFromArgv 

let platformTool tool winTool =
    let tool = if Environment.isUnix then tool else winTool
    match ProcessUtils.tryFindFileOnPath tool with
    | Some t -> t
    | _ -> failwith (tool + " was not found in path. ")

let run cmd args workingDir =
    let arguments = args |> String.split ' ' |> Arguments.OfArgs
    Command.RawCommand (cmd, arguments)
    |> CreateProcess.fromCommand
    |> CreateProcess.withWorkingDirectory workingDir
    |> CreateProcess.ensureExitCode
    |> Proc.run
    |> ignore


let yarn   = run (platformTool "yarn" "yarn.cmd")
let dotnet = run (platformTool "dotnet" "dotnet.exe")  


let watchFile fn file =
    let watcher = new FileSystemWatcher(Path.getDirectory file, Path.GetFileName file)
    watcher.NotifyFilter <-  NotifyFilters.CreationTime ||| NotifyFilters.Size ||| NotifyFilters.LastWrite
    watcher.Changed.Add (fun _ ->
        printfn "File changed %s" file
        fn())
    watcher.EnableRaisingEvents <- true
    watcher


// It will generate all source code for the target project in the dir and put the js under fablejs
let runFable dir env watch =
    let mode = match watch with false -> "" | true -> " watch"
    let define = match env with PROD -> "" | DEV -> " --define DEBUG"
    dotnet (sprintf "fable%s . --outDir ./www/fablejs%s --typedArrays false" mode define) dir

let cleanGeneratedJs dir = Shell.cleanDir (dir </> "www/fablejs")

// It will generate css under the starget dir`s www/css folder
let buildTailwindCss dir =
    printfn "Build client css"
    yarn "tailwindcss build css/tailwind-source.css -o css/tailwind-generated.css -c tailwind.config.js" (dir </> "www")

let watchTailwindCss dir =
    !!(dir </> "www/css/app-dev.css")
    ++(dir </> "www/tailwind.config.js")
    |> Seq.toList
    |> List.map (watchFile (fun () -> buildTailwindCss dir))

let serveDevJs dir =
    Shell.cleanDir (dir </> "www/.dist")
    yarn "parcel index.html --dist-dir .dist" (dir </> "www")

let runBundle dir =
    Shell.cleanDir (dir </> "www/.dist_prod")
    yarn "parcel build index.html --dist-dir .dist_prod --public-url ./ --no-source-maps" (dir </> "www")


let checkEnv =
    BuildTask.create "Check environment" [] {
        yarn "--version" ""
        yarn "install" (clientProjectDir </> "www")
        dotnet "tool restore" ""
    }


let preBuildClient =
    BuildTask.create "PreBuildClient" [ checkEnv ] {
        cleanGeneratedJs clientProjectDir
        buildTailwindCss clientProjectDir
    }


let runClientDev =
    BuildTask.create "RunClientDev" [ preBuildClient ] {
        runFable clientProjectDir DEV false
        [
            async {
                runFable clientProjectDir DEV true
            }
            async {
                let watchers = watchTailwindCss clientProjectDir
                serveDevJs clientProjectDir
                printfn "Clean up..."
                watchers |> List.iter (fun x -> x.Dispose())
            }
        ]
        |> Async.Parallel
        |> Async.RunSynchronously
        |> ignore
    }


let bundleClientProd =
    BuildTask.create "BundleClientProd" [ preBuildClient ] {
        runFable clientProjectDir PROD false
        runBundle clientProjectDir
    }


BuildTask.runOrDefault runClientDev
