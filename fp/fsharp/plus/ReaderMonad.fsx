#r "nuget:FSharpPlus"

open FSharpPlus
open FSharpPlus.Data
open FSharpPlus.Lens

let r1 x = Reader(fun (env: int) -> Ok(x + env))

let result =
    monad {
        let! x = r1 5

        let! y =
            x
            |> function
                | Ok v -> r1 v
                | Error e -> Reader(fun _ -> Error e)

        return y
    }
    |> Reader.run
    <| 10
    |> function
        | Ok v -> v
        | Error e -> 0


printfn "%A" result
