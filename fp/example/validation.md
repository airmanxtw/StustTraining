# Validation Monad
```csharp
    //檢查字串長度是否在4~10之間
    public static Validation<Error, string> LengthRule(string s) =>
        Cond<string>(s => s.Length >= 4 && s.Length <= 10)
        .Then(Success<Error, string>(s))
        .Else(Fail<Error, string>(Error.New("長度不在4~10之間")))
        .Invoke(s);


    //檢查字串是否符合a-zA-Z0-9
    public static Validation<Error, string> IsAlphaNumeric(string s) =>
        Cond<string>(s => s.ForAll(c => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9')))
        .Then(Success<Error, string>(s))
        .Else(Fail<Error, string>(Error.New("不是字母或數字")))
        .Invoke(s);


    //檢查字串是否為admin或administrator
    public static Validation<Error, string> CheckHasAdmin(string s) =>
        Cond<string>(List("admin", "administator").Contains)
        .Then(Fail<Error, string>(Error.New("帳號不可為admin或administrator")))
        .Else(Success<Error, string>(s))
        .Invoke(s);


    // 使用LengthRule,IsAlphaNumeric,CheckHasAdmin組合一個檢查規則
    public static Either<Seq<Error>, string> CheckAccount(string s) =>
    LengthRule(s).Bind(IsAlphaNumeric).Bind(CheckHasAdmin).ToEither();


    public static void Run()
    {
        CheckAccount("id='airmanx' or 1=1").Match(
            Left: errs => errs.Iter(Console.WriteLine),
            Right: s => Console.WriteLine($"通過檢查的帳號:{s}")
        );
    }
```