#r @"packages/FAKE/tools/FakeLib.dll"

open Fake
open System
open System.IO

let buildDir = "bin"

Target "Clean" (fun _ ->
    CleanDirs [buildDir]
)

Target "Build" (fun _ ->
   !! "src/**/*.csproj"
     |> MSBuildRelease buildDir "Build"
     |> Log "AppBuild-Output: "
)
"Clean"
  ==> "Build"
RunTargetOrDefault "Build"