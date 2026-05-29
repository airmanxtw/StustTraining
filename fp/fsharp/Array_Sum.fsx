// Summing the elements of an array
#r "nuget:FSharpPlus"

open FSharpPlus
let rec sum1 data =
    match data with
    | [] -> 0
    | head :: tail -> head + sum1 tail

let rec sum2 data =
    match data with
    | [] -> 0
    | _ -> List.head data + sum2 (List.tail data)


printf "The sum of [1; 2; 3] is %d\n" (sum1 [ 1; 2; 3 ])
printf "The sum of [1; 2; 3] is %d\n" (sum2 [ 1; 2; 3 ])


[1;2;3] |> map (fun i -> i*2)
|> printfn "The double of [1; 2; 3] is %A"

[1;2;3] |> bind (fun i -> [i; i*2])
|> printfn "The bind of [1; 2; 3] is %A"

