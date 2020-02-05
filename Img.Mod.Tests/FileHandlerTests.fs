namespace Img.Mod.Tests

open System
open System.IO
open Xunit
open Img.Mod.Lib.FileHandler

module FileHandlerTests =
    
    let outputLocation = Helpers.GetOutputLocation()

    [<Fact>]
    let ``GetBytesFromUrl -> non-empty Byte[]`` () =
        // ARRANGE 
        let url = Helpers.GetImageUrl 200 300

        // ACT
        let res = GetBytesFromUrl url
                  |> Async.RunSynchronously

        // ASSERT
        Assert.NotNull(res)
        Assert.NotEmpty(res)

    [<Theory>]
    [<InlineData(200, 300)>]
    [<InlineData(300, 100)>]
    [<InlineData(150, 150)>]
    let ``GetImageFromUrl -> valid Image`` (expectedHeight, expectedWidth) =
        // ARRANGE 
        let url = Helpers.GetImageUrl expectedHeight expectedWidth 

        // ACT
        let res = GetImageFromUrl url |> Async.RunSynchronously

        // ASSERT
        Assert.NotNull(res)
        Assert.False(res.IsDisposed)
        Assert.Equal(expectedHeight, res.Height)
        Assert.Equal(expectedWidth, res.Width)

    [<Fact>]
    let ``Save -> creates file, does not dispose image`` () = 
        // ARRANGE
        let url = Helpers.GetImageUrl 200 300
        use image = GetImageFromUrl url |> Async.RunSynchronously

        // ACT
        Save(image, outputLocation)

        // ASSERT
        Assert.NotNull(image)
        Assert.False(image.IsDisposed)
        Assert.True(File.Exists(outputLocation))

