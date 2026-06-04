#r "nuget:FSharpPlus"

open FSharpPlus
open FSharpPlus.Data
open FSharpPlus.Lens

let w = (1, "one")

let result = extract w

let f1 = lazy (100)

let resultf1 = extract f1
