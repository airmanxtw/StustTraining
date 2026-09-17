#r "nuget:FSharpPlus"

open FSharpPlus
open FSharpPlus.Data
open FSharpPlus.Lens
open FSharpPlus.Internals
open FSharpPlus.Control


let f1 (x: string) = x.ToString().Length

let result = contramap f1 (fun i -> i.ToString())

let result2 = f1 >> fun i -> i.ToString()

printfn "%A" (result "hello")

let rule1 = contramap id (fun i -> i.ToString()) 3 = id "3"
printfn "rule1:%A" rule1



let rule2_1 =
    let g = fun y -> y + 1
    let z = fun x -> x * 2
    (contramap g << contramap z) (fun i -> i.ToString())

let rule2_2 = contramap ((fun x -> x * 2) << fun y -> y + 1) (fun i -> i.ToString())
printfn "%A" (rule2_1 10)
printfn "%A" (rule2_2 10)
