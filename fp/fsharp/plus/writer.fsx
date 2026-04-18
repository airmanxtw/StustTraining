#r "nuget:FSharpPlus"
open FSharpPlus
open FSharpPlus.Data

let add x y = monad {    
    do! Writer.tell ["Added 1 to 100,000"]
    do! Writer.tell ["Added 2 to 100,000"]
    return x + y
}

let (result,log) = Writer.run (add 1 100000)

printfn "Result: %d" result
printfn "Log: %s" (String.concat "; " log)