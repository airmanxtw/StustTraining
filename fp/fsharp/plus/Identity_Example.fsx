#r "nuget:FSharpPlus"

open FSharpPlus
open FSharpPlus.Data


let x = Identity 10
let y = x |> map (fun v -> v + 5)

printfn "Identity Example: %A" y
