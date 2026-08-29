# Arrow 操作详解 + 代码范例

## 🎯 概述

Arrow<'T, 'U> 表示一个 **过程**，接收类型为 'T 的输入，输出类型为 'U 的结果。

---

## 1️⃣ `arr` - 函数转换成 Arrow

**说明**：将普通函数 `'T -> 'U` 转换成 Arrow 形式

```fsharp
// 定义 Arrow 类型
type Arrow<'T, 'U> = Arrow of ('T -> 'U)

// 实现 arr
let arr f = Arrow f

// 范例
let addOne x = x + 1
let arrowAddOne = arr addOne  // 普通函数变成 Arrow

// 使用
let (Arrow f) = arrowAddOne
f 5  // -> 6
```

**为什么需要它**：统一函数和更复杂的计算过程的表示方式

---

## 2️⃣ `first` - 作用在元组第一个元素

**说明**：将 Arrow 只应用到元组的第一个元素，第二个元素保持不变

```fsharp
let first (Arrow f) = Arrow (fun (x, y) -> (f x, y))

// 范例：处理坐标
let addTen x = x + 10
let arrowAddTen = arr addTen

let firstAddTen = first arrowAddTen

// 使用
let (Arrow f) = firstAddTen
f (5, 100)  // -> (15, 100)
            // 第一个元素 5 变成 15，第二个 100 不变
```

**实际应用**：
```fsharp
// 场景：更新用户信息中的年龄，保留其他数据
let incrementAge x = x + 1
let arrowIncrementAge = arr incrementAge

let updateAgeKeepName = first arrowIncrementAge

// (name, age) -> (name, age+1)
let (Arrow f) = updateAgeKeepName
f ("Alice", 25)  // -> ("Alice", 26)
```

---

## 3️⃣ `second` - 作用在元组第二个元素

**说明**：与 `first` 相反，只改变元组的第二个元素

```fsharp
let second (Arrow f) = Arrow (fun (x, y) -> (x, f y))

// 范例
let double x = x * 2
let arrowDouble = arr double

let secondDouble = second arrowDouble

let (Arrow f) = secondDouble
f (5, 10)  // -> (5, 20)
           // 第一个元素 5 不变，第二个 10 变成 20
```

**实际应用**：
```fsharp
// 场景：保留用户ID，只修改邮箱
let normalizeEmail email = email.ToLower()
let arrowNormalizeEmail = arr normalizeEmail

let updateEmailKeepId = second arrowNormalizeEmail

let (Arrow f) = updateEmailKeepId
f (123, "Alice@GMAIL.COM")  // -> (123, "alice@gmail.com")
```

---

## 4️⃣ `***` - 并行处理两个 Arrow（重要！）

**说明**：分别对元组的两个元素应用两个不同的 Arrow

```fsharp
let (***) (Arrow f) (Arrow g) = 
    Arrow (fun (x, y) -> (f x, g y))

// 范例 1：基础用法
let addOne x = x + 1
let double x = x * 2

let arrowAddOne = arr addOne
let arrowDouble = arr double

let combined = arrowAddOne *** arrowDouble

let (Arrow f) = combined
f (5, 10)  // -> (6, 20)
           // 5 经过 addOne -> 6
           // 10 经过 double -> 20
```

**实际应用 - 多字段处理**：
```fsharp
// 场景 1：处理用户表单的多个字段
let trimString s = s.Trim()
let parseAge s = int s

let arrowTrimName = arr trimString
let arrowParseAge = arr parseAge

let processForm = arrowTrimName *** arrowParseAge

let (Arrow f) = processForm
f ("  Alice  ", "25")  // -> ("Alice", 25)

// 场景 2：异步 API 调用
type AsyncArrow<'T, 'U> = AsyncArrow of ('T -> Async<'U>)

let fetchUserAsync id = AsyncArrow (fun _ -> async { return User(id) })
let fetchPostsAsync id = AsyncArrow (fun _ -> async { return [Post()] })

let combinedAsync = fetchUserAsync *** fetchPostsAsync
// 同时获取 user 和 posts（并行！）
```

---

## 5️⃣ `&&&` - 扇形操作（Fan-out）

**说明**：从 **同一个输入** 分别通过两个 Arrow，得到两个输出的元组

```fsharp
let (&&&) (Arrow f) (Arrow g) = 
    Arrow (fun x -> (f x, g x))

// 范例：同一个数计算两个结果
let addOne x = x + 1
let double x = x * 2

let arrowAddOne = arr addOne
let arrowDouble = arr double

let fanOut = arrowAddOne &&& arrowDouble

let (Arrow f) = fanOut
f 5  // -> (6, 10)
     // 5 经过 addOne -> 6
     // 5 经过 double -> 10
     // 都用同一个输入！
```

