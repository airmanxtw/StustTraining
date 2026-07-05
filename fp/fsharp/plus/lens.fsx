#r "nuget:FSharpPlus"

open FSharpPlus
open FSharpPlus.Data
open FSharpPlus.Lens

type Dept = { Name: string; Depts: Dept list }

let company =
    [ { Name = "HR"; Depts = [] }
      { Name = "IT"
        Depts = [ { Name = "Support"; Depts = [] }; { Name = "Development"; Depts = [] } ] } ]

// 取得部門名稱為Development的部門,用Lens




let t4 =
    [| "Something"; "Nothing"; "Something Else"; "Nothing" |]
    |> view (_all "Something")

printfn "Departments with name 'Development': %A" t4


// 第二個例子
type Person = { Name: string }
type SuperMan = { Person: Person }

let inline _name f s =
    f s.Name <&> fun x -> { s with Name = x }

let inline _person f s =
    f s.Person <&> fun x -> { s with Person = x }


let person = { Name = "Clark Kent" }
let superman = { Person = person }

printfn "change name: %A" (setl (_person << _name) "Superman" superman)

// 第三個例子
type Student = { no: string; name: string }
type Class = { students: Student list }

let inline _studentName f s =
    f s.name <&> fun x -> { s with name = x }

let inline _class no f s =
    let student = List.find (fun s -> s.no = no) s.students

    f student
    <&> fun x ->
        let updatedStudents = s.students |> List.map (fun s -> if s.no = no then x else s)
        { s with students = updatedStudents }

let class1 =
    { students =
        [ { no = "001"; name = "Alice" }
          { no = "002"; name = "Bob" }
          { no = "003"; name = "Charlie" } ] }

printfn "change student name: %A" (setl (_class "002" << _studentName) "Bobby" class1)

printfn "get student name: %A" (view (_class "003" << _studentName) class1)
