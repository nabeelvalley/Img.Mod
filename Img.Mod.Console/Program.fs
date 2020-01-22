open System
open Img.Mod.Lib
open System.IO

let url = "https://source.unsplash.com/random/800x600"
let outputLocation = Path.GetTempPath() + Guid.NewGuid().ToString() + ".jpg"

[<EntryPoint>]
let main argv =

    printfn "Location: %s" outputLocation

    let image = File.GetImage url
                |> Async.RunSynchronously
                |> File.GetImageFromBytes

    File.SaveAndDispose(image, outputLocation)

    0
        