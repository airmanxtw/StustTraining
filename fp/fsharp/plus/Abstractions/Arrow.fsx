#r "nuget:FSharpPlus"
open FSharpPlus
open FSharpPlus.Data
open FSharpPlus.Lens

let f1 = fun x -> x + 1
let f2 = fun x -> x * 2

let r1 = first f1


printfn "Arrow Result: %A" (r1 (1, 10))