namespace Img.Mod.Tests

open System
open System.IO
open Xunit
open Img.Mod.Lib
open SixLabors.ImageSharp
open SixLabors.ImageSharp.PixelFormats

module ImageResizeTests =

    let ImageSizeAssertions expectedHeight expectedWidth (image: Image<Rgba32>) = 
        Assert.NotNull(image)
        Assert.False(image.IsDisposed)
        Assert.Equal(expectedHeight, image.Height)
        Assert.Equal(expectedWidth, image.Width)

    [<Theory>]
    [<InlineData(300, 300, 100, 100)>]
    [<InlineData(100, 300, 50, 150)>]
    [<InlineData(200, 100, 100, 50)>]
    let ``ResizeImageByHeight -> expected dimensions`` (initWidth, initHeight, finalWidth, finalHeight) =
        // ARRANGE 
        let url = Helpers.GetImageUrl initHeight initWidth

        let image = FileHandler.GetImageFromUrl url
                    |> Async.RunSynchronously

        // ACT
        let res = ImageResize.ResizeImageByHeight finalHeight image 
                  |> Async.RunSynchronously

        // ASSERT
        ImageSizeAssertions finalHeight finalWidth res

    [<Theory>]
    [<InlineData(300, 300, 100, 100)>]
    [<InlineData(100, 300, 50, 150)>]
    [<InlineData(200, 100, 100, 50)>]
    let ``ResizeImageByWidth -> expected dimensions`` (initWidth, initHeight, finalWidth, finalHeight) =
        // ARRANGE 
        let url = Helpers.GetImageUrl initHeight initWidth

        let image = FileHandler.GetImageFromUrl url
                    |> Async.RunSynchronously

        // ACT
        let res = ImageResize.ResizeImageByWidth finalWidth image 
                  |> Async.RunSynchronously

        // ASSERT
        ImageSizeAssertions finalHeight finalWidth res
