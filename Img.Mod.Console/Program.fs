﻿open System
open Img.Mod.Lib
open System.IO

let url = "https://source.unsplash.com/random/300x200"
let outputLocation = Path.GetTempPath() + Guid.NewGuid().ToString() + ".jpg"

[<EntryPoint>]
let main argv =

    printfn "Location: %s" outputLocation

    use image = FileHandler.GetImageFromUrl url
                |> Async.RunSynchronously

    FileHandler.Save(image, outputLocation)

    0
        