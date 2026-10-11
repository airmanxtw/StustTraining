#r "nuget: TorchSharp-cpu, 0.102.0"

open TorchSharp
open type TorchSharp.torch

let x = torch.arange 12
printfn "x: %A" (x.data<int64>().ToArray())

let y = x.reshape (3, 4)
printfn "y: %A" y

let z = torch.randn 50
printfn "z: %A" (z.data<float32>().ToArray())

let z2 = torch.randn 50
printfn "z + z2: %A" ((z + z2).data<float32>().ToArray())
