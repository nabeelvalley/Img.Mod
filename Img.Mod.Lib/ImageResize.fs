namespace Img.Mod.Lib

open FileHandler
open SixLabors.ImageSharp
open SixLabors.ImageSharp.Processing

module ImageResize =

    let ResizeImageByWidth (url, width) =
        async {
            use! image = GetImageFromUrl url

            let mutateAction (img:IImageProcessingContext) = 
                img.Resize(width, 0) |> ignore

            image.Mutate(mutateAction)

            return GetBytesFromImage image
        }
        

    let ResizeImageByHeight (url, height) =
        async {
            use! image = GetImageFromUrl url

            let mutateAction (img:IImageProcessingContext) = 
                img.Resize(0, height) |> ignore

            image.Mutate(mutateAction)

            return GetBytesFromImage image   
        }
    