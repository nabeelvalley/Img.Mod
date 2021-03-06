﻿namespace Img.Mod.Web.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Img.Mod.Lib
open System.IO

type ImageResult = 
    | Error of string
    | Bytes of byte[]

[<ApiController>]
[<Route("Resize")>]
type ResizeController (logger : ILogger<ResizeController>) =
    inherit ControllerBase()

    [<HttpGet>]
    /// Get and resize the provided image to the given dimension. Prioritized width over height
    member this.Get (src: string, width: int, height: int) = 
        async {
          
            let res: ImageResult = 
                match (String.IsNullOrEmpty src) with
                    | true -> Error "Image source is required"
                    | false -> 
                        use image = FileHandler.GetImageFromUrl src 
                                    |> Async.RunSynchronously

                        match (width > 0) with
                        | true -> image 
                                  |> ImageResize.ResizeImageByWidth width 
                                  |> Async.RunSynchronously 
                                  |> FileHandler.GetBytesFromImage|> Bytes
                        | false -> 
                            match (height > 0) with
                            | true -> image
                                      |> ImageResize.ResizeImageByHeight height
                                      |> Async.RunSynchronously 
                                      |> FileHandler.GetBytesFromImage|> Bytes

                            | false -> Error "Height or Width are required"

            return match res with 
                   | Error e -> this.BadRequest(e) :> IActionResult
                   | Bytes b -> FileContentResult(b, "image/jpeg") :> IActionResult
        }