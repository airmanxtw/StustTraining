#r "nuget:FSharpPlus"

open FSharpPlus
open FSharpPlus.Data
open FSharpPlus.Lens


let f x = if x > 0 then [ x ] else []

let ft = traverse f [ 1; 2; 3; 4; 5 ]

printfn "%A" ft
