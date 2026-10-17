#r "nuget: TorchSharp-cpu, 0.102.0"

open TorchSharp
open type TorchSharp.torch

let genData w b n =
    let X = torch.normal (0.0, 1.0, size = System.ReadOnlySpan<int64> [| 3L; 4L |])
    let (y: torch.Tensor) = torch.matmul (X, w) + b
    y

let w = torch.tensor ([| 2.0; -3.4 |])
let b = torch.tensor 4.2

let n = 1000
let y = genData w b n
y.print ()
