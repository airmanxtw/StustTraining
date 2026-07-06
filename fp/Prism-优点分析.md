# Prism 的优点（对比传统方式）

你的疑惑是合理的。让我通过**对比**来展示 Prism 真正的价值。

## 问题场景：处理嵌套的 API 响应

```fsharp
type ApiResponse = 
    | Success of { data: string option }
    | Error of string

let responses = [
    Success { data = Some "Hello" }
    Success { data = None }
    Error "Failed"
]
```

## ❌ 传统方式（模式匹配）

```fsharp
// 每次都要重复这个逻辑
let extractData response =
    match response with
    | Success { data = Some x } -> Some x
    | _ -> None

// 修改数据要再写一遍
let updateData newValue response =
    match response with
    | Success r -> Success { r with data = Some newValue }
    | Error e -> Error e

// 查询满足条件的数据
let filtered = responses 
    |> List.choose extractData  // 需要单独写 choose 函数
```

**问题：**
- 逻辑重复（提取、修改各写一遍）
- 如果结构变了，到处都要改
- 嵌套多层时，模式匹配会很复杂

## ✅ Prism 方式

```fsharp
open FSharpPlus.Optics

// 定义一次，到处复用
let successDataPrism = 
    Prism.prism 
        (fun x -> Success { data = Some x })
        (function Success { data = Some x } -> Some x | _ -> None)

// 现在所有操作都用这个 Prism
let filtered = responses 
    |> List.choose (successDataPrism |> Optic.getOption)

let updated = responses 
    |> List.map (successDataPrism |> Optic.set "Updated")
```

## 🎯 真正的优点：**可组合性**

这是 Prism 最强大的地方：

```fsharp
// 定义基础 Prism（小的、可复用的）
let _Success = Prism.prism Success (function Success x -> Some x | _ -> None)
let _Data = Prism.prism (fun x -> { data = x }) (fun r -> r.data)
let _Some = Prism.some

// 组合起来处理复杂情况（无需重写逻辑！）
let deepAccess = _Success >> _Data >> _Some

// 直接用组合后的 Prism
let allData = responses |> List.choose (deepAccess |> Optic.getOption)

// 批量修改深层数据
let allUpdated = responses |> List.map (deepAccess |> Optic.set "New")
```

## 对比总结

| 方面 | 传统模式匹配 | Prism |
|------|-----------|-------|
| **嵌套层数多** | 模式会很长，难读 | 简单组合，清晰 |
| **代码复用** | 每个操作重写 | 定义一次，复用 |
| **修改结构** | 要改很多地方 | 只改 Prism 定义 |
| **链式操作** | 困难 | 自然 |

## 简单类比

想象你要在文件夹里找特定的文件：

- **模式匹配**：每次都要打开文件夹 → 检查是否是你要的 → 打开子文件夹...（重复）
- **Prism**：定义一个"路径规则"，可以反复查询、修改，不用每次都重新定义

**如果你的数据结构简单且不嵌套，Prism 确实没太大优势。但当结构复杂、需要多次访问或修改时，Prism 的优势就凸显出来了。**
