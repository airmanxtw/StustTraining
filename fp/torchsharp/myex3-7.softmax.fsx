#r "nuget: TorchSharp-cpu, 0.102.0"
#r "nuget: TorchVision, 0.102.0"

open System
open System.IO
open TorchSharp
open type TorchSharp.torch
open type TorchSharp.torchvision

let batchSize = 256
let dataRoot = Path.Combine(__SOURCE_DIRECTORY__, "data")

// FashionMNIST uses /256 internally; rescale it to match PyTorch ToTensor's /255.
let transform =
    transforms.Lambda(
        Func<Tensor, Tensor>(fun image -> image * (256.0 / 255.0)))

let trainDataset =
    datasets.FashionMNIST(
        root = dataRoot,
        train = true,
        download = true,
        target_transform = transform)

let testDataset =
    datasets.FashionMNIST(
        root = dataRoot,
        train = false,
        download = true,
        target_transform = transform)

let trainIter =
    torch.utils.data.DataLoader(
        trainDataset,
        batchSize,
        shuffle = true)

let testIter =
    torch.utils.data.DataLoader(
        testDataset,
        batchSize,
        shuffle = false)

printfn "Training samples: %d" trainDataset.Count
printfn "Test samples: %d" testDataset.Count

let linear = torch.nn.Linear(784, 10)
let net =
    torch.nn.Sequential(
        torch.nn.Flatten(),
        linear)

torch.nn.init.normal_(linear.weight, mean = 0.0, std = 0.01)
|> ignore

let loss = torch.nn.CrossEntropyLoss(reduction = torch.nn.Reduction.None)

let trainer = torch.optim.SGD(net.parameters(), learningRate = 0.1)

let countCorrect (predictions: Tensor) (labels: Tensor) =
    use predictedLabels = predictions.argmax(1L)
    use matches = predictedLabels.eq(labels)
    use correct = matches.sum()
    correct.item<int64>()

let evaluateAccuracy (model: nn.Module<Tensor, Tensor>) =
    model.eval() |> ignore
    use _ = torch.no_grad()

    let mutable correct = 0L
    let mutable total = 0L

    for batch in testIter do
        let features = batch.["data"]
        let labels = batch.["label"]
        use predictions = model.forward(features)
        correct <- correct + countCorrect predictions labels
        total <- total + labels.shape.[0]

    Convert.ToDouble(correct) / Convert.ToDouble(total)

let numEpochs = 10

for epoch in 1 .. numEpochs do
    net.train() |> ignore

    let mutable totalLoss = 0.0
    let mutable totalCorrect = 0L
    let mutable totalExamples = 0L

    for batch in trainIter do
        let features = batch.["data"]
        let labels = batch.["label"]
        use predictions = net.forward(features)
        use batchLoss = loss.forward(predictions, labels)
        use meanLoss = batchLoss.mean()

        trainer.zero_grad()
        meanLoss.backward()
        trainer.step() |> ignore

        totalLoss <-
            totalLoss + Convert.ToDouble(batchLoss.sum().item<float32>())
        totalCorrect <- totalCorrect + countCorrect predictions labels
        totalExamples <- totalExamples + labels.shape.[0]

    let trainLoss = totalLoss / Convert.ToDouble(totalExamples)
    let trainAccuracy =
        Convert.ToDouble(totalCorrect) / Convert.ToDouble(totalExamples)
    let testAccuracy = evaluateAccuracy net

    printfn
        "epoch %d, loss %.4f, train accuracy %.4f, test accuracy %.4f"
        epoch
        trainLoss
        trainAccuracy
        testAccuracy
