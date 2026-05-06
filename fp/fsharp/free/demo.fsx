module FreeMonadDemo

// 這個範例刻意不做成「完全泛型」的 Free Monad，
// 而是做成最小可理解版本：
// 1. 先把程式描述成資料
// 2. 再用不同 interpreter 決定怎麼執行

type ConsoleProgram<'a> =
    | Pure of 'a
    | Ask of string * (string -> ConsoleProgram<'a>)
    | Print of string * (unit -> ConsoleProgram<'a>)

module ConsoleProgram =
    let ret x = Pure x

    let rec bind f program =
        match program with
        | Pure x ->
            f x
        | Ask (prompt, next) ->
            Ask (prompt, next >> bind f)
        | Print (message, next) ->
            Print (message, fun () -> next () |> bind f)

    type Builder() =
        member _.Return x = ret x
        member _.Bind (program, f) = bind f program
        member _.ReturnFrom program = program

    let free = Builder()

    let ask prompt = Ask (prompt, Pure)
    let print message = Print (message, fun () -> Pure ())

    let rec runIO program =
        match program with
        | Pure x ->
            x
        | Ask (prompt, next) ->
            printf "%s" prompt
            let input = System.Console.ReadLine()
            runIO (next input)
        | Print (message, next) ->
            printfn "%s" message
            runIO (next ())

    let runPure inputs program =
        let rec loop remainingInputs outputs current =
            match current with
            | Pure x ->
                x, List.rev outputs
            | Ask (prompt, next) ->
                match remainingInputs with
                | input :: rest ->
                    loop rest (("ASK: " + prompt + " -> " + input) :: outputs) (next input)
                | [] ->
                    failwith "runPure 需要足夠的假輸入資料"
            | Print (message, next) ->
                loop remainingInputs (("PRINT: " + message) :: outputs) (next ())

        loop inputs [] program

let program =
    ConsoleProgram.free {
        do! ConsoleProgram.print "歡迎來到超簡單 Free Monad 範例"
        let! name = ConsoleProgram.ask "請輸入名字："
        do! ConsoleProgram.print ("你好, " + name)
        return name.Length
    }

let fakeResult, fakeLogs =
    ConsoleProgram.runPure [ "FSharp" ] program

printfn "=== 用假 interpreter 執行 ==="
printfn "結果: %d" fakeResult
fakeLogs |> List.iter (printfn "%s")

printfn ""
printfn "=== 概念重點 ==="
printfn "program 只是描述，不是立刻做 I/O。"
printfn "同一份 program 可以交給不同 interpreter 執行。"
printfn "1. runIO   -> 真正讀寫主控台"
printfn "2. runPure -> 用假資料模擬，方便測試與理解"
