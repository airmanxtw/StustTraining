#r "nuget:FSharpPlus"

open FSharpPlus
open FSharpPlus.Data
open FSharpPlus.Lens

let e0:Result<int,string> = Error "Negative value"
let r0 v = if v > 0 then Ok v else Error "Negative value"

let r1 x = Reader(fun (env: int) -> r0 x)

let r1' (x:int) = r1 10 |> Reader.bind (fun r ->
    match r with
    | Ok v -> r1 v
    | Error e -> 
        let re = Reader(fun _ -> Error e)
        re
)

let  t1 = Reader.Return<Result<int,string>,int> (Ok 10)
let r1'' (x:int) = monad{
    let! r = r1 10
    match r with
    | Ok v -> return! r1 v
    | Error e -> return! Reader.Return<Result<int,string>,int> (Error e)
}


let result =
    monad {
        let! x = r1 5

        let! y =
            x
            |> function
                | Ok v -> r1 v
                | Error e -> Reader(fun _ -> Error e)

        return y
    }
    |> Reader.run
    <| 10
    |> function
        | Ok v -> v
        | Error e -> 0

let result2 = monad {
    let! x = r1 5
    let! y = 
        Result.bimap (fun (e:string) -> Reader(fun _ -> Error e)) (fun v -> r1 v)  x

    
    
}


printfn "%A" result
