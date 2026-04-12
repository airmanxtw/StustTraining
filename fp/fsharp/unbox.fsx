// This example demonstrates how to use the unbox function in F# to convert a boxed value back to its original type.
let test =
    let x = box 10
    let y = unbox<int> x
    y

printfn "The unboxed value is %d" test
