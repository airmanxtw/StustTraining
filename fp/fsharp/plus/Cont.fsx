#r "nuget:FSharpPlus"

open FSharpPlus
open FSharpPlus.Data
open FSharpPlus.Lens

let f1 = Cont(fun (c: int -> int) -> c 10)
let f2 = Cont(fun (c: int -> int) -> c 20)

let factorial n =
    Cont(fun (f: int -> int) -> if n <= 1 then 1 else n * f (n - 1))

let test2 = 
    Cont.callCC(fun k -> 
        factorial 5
        |> Cont.bind(fun x -> 
            if x>100 then k x else Cont.Return<int, int> (x-2)
        )  
        |> Cont.map(fun x -> x-1)  
    )
    
let f1Result = Cont.run (test2) id

printfn "Factorial  is: %d" f1Result
