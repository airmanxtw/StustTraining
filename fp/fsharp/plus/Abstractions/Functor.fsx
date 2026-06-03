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
