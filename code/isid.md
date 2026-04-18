# 身分證檢查
```csharp
public static bool isIDv2(string id)
{
    if (string.IsNullOrEmpty(id) || id.Length != 10)
        return false;

    id = id.ToUpper();

    // 第1碼必須是英文字母 A-Z
    if (id[0] < 'A' || id[0] > 'Z')
        return false;

    // 判斷格式類型：
    // 國民身分證：     [A-Z][1-2]\d{8}
    // 舊式統一證號：   [A-Z][A-D]\d{8}   (2021年前)
    // 新式統一證號：   [A-Z][89][0-9]\d{7} (2021年起)
    bool isNationalId  = id[1] == '1' || id[1] == '2';
    bool isOldUiNumber = id[1] >= 'A' && id[1] <= 'D';
    bool isNewUiNumber = id[1] == '8' || id[1] == '9';

    if (!isNationalId && !isOldUiNumber && !isNewUiNumber)
        return false;

    // 第3碼至第10碼必須為數字
    for (int i = 2; i < 10; i++)
        if (id[i] < '0' || id[i] > '9')
            return false;

    // 第1碼英文字母轉換值：對應數字十位 × 1 + 個位 × 9
    // 對應數字：A=10,B=11,C=12,D=13,E=14,F=15,G=16,H=17,I=34,J=18,
    //          K=19,L=20,M=21,N=22,O=35,P=23,Q=24,R=25,S=26,T=27,
    //          U=28,V=29,W=32,X=30,Y=31,Z=33
    int[] localeCodeList =
    {
         1, 10, 19, 28, 37, 46, 55, 64, 39, 73, // A-J
        82,  2, 11, 20, 48, 29, 38, 47, 56, 65, // K-T
        74, 83, 21,  3, 12, 30                  // U-Z
    };

    // 舊式統一證號第2碼英文字母轉換值（僅 A-D 合法）
    int[] residentCodeList =
    {
        0, 1, 2, 3, 4, 5, 6, 7, 4, 8, // A-J
        9, 0, 1, 2, 5, 3, 4, 5, 6, 7, // K-T
        8, 9, 2, 0, 1, 3              // U-Z
    };

    // 加權係數（對應 10 個計算位）
    int[] coefficients = { 1, 8, 7, 6, 5, 4, 3, 2, 1, 1 };

    // 計算各位數值
    int[] digits = new int[10];
    digits[0] = localeCodeList[id[0] - 'A'];
    digits[1] = isOldUiNumber ? residentCodeList[id[1] - 'A'] : (id[1] - '0');
    for (int i = 2; i < 10; i++)
        digits[i] = id[i] - '0';

    // 加權總和須為 10 的倍數
    int sum = 0;
    for (int i = 0; i < 10; i++)
        sum += digits[i] * coefficients[i];

    return sum % 10 == 0;
}
```

