// IF
let TRUE x = fun y -> x
let FALSE x = fun y -> y
let IF cond = fun x -> fun y -> cond x y

printfn "Identity Example with IF: %A" (IF TRUE 1 0)

//Add
let add x = fun y -> x + y
printfn "Identity Example with Add: %A" (add 5 10)
