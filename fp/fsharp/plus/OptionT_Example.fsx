#r "nuget:FSharpPlus"

open FSharpPlus
open FSharpPlus.Data
open FSharpPlus.Lens

module OptionTExample =
    let add a b = Ok(Some(a + b))


    let result() =
        let t = OptionT(add 5 10)

        let t2 =
            monad {
                let! x = t
                return x + 1
            }

        t2

    OptionT.run (result())
    |> function
        | Ok(Some v) -> printfn "Result: %A" v
        | Ok None -> printfn "Result: None"
        | Error e -> printfn "Error: %A" e
