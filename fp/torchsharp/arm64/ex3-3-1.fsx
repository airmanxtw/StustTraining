#r "nuget: TorchSharp-cpu, 0.107.0"
#r "nuget: libtorch-cpu-osx-arm64, 2.10.0"
#r "nuget: Plotly.NET"

open System
open System.IO
open System.Runtime.InteropServices
open TorchSharp
open type TorchSharp.torch

let nugetPackages =
    match Environment.GetEnvironmentVariable "NUGET_PACKAGES" with
    | null
    | "" -> Path.Combine(Environment.GetFolderPath Environment.SpecialFolder.UserProfile, ".nuget", "packages")
    | path -> path

Path.Combine(
    nugetPackages,
    "libtorch-cpu-osx-arm64",
    "2.10.0",
    "runtimes",
    "osx-arm64",
    "native",
    "libomp.dylib"
)
|> NativeLibrary.Load
|> ignore

let genData (w: torch.Tensor) b num_examples =
    let X =
        torch.normal (0.0, 1.0, size = System.ReadOnlySpan<int64> [| num_examples; w.numel () |], dtype = w.dtype)

    let y = matmul (X, w) + b
    let noise = randn_like y * 0.01

    X, y + noise

let trueWeight = torch.tensor [| 2.0f; -3.4f |]
let trueBias = 4.2f
let data = genData trueWeight trueBias 1000
let features, rawLabels = data
let labels = rawLabels.reshape(-1L, 1L)

let loadArray (dataArrays: torch.Tensor array) batchSize =
    let dataset = utils.data.TensorDataset dataArrays
    torch.utils.data.DataLoader(dataset, batchSize, shuffle = true)

let dataLoader = loadArray [| features; labels |] 10

let linear = torch.nn.Linear(2, 1)
let net = linear |> torch.nn.Sequential

torch.nn.init.normal_(linear.weight, 0.0, 0.01) |> ignore
torch.nn.init.zeros_ linear.bias |> ignore

let loss = torch.nn.MSELoss()
let optimizer = torch.optim.SGD(net.parameters(), learningRate = 0.03)
let numEpochs = 10

for epoch in 1..numEpochs do
    net.train() |> ignore

    for batch in dataLoader do
        let batchFeatures = batch[0]
        let batchLabels = batch[1]
        use predictions = net.forward batchFeatures
        use batchLoss = loss.forward(predictions, batchLabels)

        optimizer.zero_grad()
        batchLoss.backward()
        optimizer.step() |> ignore

    net.eval() |> ignore
    use noGrad = torch.no_grad()
    use predictions = net.forward features
    use epochLoss = loss.forward(predictions, labels)
    printfn "epoch %d, loss %.6f" epoch (epochLoss.item<float32>())

let learnedWeightValues =
    use learnedWeight = linear.weight.detach().reshape(-1L)
    learnedWeight.data<float32>().ToArray()

let learnedBias = linear.bias.item<float32>()

printfn "true weight:    %A" (trueWeight.data<float32>().ToArray())
printfn "learned weight: %A" learnedWeightValues
printfn "true bias:      %.4f" trueBias
printfn "learned bias:   %.4f" learnedBias
