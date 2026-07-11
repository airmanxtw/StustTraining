#r "nuget:FSharpPlus"

open FSharpPlus
open FSharpPlus.Data
open System

type ObjectError =
    | IsEmpty of string
    | IsTooLong of string * int
    | IsTooShort of string * int

type Man = { Name: string; Age: int }


let checkSpace (s: string) =
    if String.IsNullOrWhiteSpace s then
        Failure [ IsEmpty "String is empty" ]
    else
        Success s

let checkLength (s: string) =
    if s.Length < 3 then Failure [ IsTooShort(s, 3) ]
    elif s.Length > 10 then Failure [ IsTooLong(s, 10) ]
    else Success s

let createMan name age =
    (fun n a -> { Name = n; Age = a }) <!> checkSpace name *> checkLength name
    <*> Success age

let result = createMan "J" 30

match result with
| Success man -> printfn "Created man: %A" man
| Failure errors ->
    printfn
        "Failed to create man due to
    the following errors:"

    errors |> List.iter (printfn "%A")
