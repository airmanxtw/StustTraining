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



