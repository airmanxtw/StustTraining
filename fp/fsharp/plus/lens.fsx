#r "nuget:FSharpPlus"

open FSharpPlus
open FSharpPlus.Data
open FSharpPlus.Lens

type Dept = {
    Name: string
    Depts: Dept list
}

let company = [
    { Name = "HR"; Depts = [] }
    { Name = "IT"; Depts = [
        { Name = "Support"; Depts = [] }
        { Name = "Development"; Depts = [] }
    ] }
]

// 取得部門名稱為Development的部門,用Lens




let t4 = [|"Something"; "Nothing"; "Something Else"; "Nothing"|] |> view  (_all "Something")

printfn "Departments with name 'Development': %A" t4

