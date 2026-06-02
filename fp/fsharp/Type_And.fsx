// 相互引用

type Student =
    | Name of string
    | Nickname of string
    | Info of int * int
    | D of Dept

and Dept =
    | Students of Student list
    | Stud of Student * int


let d1 = Students [ Name "Alice"; Nickname "Ally"; Info(20, 3) ]
