#r "nuget:FSharpPlus"

open FSharpPlus
open FSharpPlus.Data
open FSharpPlus.Lens


let f1 (x: int) (y: string) = x.ToString() + y

let f2 = (/>) f1

let f3 = (</) 10 (fun i -> i + 1)

let f4 (x: string) = x |- (printf "%s")
