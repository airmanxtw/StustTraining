#r "nuget:FSharpPlus"

open FSharpPlus
open FSharpPlus.Data
open FSharpPlus.Lens

let f1 (x: int) = x.ToString()

let f2 = lmap (fun (i: string) -> i.Length) f1

printfn "%A" (f2 "12345")
