#r "nuget:FSharpPlus"

open FSharpPlus
open FSharpPlus.Data
open FSharpPlus.Lens

let f1 = Cont(fun (c: int -> int) -> c 10)
let f2 = Cont(fun (c: int -> int) -> c 20)

let factorial n =
    Cont(fun (f: int -> int) -> if n <= 1 then 1 else n * f (n - 1))



let f1Result = Cont.run (factorial 4)

printfn "%A" f1Result
