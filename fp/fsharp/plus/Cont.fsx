#r "nuget:FSharpPlus"

open FSharpPlus
open FSharpPlus.Data
open FSharpPlus.Lens

let f1 = Cont(fun (c: int -> int) -> c 10)
let f2 = Cont(fun (c: int -> int) -> c 20)

let factorial n =
    Cont(fun (f: int -> int) -> if n <= 1 then 1 else n * f (n - 1))

let test2 = factorial 5 |> Cont.bind (fun x -> factorial x)



let f1Result = Cont.run (test2) id

printfn "Factorial of 4 is: %d" f1Result
