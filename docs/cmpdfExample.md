```vb
 Function Uploadfile(ByVal bytes As Byte(), ByVal filename As String) As String
    If System.IO.Path.GetExtension(filename).ToLower = ".pdf" Then
        Dim pdf As New CMPDF.pdf()
        Dim newBytes = pdf.compression(bytes)
        Return fs.UploadFileByDirectory(newBytes, filename, "StudentLicense")
    Else
        Return fs.UploadFileByDirectory(bytes, filename, "StudentLicense")
    End If
End Function
```