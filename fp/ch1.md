# 函式幾種樣態
1. 保證不會出錯的函式  
   * 範例1:  
        ```csharp
        public int GetStrLength(string s) => s.Length;
        
        ```
2. 會出錯，但可以掌握錯誤的函式  
   * 範例1:
        ```csharp
        public Option<string> GetAgeDesc(int age) => age switch
        {
            < 0 => None,
            < 18 => "少年仔",
            < 65 => "中年仔",
            < 120 => "老年仔",
            _ => None
        };
        ```  
    * 範例2:
        ```csharp
        public Either<Error, string> GetAgeDesc(int age) => age switch
        {
            < 0 => Left<Error, string>(Error.New("年齡不能小於0")),
            < 18 => Right("少年仔"),
            < 65 => Right("中年仔"),
            < 120 => Right("老年仔"),
            _ => Left<Error, string>(Error.New("年齡不能大於120"))
        };
        ```  
3. 知道會出錯，但無法掌握，且跟外部存取無關  
    * 範例1:
        ```csharp
        public Either<Error, short> Add(short a, short b) =>
        Try(() => a + b)
        .Match(
            Succ: v => Right((short)(v)),
            Fail: ex => Left<Error, short>(Error.New(ex.Message))
        );
        ```
4. 知道會出錯，且掌握部份錯誤處理，剩下錯誤無法掌握，且跟外部存取無關
    * 範例1:
        ```csharp
        public Either<Error, short> Add(short a, short b) =>
        from _1 in guard(a > 0, Error.New("a 必須大於0")).ToEither()
        from _2 in guard(b > 0, Error.New("b 必須大於0")).ToEither()
        from result in Try(() => a + b).Match(
                                            Succ: v => Right((short)(v)),
                                            Fail: ex => Left<Error, short>(Error.New(ex.Message))
                                        )
        select result;
        ```
5. 知道會出錯，但無法掌握，跟外部存取有關
   * 範例1:
        ```csharp
        public Eff<string> GetFileContent(string path) => Eff(() => File.ReadAllText(path));
        ```
        