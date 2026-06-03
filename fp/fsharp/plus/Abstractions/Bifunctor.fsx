#r "nuget:FSharpPlus"

open FSharpPlus
open FSharpPlus.Data
open FSharpPlus.Lens

(1, "one")
|> bimap (fun x -> x + 1) (fun (s: string) -> s.ToUpper())
|> printfn "Bimap Result: %A"


let f1 v =
    if v > 0 then
        Ok(v + 1)
    else
        Error "Input must be greater than 0"
    |> bimap (fun e -> "Error: " + e) (fun x -> x * 2)
    |> printfn "Bimap Result: %A"

(1, "one") |> first (fun (x: int) -> x + 1) |> printfn "First Result: %A"

(1, "one")
|> second (fun (s: string) -> s.ToUpper())
|> printfn "Second Result: %A"
