namespace Img.Mod.Lib

open System.Net.Http
open SixLabors.ImageSharp
open SixLabors.ImageSharp.PixelFormats
open System.IO

module FileHandler =
    let GetBytesFromUrl url =
        async {
            printfn "Fetching %s" url
            let client = new HttpClient()

            let! res = client.GetAsync url
                       |> Async.AwaitTask

            let! content = res.Content.ReadAsByteArrayAsync()
                           |> Async.AwaitTask

            printfn "Done fetching %s" url

            return content  
        } 

    let GetImageFromBytes (data: byte[]) =
        Image.Load<Rgba32> data      

    let GetBytesFromImage (image: Image<Rgba32>) =
        let memoryStream = new MemoryStream()
        image.SaveAsPng(memoryStream)

        image.SaveAsJpeg(memoryStream)  
        
        memoryStream.ToArray()

    let GetImageFromUrl url =
        async {
            return GetBytesFromUrl url 
                   |> Async.RunSynchronously
                   |> GetImageFromBytes
        }

    let SaveAndDispose (image:Image<Rgba32>, path) = 
        image.Save(path)
        printf("Image saved")