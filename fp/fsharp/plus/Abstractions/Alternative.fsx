#r "nuget:FSharpPlus"
open FSharpPlus

let v1 () = Some 1

let v2 () =
    printfn "v2 called"
    Some 2

let result = v1 () <|> v2 ()

printfn "Result: %A" result


let v3 = empty<int option>
