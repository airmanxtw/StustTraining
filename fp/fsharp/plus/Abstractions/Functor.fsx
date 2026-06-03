#r "nuget:FSharpPlus"

open FSharpPlus
open FSharpPlus.Data
open FSharpPlus.Lens


let f1 =
    fun i -> i + 1
    |> map (fun j -> j * 2)

printfn "Result: %A" (f1 5)
