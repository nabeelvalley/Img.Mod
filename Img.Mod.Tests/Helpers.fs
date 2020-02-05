namespace Img.Mod.Tests

open System.IO
open System

module Helpers = 

    let GetImageUrl height width = 
        "https://source.unsplash.com/random/" + width.ToString() + "x" + height.ToString()

    let GetOutputLocation () = Path.GetTempPath() + Guid.NewGuid().ToString() + ".jpg"
