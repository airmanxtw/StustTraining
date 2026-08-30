#r "nuget:FSharpPlus"

open FSharpPlus
open FSharpPlus.Data
open FSharpPlus.Lens

let f1 = fun x -> x + 1
let f2 = fun x -> x * 2

let a1 = arr f1
let a2 = arr f2

let a12 = a1 *** a2


let r1 = first f1


printfn "Arrow Result1: %A" (r1 (1, 10))

printfn "Arrow Result2: %A" (a1 10)

printfn "Arrow Result3: %A" (a12 (1, 10))
