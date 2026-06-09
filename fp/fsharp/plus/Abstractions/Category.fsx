#r "nuget:FSharpPlus"
open FSharpPlus
open FSharpPlus.Data
open FSharpPlus.Lens

let c = catId()

let f1 = fun x -> x + 1
let f2 = fun x -> x * 2

let result1 = catComp f1 f2

let result2 = (<<<)

printfn "Category Identity: %A" result2



