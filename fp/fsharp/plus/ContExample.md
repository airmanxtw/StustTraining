# FSharpPlus 中的 `Cont<R,U>` 介绍

`Cont<R,U>` 是 FSharpPlus 提供的 **Continuation Monad**，让我详细说明如何使用。

## 基本概念

```fsharp
open FSharpPlus
open FSharpPlus.Data

// Cont<R,U> 的定义
// R: 最终结果类型
// U: 当前 continuation 将传递的值的类型
```

## 创建 Cont

### 方式 1：使用 `Cont` 构造函数

```fsharp
// 基本用法：Cont(fun (cont: U -> R) -> ...)
let hello = Cont(fun cont -> cont "Hello")

// 运行 continuation，使用 id 作为最终 continuation
let result = Cont.run hello id  // "Hello"
```

### 方式 2：使用 Monad Operations

```fsharp
let value x = Cont(fun cont -> cont x)

let x = value 42
let result = Cont.run x id  // 42
```

## Monad 操作

### Bind (>>=) - 链接操作

```fsharp
let getUser userId = Cont(fun cont -> 
    cont { id = userId; name = "Alice" }
)

let getOrders userId = Cont(fun cont ->
    cont [1; 2; 3]
)

// 使用 bind 链接
let workflow = monad {
    let! user = getUser 1
    let! orders = getOrders user.id
    return (user.name, List.length orders)
}

let result = Cont.run workflow id  // ("Alice", 3)
```

### Map 操作

```fsharp
let double = Cont(fun cont -> cont 5)
let mapped = double |> Cont.map (fun x -> x * 2)

Cont.run mapped id  // 10
```

## 实际例子：栈操作

```fsharp
// 模拟栈操作
let push x = Cont(fun cont ->
    let stack = [x]
    cont stack
)

let pop stack = Cont(fun cont ->
    match stack with
    | x::xs -> cont (x, xs)
    | [] -> cont (0, [])
)

let stackOps = monad {
    let! stack1 = push 10
    let! stack2 = push 20
    let! stack3 = push 30
    let! (top, rest) = pop stack3
    return (top, List.length rest)
}

Cont.run stackOps id  // (30, 2)
```

## 高级用法：改变结果类型

### callCC (Call with Current Continuation)

```fsharp
// 早期退出
let earlyExit = Cont(fun cont ->
    let escape = cont  // 保存当前 continuation
    // 可以在任何时候调用 escape 来提前返回
    escape 42
)

Cont.run earlyExit id  // 42
```

### 转换结果类型

```fsharp
let stringCont = Cont(fun cont -> cont 42)

// 将结果转为字符串
let asString = Cont.map string stringCont

Cont.run asString id  // "42"

// 或使用 Cont.mapCont 改变 continuation 本身
let modified = Cont.mapCont (fun f x -> f x + 100) stringCont
Cont.run modified id  // 142
```

## 实用例子：异常处理

```fsharp
let safeDivide a b = Cont(fun cont ->
    if b = 0 then
        cont (Error "Division by zero")
    else
        cont (Ok (a / b))
)

let calc = monad {
    let! result1 = safeDivide 10 2
    match result1 with
    | Ok value -> 
        let! result2 = safeDivide value 3
        return result2
    | Error msg -> 
        return Error msg
}

Cont.run calc id  // Ok 1 or Error message
```

## 关键方法总结

| 方法 | 说明 |
|------|------|
| `Cont.run cont finalCont` | 执行 continuation，提供最终 continuation |
| `Cont.map f cont` | 对结果应用函数 |
| `Cont.mapCont f cont` | 对 continuation 函数进行变换 |
| `Cont.bind f cont` | Monad bind 操作 |
| `Cont.return' x` | 包装一个值 |

## 为什么使用 Cont？

```fsharp
// 1. 控制流控制
// 2. 避免深层嵌套回调
// 3. 优雅的异步/事件处理
// 4. 性能优化（尾调用）

let complexFlow = monad {
    let! x = Cont(fun cont -> cont 5)
    let! y = Cont(fun cont -> cont 10)
    let! z = Cont(fun cont -> cont (x + y))
    return z * 2
}

Cont.run complexFlow id  // 30
```

## 关键要点

`Cont<R,U>` 让你以声明式方式处理复杂的控制流，避免了"回调地狱"，特别适合异步操作和高阶控制流。
