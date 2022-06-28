# 落地壓縮及大檔下載
``` csharp
 // 落地壓縮
var filename=System.IO.Path.GetTempFileName();
FileStream fsFile = new FileStream(filename, FileMode.Create);
using (var zip = new ZipOutputStream(fsFile))
{
    zip.PutNextEntry(System.IO.Path.GetFileName(FileUpload1.FileName));         
    zip.Write(FileUpload1.FileBytes, 0, FileUpload1.FileBytes.Count());

    zip.PutNextEntry(System.IO.Path.GetFileName(FileUpload2.FileName));
    zip.Write(FileUpload2.FileBytes, 0, FileUpload2.FileBytes.Count());         
}        
fsFile.Close();

//var bytes = System.IO.File.ReadAllBytes(filename);

Label1.Text = filename;


// 輸出檔案 *****************************************************************************
Page.Response.HeaderEncoding = System.Text.Encoding.GetEncoding("big5");
Page.Response.AddHeader("Content-Disposition", "attachment; filename=test.zip");        
Page.Response.ContentType = "application/octet-stream";

var targetfile =new System.IO.FileStream(filename,FileMode.Open);
Page.Response.AddHeader("Content-Length", targetfile.Length.ToString());

List<byte> outbytes =new List<byte>();
        
long block = 1024 * 5; // buffer 大小
byte[] buffer;
int readCount = 0;
long totalRead = 0;
do
{
    if ((targetfile.Length - totalRead) > block)
    {
        buffer = new byte[block];
    }
    else
    {
        buffer = new byte[targetfile.Length - totalRead];
        block = targetfile.Length - totalRead;
    }
        
    readCount = targetfile.Read(buffer, 0, (int)block);
    if (readCount > 0)
    {
        Page.Response.BinaryWrite(buffer);
        Page.Response.Flush();

    }

} while (readCount>0);

Page.Response.End();
```