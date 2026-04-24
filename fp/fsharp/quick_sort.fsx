let rec quicksort data =
    match data with
    | [] -> []
    | pivot :: tail ->
        let lessThanPivot = List.filter (fun x -> x < pivot) tail
        let greaterThanPivot = List.filter (fun x -> x >= pivot) tail
        quicksort lessThanPivot @ [ pivot ] @ quicksort greaterThanPivot

printfn "Sorted list: %A" (quicksort [ 3; 6; 8; 10; 1; 2; 1 ])
