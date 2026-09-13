#r "nuget:FSharpPlus"

open FSharpPlus
open FSharpPlus.Data
open FSharpPlus.Lens
open FSharpPlus.Data
open Microsoft.FSharp.Collections

let v = (+) 1 1
printfn "%A" v

let v1 = (+) zero 2
printfn "%A" v1

let v2 = (++) [ 1; 2; 3 ] [ 3 ]
printfn "%A" v2

let v3 = (++) (Some 10) (Some 1)
printfn "%A" v3

let v4 = (++) None (Some 5)
printfn "%A" v4
