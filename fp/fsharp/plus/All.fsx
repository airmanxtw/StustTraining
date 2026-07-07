#r "nuget:FSharpPlus"

open FSharpPlus
open FSharpPlus.Data

let data = [ 1; 2; 3; 4; 5 ]

let result = All true

let (All b) = result

if b then
    printfn "All values are true"
else
    printfn "Not all values are true"
