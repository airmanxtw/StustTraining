# 有關乘數與被乘數的討論 : https://parents.hsin-yi.org.tw/Forum/Topic/20/Discuss/Detail/414
1. 在一般C#的函數中,兩數什麼位置好像都沒什麼關係,功能一樣可以用:
``` csharp
    // c#
    public int multiply(int x,int y) => x*y;
    // or
    public int multiply(int y,int x) => x*y;
```
2. 但如果你的函數考慮了柯里化,前後位置在定義上就會不一樣:
```javascript
    // javascript
    let multiply = x => y => x*y;
    let double = multiply(2);
    let triple = multiply(3);

    // example
    console.log(`5的倍數為:${double(5)}`);
    console.log(`9的三倍數為:${triple(9)}`);
```

3. 那c#可不可以柯里化?可以,醜,心智負擔大
``` csharp
    // c#
    public static Func<int,Func<int,int>> multiply = x => y => x * y;
    public Func<int,Func<int,int>> dbl = x => multiply(2);
    public Func<int,Func<int,int>> tpl = x => multiply(3); 

    //or 
    public static Func<int,int> mutiply(int x) => y => x * y;
    public Func<int,int> dbl(int x) => mutiply(2);
    public Func<int,int> tpl(int x) => mutiply(3);
```

4. 補充
```javascript
    # javascript

    let isValidHeight = min => max => height => (height>=min && height<=max)

    // 人類身高判斷器
    let isValidHumanHeight = height => isValidHeight(80)(250);

    // 狗狗高度判斷器
    let isValidDogHeight = height => isValidHeight(30)(120);

    // example
    console.log(`高160公分的人類,身高是否合理:${isValidHumanHeight(160)}`);
    console.log(`高200公分的狗狗,高度是否合理:${isValidDogHeight(200)}`);

```

5. 捕充 C#版
``` csharp
    // using static LanguageExt.Prelude; 
    # c#
    public static bool isValidHeight(int minHeight, int maxHeight, int height) => height >= minHeight && height <= maxHeight;

    // 人類身高判斷器
    public Func<int,bool> isValidHumanHeight = curry<int,int,int,bool>(isValidHeight)(80)(200);

    // 狗狗高度判斷器
    public Func<int,bool> isValidDogHeight = curry<int,int,int,bool>(isValidHeight)(30)(120);

```

