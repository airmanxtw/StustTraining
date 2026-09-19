Contravariant Functor 用在「你不是產生資料，而是在消費／檢查資料」的情境。
它讓你可以把一個已存在的檢查器、比較器或 formatter，透過「先取出其中某個欄位」重複使用在更大的型態上。

最典型的型態是：

'T -> bool

也就是  Predicate<'T> ：它吃進一個  'T ，判斷是否符合條件。

────────────────────

假設你已經有一個判斷整數是否為負數的規則：

open System

let isNegative =
    Predicate<int>(fun n -> n < 0)

它只能判斷  int ：

isNegative.Invoke(-100) // true
isNegative.Invoke(50)   // false

但系統裡有  Person ：

type Person =
    {
        Name: string
        Balance: int
    }

你想判斷某人是否透支。最直接的寫法是重新寫規則：

let isOverdrawn =
    Predicate<Person>(fun person -> person.Balance < 0)

這沒問題，但你把「負數」這條既有規則重寫了。

使用  contramap ，你保留原規則，只提供  Person -> int  的投影：

let balance (person: Person) =
    person.Balance

let isOverdrawn =
    FSharpPlus.contramap balance isNegative

概念等同於：

let isOverdrawn =
    Predicate<Person>(fun person ->
        isNegative.Invoke(person.Balance))

使用：

let alice = { Name = "Alice"; Balance = -500 }
let bob = { Name = "Bob"; Balance = 1000 }

isOverdrawn.Invoke(alice) // true
isOverdrawn.Invoke(bob)   // false

 contramap balance  做的事是：在資料送進既有規則之前，先把  Person  轉成  Balance 。

────────────────────

實際情境 1：輸入驗證

先定義一個 Email 格式檢查：

let containsAtSign =
    Predicate<string>(fun value -> value.Contains("@"))

資料模型：

type Registration =
    {
        Email: string
        Password: string
    }

不必重新寫 email 規則：

let registrationHasValidEmail =
    FSharpPlus.contramap (fun registration -> registration.Email) containsAtSign

這能讓同一個  Predicate<string>  用在：

type LoginRequest =
    {
        Email: string
        Password: string
    }

let loginHasValidEmail =
    FSharpPlus.contramap (fun request -> request.Email) containsAtSign

規則仍只有一份；差別只在「怎麼從外層資料取出 Email」。

────────────────────

實際情境 2：排序／比較器

 IComparer<'T>  也是 contravariant，因為它消費兩個  'T  來比較：

IComparer<'T>

你可以先有一個  string  比較器：

open System.Collections.Generic

let caseInsensitiveStringComparer =
    StringComparer.OrdinalIgnoreCase

定義資料：

type Product =
    {
        Id: int
        Name: string
    }

想依商品名稱排序，核心概念是把  Product  投影為  Name ：

let compareProductByName =
    Comparer<Product>.Create(fun x y ->
        caseInsensitiveStringComparer.Compare(x.Name, y.Name))

使用 FSharpPlus 的  contramap  時，概念可寫成：

let compareProductByName =
    FSharpPlus.contramap (fun product -> product.Name) caseInsensitiveStringComparer

你重用的是「字串比較規則」，只更換  Product -> string  這個取值方式。

────────────────────

實際情境 3：去重／判斷相等

 IEqualityComparer<'T>  也可以 contramap。

例如先有一個不分大小寫的字串相等比較器：

open System

let emailComparer =
    StringComparer.OrdinalIgnoreCase

資料：

type User =
    {
        Id: int
        Email: string
    }

你要依 email 去重，而不是依整個  User  record：

let userEmailComparer =
    FSharpPlus.contramap (fun user -> user.Email) emailComparer

概念上等於：

let userEmailComparer =
    { new System.Collections.Generic.IEqualityComparer<User> with
        member _.Equals(left, right) =
            String.Equals(
                left.Email,
                right.Email,
                StringComparison.OrdinalIgnoreCase)

        member _.GetHashCode(user) =
            StringComparer.OrdinalIgnoreCase.GetHashCode(user.Email) }

然後：

let users =
    [
        { Id = 1; Email = "Alice@example.com" }
        { Id = 2; Email = "alice@EXAMPLE.com" }
        { Id = 3; Email = "bob@example.com" }
    ]

let uniqueUsers =
    users
    |> Seq.distinctWith userEmailComparer

結果只保留兩個不同 email 的使用者。

────────────────────

為何叫「反變」？

對一般 Functor，例如  List<'T> ：

map : ('T -> 'U) -> List<'T> -> List<'U>

你把容器裡面的輸出值從  T  轉成  U 。

[1; 2; 3]
|> List.map string
// ["1"; "2"; "3"]

但  Predicate<'T>  的  T  是輸入：

Predicate<'T> = 'T -> bool

如果你原本有：

Predicate<int>

而你想得到：

Predicate<Person>

你不能用  int -> Person ；因為 predicate 需要的是  int ，不是  Person 。

你需要提供反方向：

Person -> int

也就是：

contramap : ('U -> 'T) -> Predicate<'T> -> Predicate<'U>

原本規則：          int ──────> bool
                         isNegative

你提供：      Person ──────> int
                        balance

組合後：      Person ──────> bool
                        isOverdrawn

所以它的核心價值可以濃縮成一句：

把針對小型資料寫好的「消費規則」，藉由欄位投影，重用到更大型的資料結構。
