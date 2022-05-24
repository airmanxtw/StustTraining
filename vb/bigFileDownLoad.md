# 檔案分割下載
```vb
Public Sub DownLoadFile(ByVal filename As String, ByVal guidfilename As String, ByVal pg As Page)
    pg.Response.HeaderEncoding = System.Text.Encoding.GetEncoding("big5")
    pg.Response.AddHeader("Content-Disposition", "attachment; filename=" + filename)
    pg.Response.ContentType = "application/octet-stream"

    Dim fileapi As New FileAPI

    Dim fe As New FileInfo(guidfilename)
        
    Dim flength = fe.Length

    Dim block = 1024 * 1024
    For i As Integer = 0 To flength - 1 Step block
        Dim temp_block = block
        If i + block > flength Then
            temp_block = flength - i
        End If
        Dim bytes = fileapi.DownLoadFileBytes(guidfilename, i, temp_block)               
        pg.Response.BinaryWrite(bytes)
        pg.Response.Flush()

    Next
    pg.Response.End()
End Sub

Public Function DownLoadFileBytes(ByVal FileName As String, ByVal offset As Integer, ByVal count As Integer) As Byte()
    Dim fs As IO.FileStream = Nothing
    Try      
        fs = New IO.FileStream(FileName, FileMode.Open)
        Dim rs As New List(Of Byte)
        fs.Position = offset

        For i As Integer = 1 To count
            If fs.Position <= fs.Length - 1 Then
                rs.Add(fs.ReadByte)
            Else
                Exit For
            End If
        Next
        Return rs.ToArray
    Catch ex As Exception
        Throw ex
    Finally
        fs.Close()
    End Try
End Function