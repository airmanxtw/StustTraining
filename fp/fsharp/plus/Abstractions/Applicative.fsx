#r "nuget:FSharpPlus"

open FSharpPlus
open FSharpPlus.Data
open FSharpPlus.Lens

let r1 = result 10 |> Async.map (fun x -> x * 2)

let r2 = result 10 |> List.map (fun x -> x * 2)

let data1 = [ 1; 2; 3 ]
let data2 = [ 4; 5; 6; 7 ]

let result1 = (fun x y -> x + y) <!> data1 <*> data2

printfn "Result1: %A" result1

let result2 = (fun x y -> x + y) <!> data1 <.> data2
printfn "Result2: %A" result2
