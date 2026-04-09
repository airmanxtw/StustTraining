// box is a function that takes a value of any type and returns an object that contains that value.
let echo count =
    match count with
    | _ when count > 3 -> box {|count=count|}
    | _ -> box count

printfn "%A" (echo 10)