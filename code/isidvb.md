```vb
Public Shared Function isIDv2(id As String) As Boolean
    If String.IsNullOrEmpty(id) OrElse id.Length <> 10 Then
        Return False
    End If

    id = id.ToUpper()

    ' 第1碼必須是英文字母 A-Z
    If id(0) < "A"c OrElse id(0) > "Z"c Then
        Return False
    End If

    ' 判斷格式類型：
    ' 國民身分證：     [A-Z][1-2]\d{8}
    ' 舊式統一證號：   [A-Z][A-D]\d{8}   (2021年前)
    ' 新式統一證號：   [A-Z][89][0-9]\d{7} (2021年起)
    Dim isNationalId  As Boolean = id(1) = "1"c OrElse id(1) = "2"c
    Dim isOldUiNumber As Boolean = id(1) >= "A"c AndAlso id(1) <= "D"c
    Dim isNewUiNumber As Boolean = id(1) = "8"c OrElse id(1) = "9"c

    If Not isNationalId AndAlso Not isOldUiNumber AndAlso Not isNewUiNumber Then
        Return False
    End If

    ' 第3碼至第10碼必須為數字
    For i As Integer = 2 To 9
        If id(i) < "0"c OrElse id(i) > "9"c Then
            Return False
        End If
    Next

    ' 第1碼英文字母轉換值：對應數字十位 × 1 + 個位 × 9
    ' 對應數字：A=10,B=11,C=12,D=13,E=14,F=15,G=16,H=17,I=34,J=18,
    '          K=19,L=20,M=21,N=22,O=35,P=23,Q=24,R=25,S=26,T=27,
    '          U=28,V=29,W=32,X=30,Y=31,Z=33
    Dim localeCodeList As Integer() = {
         1, 10, 19, 28, 37, 46, 55, 64, 39, 73, ' A-J
        82,  2, 11, 20, 48, 29, 38, 47, 56, 65, ' K-T
        74, 83, 21,  3, 12, 30                  ' U-Z
    }

    ' 舊式統一證號第2碼英文字母轉換值（僅 A-D 合法）
    Dim residentCodeList As Integer() = {
        0, 1, 2, 3, 4, 5, 6, 7, 4, 8, ' A-J
        9, 0, 1, 2, 5, 3, 4, 5, 6, 7, ' K-T
        8, 9, 2, 0, 1, 3              ' U-Z
    }

    ' 加權係數（對應 10 個計算位）
    Dim coefficients As Integer() = {1, 8, 7, 6, 5, 4, 3, 2, 1, 1}

    ' 計算各位數值
    Dim digits(9) As Integer
    digits(0) = localeCodeList(AscW(id(0)) - AscW("A"c))
    digits(1) = If(isOldUiNumber, residentCodeList(AscW(id(1)) - AscW("A"c)), AscW(id(1)) - AscW("0"c))
    For i As Integer = 2 To 9
        digits(i) = AscW(id(i)) - AscW("0"c)
    Next

    ' 加權總和須為 10 的倍數
    Dim sum As Integer = 0
    For i As Integer = 0 To 9
        sum += digits(i) * coefficients(i)
    Next

    Return sum Mod 10 = 0
End Function
```