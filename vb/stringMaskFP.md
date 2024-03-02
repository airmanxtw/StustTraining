```csharp
// * 檢查索引位置是否超出範圍
var CheckIndex = (string s, int i, int n) => i < 0 || i > s.Length - 1
     ? Left<string, Tuple<string, int, int>>("位置索引已超出範圍")
     : Right<string, Tuple<string, int, int>>(Tuple(s, i, n));

// * 檢查長度是否超出範圍
var CHeckLength = (string s, int i, int n) => n < 0 || i + n > s.Length
    ? Left<string, Tuple<string, int, int>>("長度已超出範圍")
    : Right<string, Tuple<string, int, int>>(Tuple(s, i, n));

// * 取得字串前段
var FrontString = (string s, int i, int n) => s[..i];

// * 取得字串後段
var TailString = (string s, int i, int n) => s[(i + n)..];

// * 組合字串
var CompineString = (string s, int i, int n) => $"{FrontString(s, i, n)}{new string('*', n)}{TailString(s, i, n)}";

// * 字串遮罩
var StringMask = (string s, int i, int n) =>
    Some(Tuple(s, i, n)).ToEither("參數錯誤")
    .Bind(t => t.Map(CheckIndex))
    .Bind(t => t.Map(CHeckLength))
    .Map(t => t.Map(CompineString));