#r "nuget:FSharpPlus"

open FSharpPlus
open FSharpPlus.Data
open FSharpPlus.Lens
open FSharpPlus.Internals
open FSharpPlus.Control


let f1 (x: string) = x.ToString().Length

let result = contramap f1 (fun i -> i.ToString())

let result2 = f1 >> fun i -> i.ToString()

printfn "%A" (result "hello")
