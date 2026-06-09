#r "nuget:FSharpPlus"

open FSharpPlus
open FSharpPlus.Data
open FSharpPlus.Lens

let f1 = Cont(fun (c: int -> int) -> c 10)
let f2 = Cont(fun (c: int -> int) -> c 20)

let f1Result = Cont.run f1 id

printfn "%A" f1Result
