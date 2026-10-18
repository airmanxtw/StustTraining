#r "nuget: TorchSharp-cpu, 0.102.0"

open TorchSharp
open type TorchSharp.torch

let genData (w: torch.Tensor) b num_examples =
    let X =
        torch.normal (0.0, 1.0, size = System.ReadOnlySpan<int64> [| num_examples; w.numel () |], dtype = w.dtype)

    let y = matmul (X, w) + b
    let noise = randn_like y * 0.01

    X, y + noise

let w = torch.tensor [| 2.0; -3.4 |]
let b = torch.tensor 4.2

let n = 1000
let X, y = genData w b n
printfn "%A" X
y.print ()
