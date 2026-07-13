#r "nuget:FSharpPlus"

open FSharpPlus
open FSharpPlus.Data
open FSharpPlus.Lens


let data = (100, "Hello World")

let result = first (fun i -> i * 2) data

let result2 = fst data
