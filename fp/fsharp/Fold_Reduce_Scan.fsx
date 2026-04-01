type Student ={
    No : int
    Name : string
    Score : int
}

let students = [
    {No = 1; Name = "Alice"; Score = 85}
    {No = 2; Name = "Bob"; Score = 90}
    {No = 3; Name = "Charlie"; Score = 78}
]

let totalScore studs = 
    studs
    |> List.fold (fun acc student -> acc + student.Score) 0

let getScore80 studs =
    studs
    |> List.reduce (fun acc student -> if student.Score >=80 then student else acc)

let getScore70s studs =
    studs
    |> List.scan (fun acc student -> if student.Score >=70  then student :: acc else acc) []

printfn "Total score of all students: %d" (totalScore students)
printfn "Student with score >= 80: %s with score %d" (getScore80 students).Name (getScore80 students).Score
printfn "Students with score >= 70: %A" (getScore70s students)