**实际应用 - 数据分析**：
```fsharp
// 场景 1：从同一个数据计算多个指标
let calculateSum items = List.sum items
let calculateAverage items = (List.sum items) / (List.length items)
let calculateMax items = List.max items

let arrowSum = arr calculateSum
let arrowAvg = arr calculateAverage
let arrowMax = arr calculateMax

let analyzeData = arrowSum &&& arrowAvg &&& arrowMax

let (Arrow f) = analyzeData
f [1; 2; 3; 4; 5]  // -> (15, 3, 5)
                    // 和为 15，平均 3，最大 5

// 场景 2：验证用户输入，同时返回原始值和验证结果
let validateEmail email = 
    email.Contains("@")

let userInput = "alice@example.com"

let keepOriginal = arr id  // 保持原始值
let validateIt = arr validateEmail

let processUser = keepOriginal &&& validateIt

let (Arrow f) = processUser
f userInput  // -> ("alice@example.com", true)
```

---

## 6️⃣ `>>>` - Arrow 组合（顺序执行）

**说明**：将两个 Arrow 连接起来，前一个的输出是后一个的输入

```fsharp
let (>>>) (Arrow f) (Arrow g) = 
    Arrow (fun x -> g (f x))

// 范例：处理流程
let addOne x = x + 1
let double x = x * 2

let arrowAddOne = arr addOne
let arrowDouble = arr double

let pipeline = arrowAddOne >>> arrowDouble

let (Arrow f) = pipeline
f 5  // -> (5 + 1) * 2 = 12
     // 先加 1 变成 6，再乘以 2 得 12
```

**实际应用 - 数据处理管道**：
```fsharp
// 场景：用户输入处理流程
let trimInput s = s.Trim()
let toLower s = s.ToLower()
let validateEmail s = if s.Contains("@") then s else "invalid"

let arrowTrim = arr trimInput
let arrowLower = arr toLower
let arrowValidate = arr validateEmail

let emailPipeline = arrowTrim >>> arrowLower >>> arrowValidate

let (Arrow f) = emailPipeline
f "  ALICE@GMAIL.COM  "  // -> "alice@gmail.com"
```

---

## 📊 综合例子 - 组合多个操作

```fsharp
// 场景：处理订单，同时计算总价和税费

type Order = { id: int; amount: decimal }

let calculateTax amount = amount * 0.1m
let calculateTotal amount = amount + (amount * 0.1m)
let applyDiscount (id, amount) = (id, amount * 0.9m)

let arrowTax = arr calculateTax
let arrowTotal = arr calculateTotal
let arrowDiscount = arr applyDiscount

// 1. 先应用折扣（second 只改变 amount）
let withDiscount = second arrowDiscount

// 2. 然后同时计算税费和总价（&&& 扇形）
let calculateBoth = arrowTax &&& arrowTotal

// 3. 完整流程
let fullPipeline = second arrowDiscount >>> calculateBoth

let (Arrow f) = fullPipeline
f (101, 100m)  // -> (9m, 99m)
               // 先折扣：100 * 0.9 = 90
               // 再计算：税费 = 9, 总价 = 99
```

---

## 🔍 操作关系图

```
     单一输入 x
         │
    ┌────┴────┐
    │          │
  first      second
    │          │
  f x        g x
    │          │
 (y,x)      (x,y)


     单一输入 x
    ┌────┬────┐
    │    │    │  &&& (扇形)
    f    g    h
    │    │    │
    └────┼────┘
        (结果元组)


  输入 a ──→ Arrow f ──→ 中间结果 b ──→ Arrow g ──→ 输出 c
              (>>> 组合)
```

---

## 📐 Arrow 必须满足的规则

```
1. arr id = id
   恒等箭头等于恒等函数

2. arr (f >>> g) = arr f >>> arr g
   函数组合可以被转换成箭头的组合

3. first (arr f) = arr (first f)
   arr 和 first 可以交换顺序

4. first (f >>> g) = first f >>> first g
   first 可以分配到组合操作中

5. first f >>> arr fst = arr fst >>> f
   获取元组第一个元素的位置可以前移

6. first f >>> arr (id *** g) = arr (id *** g) >>> first f
   first 和并行操作可以交换

7. first (first f) >>> arr assoc = arr assoc >>> first f
   嵌套的 first 可以通过关联律重组
```

---

## 🎯 应用场景总结

| 场景 | 使用的操作 | 优势 |
|------|-----------|------|
| Web 请求管道 | `>>>` 组合 | 代码清晰易维护 |
| 并行 API 调用 | `***` 并行 | 效率高，代码简洁 |
| 多字段验证 | `&&&` 扇形 | 灵活组合，易于测试 |
| 表单处理 | `first/second` | 精准控制元组元素 |
| 异步操作 | 自定义 AsyncArrow | 抽象清晰，易组合 |
| 数据分析 | `&&&` + 聚合 | 一次遍历多个指标 |

---

## 💡 核心价值

Arrow 让你以 **统一的方式** 处理各种复杂计算，使代码更 **模块化** 和 **易于组合**。
