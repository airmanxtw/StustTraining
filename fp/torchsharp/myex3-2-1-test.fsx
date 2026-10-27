#r "nuget: TorchSharp-cpu, 0.102.0"
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

let Y x = 3.5 * x ** 2.0 + 1.5 * x + 2.0

let loaddata () =
    let x = [| for i in 0..100 -> Convert.ToSingle i / 10.0f |] |> torch.tensor
    let y = 3.5f * x * x + 1.5f * x + 2.0f
    x, y

let x, y = loaddata ()
printfn "x shape: %A, y shape: %A" x.shape y.shape

let xMean = x.mean ()
let xStd = x.std ()
let xNormalized = (x - xMean) / xStd

let loadArray (dataArrays: torch.Tensor array) batchSize =
    let dataset = utils.data.TensorDataset dataArrays
    torch.utils.data.DataLoader(dataset, batchSize, shuffle = true)

let dataLoader = loadArray [| xNormalized; y |] 100

let linear = torch.nn.Linear(1, 1)
let net = linear |> torch.nn.Sequential

let loss = torch.nn.MSELoss()
let optimizer = torch.optim.SGD(net.parameters (), learningRate = 0.03)
let numEpochs = 1000

for epoch in 1..numEpochs do
    for batch in dataLoader do
        let xb = batch.[0].unsqueeze 1
        let yb = batch.[1].unsqueeze 1
        let pred = net.forward xb
        let l = loss.forward (pred, yb)
        optimizer.zero_grad ()
        l.backward ()
        optimizer.step () |> ignore

    let epochLoss = loss.forward (net.forward (xNormalized.unsqueeze 1), y.unsqueeze 1)

    printfn "Epoch %d, Loss: %A" epoch (epochLoss.item<float32> ())

net.eval ()

let predictedGpa =
    use _ = torch.no_grad ()
    use prediction = net.forward(xNormalized.unsqueeze (1)).squeeze ()
    prediction.data<float32>().ToArray()

let satValues = x.data<float32>().ToArray()
let gpaValues = y.data<float32>().ToArray()

let sortedSat, sortedPredictions =
    Array.zip satValues predictedGpa |> Array.sortBy fst |> Array.unzip

[ Chart.Point(x = satValues, y = gpaValues, Name = "樣本資料")
  Chart.Line(x = sortedSat, y = sortedPredictions, Name = "預測迴歸線") ]
|> Chart.combine
|> Chart.withTitle "線性迴歸"
|> Chart.withXAxisStyle "X"
|> Chart.withYAxisStyle "Y"
|> Chart.show
