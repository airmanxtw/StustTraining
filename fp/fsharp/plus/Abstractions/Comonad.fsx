#r "nuget:FSharpPlus"

open FSharpPlus
open FSharpPlus.Data
open FSharpPlus.Lens
open FSharpPlus.Internals
open FSharpPlus.Control

let w = (1, "one")

let result = extract w

let f1 = lazy (100)

let resultf1 = extract f1

let z = (10, "a")


let zresult = duplicate z

printfn "%A" zresult

let r1 = Reader(fun (x: int) -> x + 1)

let r2 = extract r1

printfn "%A" r2
