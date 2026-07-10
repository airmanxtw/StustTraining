#r "nuget:FSharpPlus"

open FSharpPlus
open FSharpPlus.Data
open FSharpPlus.Lens


let f1 =
    fun i -> i + 1
    |> map (fun j -> j * 2)

printfn "Result: %A" (f1 5)

let data = [ (1, "one"); (2, "two"); (3, "three") ]

let f2 = data |> unzip

printfn "Unzipped: %A" f2

let data2 = [ 1; 2; 3; 4; 5 ]

let data2Result1 = (|>>) data2 (fun x -> x * 2)
let data2Result2 = (<<|) (fun x -> x + 1) data2
let data2Result3 = (<!>) (fun y -> y * 3) ((<!>) (fun x -> x - 1) data2)

printfn "Data2 Result1: %A" data2Result1
printfn "Data2 Result2: %A" data2Result2
printfn "Data2 Result3: %A" data2Result3
