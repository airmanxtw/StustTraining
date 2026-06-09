#r "nuget:FSharpPlus"
open FSharpPlus
open FSharpPlus.Data
open FSharpPlus.Lens

let r1 = result 10 |> Async.map (fun x -> x * 2)

let r2 = result 10 |> List.map (fun x -> x * 2)





