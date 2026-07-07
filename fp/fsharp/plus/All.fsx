#r "nuget:FSharpPlus"

open FSharpPlus
open FSharpPlus.Data

let data = [ 1; 2; 3; 4; 5; -1 ]

let result = data |> List.map (fun x -> All(x > 0)) |> sum

let (All b) = result

if b then
    printfn "All values are true"
else
    printfn "Not all values are true"
