#r "nuget:FSharpPlus"
#r "nuget:MathNet.Numerics"

open FSharpPlus
open FSharpPlus.Data
open FSharpPlus.Math.Applicative

open MathNet.Numerics.LinearAlgebra.Double


let a i = i + 1
let b i = i + 2

let c = b .+. a

printfn "c: %A" (c 1)







let add (a: int[]) (b: int[]) = a .+. b

//printfn "Add: %A" (add [| 1; 2; 3 |] [| 4; 5; 6 |])


let v = Vector.Build.DenseOfArray [| 4; 1 |]
let w = Vector.Build.DenseOfArray [| 2; -1 |]

let z = 2.0 * v - 3.0 * w

printfn "2v - 3w: %A" z


let o1 = Some 1
let o2 = Some 2

let oResult = plus o1 o2
