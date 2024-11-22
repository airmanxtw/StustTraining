# 函式簽名
   ## 函式簽名表示函式的宣告及定義,在C#一個有返回值的函式簽名定義方式為Func\<I1,I2.....In,O>,I為輸入型態,O為輸出型態
1. Func\<int>
   * 範例
    ```csharp
      public int GetTen() => 10;
    ```
2. Func\<string,int>
   * 範例
   ```csharp
      public int GetStrLen(string str) => str.Length;
   ```
3. Func\<int,int,int>
   * 範例
   ```csharp
      public int Add(int a, int b) => a + b;
   ```  


## 若無回應值的函式,則稱為Action,簽名定義方式為Action\<I1,I2,....In>
1. Action\<string>
   * 範例
   ```csharp
      public void PrintStr(string str) => System.Console.WriteLine(str);  
   ```
2. Action\<int,int>
   * 範例
   ```csharp
      public void PrintAdd(int a, int b) => System.Console.WriteLine(a + b);
   ```

