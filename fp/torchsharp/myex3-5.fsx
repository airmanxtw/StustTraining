#r "nuget: TorchSharp-cpu, 0.102.0"
#r "nuget: TorchVision, 0.102.0"
#r "nuget: Plotly.NET"
#r "nuget: Deedle"
#r "nuget: Plotly.NET"

open System
open System.Globalization
open System.IO
open TorchSharp
open Plotly.NET
open Deedle
open type TorchSharp.torch
open type TorchSharp.torchvision


// FashionMNIST already returns float32 values divided by 256; adjust to match Python ToTensor's /255.
let trans =
    transforms.Lambda(
        Func<Tensor, Tensor>(fun image -> image * (256.0 / 255.0)))

let dataRoot = Path.Combine(__SOURCE_DIRECTORY__, "data")
let mnistTrain =
    datasets.FashionMNIST(
        root = dataRoot,
        train = true,
        download = true,
        target_transform = trans)

let mnistTest =
    datasets.FashionMNIST(
        root = dataRoot,
        train = false,
        download = true,
        target_transform = trans)

printfn "mnistTrain: %d" mnistTrain.Count
printfn "mnistTest: %d" mnistTest.Count
