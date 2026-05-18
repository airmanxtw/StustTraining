#r "nuget:FSharpPlus"

open FSharpPlus
open FSharpPlus.Data

let add a = lazy (a + 1)

let double a = lazy (a * 2)

let result =
    monad {
        let! x = add 5
        let! y = double x
        return y
    }

printfn "The result of the lazy computation is: %d" result.Value
