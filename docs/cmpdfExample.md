## cmpdf dll download for .net 4.x
[download here](https://my.stust.edu.tw/board.php?courseID=4136&f=doc&cid=3453476)

## cmpdf vb example
```vb
 Function Uploadfile(ByVal bytes As Byte(), ByVal filename As String) As String
    If System.IO.Path.GetExtension(filename).ToLower = ".pdf" Then
        Dim pdf As New CMPDF.pdf()
        Dim newBytes = pdf.compression(bytes)
        Return fs.UploadFileByDirectory(newBytes, filename, "DirName")
    Else
        Return fs.UploadFileByDirectory(bytes, filename, "DirName")
    End If
End Function
```