#r "nuget:FSharpPlus"

open FSharpPlus
open FSharpPlus.Data

let plusOne x =
    if x > 0 then
        Ok(x + 1)
    else
        Error "Input must be greater than 0"

let doubleValue () =
    Ok(fun x -> x * 2) |> Result.mapError (fun _ -> "Failed to create function")

let result = Result.apply (doubleValue ()) (plusOne 5)

printfn "Result: %A" (Result.get result)
