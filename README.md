# Img.Mod

An on-the-fly image modification API

## Functionality

1. Given a Source URL and new dimensions will return an resized version of the image and either the `width` or `height` in pixels. Width will take preference

A test request can be done by opening the following URL for an unsplash image in your browser:

```
http://localhost:5000/Resize/?src=https%3A%2F%2Fsource.unsplash.com%2Frandom&width=500
```

## Running the Application

The application can either be run using the `dotnet cli` or Visual Studio

To run with the `dotnet cli` run `dotnet run` from the `Img.Mod.Web` application, from Visual Studio just set it as the startup application
