## ghostscript
1. 下載windows版: https://www.ghostscript.com/releases/gsdnld.html
2. 壓縮一個檔案: 
   ```
   gswin64c -sDEVICE=pdfwrite -dCompatibilityLevel=1.4 -dPDFSETTINGS=/ebook -dNOPAUSE -dQUIET -dBATCH -sOutputFile=output.pdf input.pdf
   ``` 
3. 壓縮一個檔案並取代原檔案 
   ```
   gswin64c -sDEVICE=pdfwrite -dCompatibilityLevel=1.4 -dPDFSETTINGS=/ebook -dNOPAUSE -dQUIET -dBATCH -sOutputFile=output.pdf input.pdf && move /y output.pdf input.pdf
   ``` 
4. 將目錄下所有的pdf都壓縮 
   ```
   forfiles /m *.pdf /c "cmd /c gswin64c -sDEVICE=pdfwrite -dCompatibilityLevel=1.4 -dPDFSETTINGS=/ebook -dNOPAUSE -dQUIET -dBATCH -sOutputFile=output.pdf @file && move /y output.pdf @file" /s
   ```
5. 壓縮品質參數(由上而下,低至高)
   ```
   -dPDFSETTINGS=/screen
   -dPDFSETTINGS=/ebook
   -dPDFSETTINGS=/prepress
   -dPDFSETTINGS=/printer
   -dPDFSETTINGS=/default
   ```