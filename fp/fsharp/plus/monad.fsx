#r "nuget:FSharpPlus"

open FSharpPlus
open FSharpPlus.Data
open FSharpPlus.Lens



monad.plus {
    let! x = [ 1; 2; 3 ]
    let! y = [ 4; 5 ]
    return x + y
}
|> printfn "Monad test1 result: %A"




monad.plus {
    let! x = [ 1; 2; 3 ]
    let! y = [ 4; 5 ]

    if x + y > 6 then
        return x + y
}
|> printfn "Monad test2 result: %A"


monad.plus {
    let! x = [ 1; 2; 3 ]
    let! y = [ 4; 5 ]

    if x + y > 6 then return x + y else return! []
}
|> printfn "Monad test3 result: %A"

monad.plus {
    let! x = [ 1; 2; 3 ]
    let! y = [ 4; 5 ]

    do! guard (x + y > 6)
    return x + y
}
|> printfn "Monad test4 result: %A"

monad.plus {
    let! x = Ok 5
    let! y = Ok 10

    if x + y > 12 then
        return x + y
    else
        return! Error "Sum is too small"
}
|> printfn "Monad test5 result: %A"
