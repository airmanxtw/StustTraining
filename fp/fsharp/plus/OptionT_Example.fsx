#r "nuget:FSharpPlus"

open FSharpPlus
open FSharpPlus.Data
open FSharpPlus.Lens

module OptionTExample =
    let add a b = Ok(Some(a + b))

    let add2 a b = Ok(Ok(Some(a + b)))


    let result () =
        let t = OptionT(add 5 10)

        let t2 =
            monad {
                let! x = t

                return x + 1
            }

        t2

    let result2 () =
        monad {
            let! x = add 1 2
            let! y = add 3 4


            let z =
                monad {
                    let! a = x
                    let! b = y
                    return a + b
                }

            z
        }

    OptionT.run (result ())
    |> function
        | Ok(Some v) -> printfn "Result: %A" v
        | Ok None -> printfn "Result: None"
        | Error e -> printfn "Error: %A" e

    result2 ()
    |> function
        | Ok(Some v) -> printfn "Result2: %A" v
        | Ok None -> printfn "Result2: None"
        | Error e -> printfn "Error: %A" e
