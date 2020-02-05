namespace Img.Mod.Lib

open FileHandler
open SixLabors.ImageSharp
open SixLabors.ImageSharp.Processing
open SixLabors.ImageSharp.PixelFormats

module ImageResize =

    let ResizeImageByWidth width (image: Image<Rgba32>) =
        async {
            let mutateAction (img:IImageProcessingContext) = 
                img.Resize(width, 0) |> ignore

            image.Mutate(mutateAction)

            return image
        }
        

    let ResizeImageByHeight height (image: Image<Rgba32>)  =
        async {
            let mutateAction (img:IImageProcessingContext) = 
                img.Resize(0, height) |> ignore

            image.Mutate(mutateAction)

            return image   
        }
    