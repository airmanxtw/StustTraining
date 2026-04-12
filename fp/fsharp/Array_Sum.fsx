// Summing the elements of an array

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
