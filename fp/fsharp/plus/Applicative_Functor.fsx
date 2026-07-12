#r "nuget:FSharpPlus"

open FSharpPlus
open FSharpPlus.Data
open FSharpPlus.Lens


let f1 = Some(fun x -> x + 1)
let v1 = Some 5

f1 <*> v1
|> function
    | Some result -> printf "The result is %d\n" result
    | None -> printf "No result\n"

let a1 =
    applicative {
        let! x = Some 5
        and! y = None
        return x + y
    }

match a1 with
| Some result -> printf "The result is %d\n" result
| None -> printf "No result\n"
