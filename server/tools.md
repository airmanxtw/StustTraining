1. 僅複製目錄結構,不複製檔案  
   ```robocopy "c:\Intel" "d:\Intel" /e /xf /lev:3 *```  
   /lev:3 代表僅複製來源目錄前三層  
2. IIS應用集區及應用程式結構複製至另一台  
   來源主機: ```appcmd list apppool /config /xml > c:\apppools.xml``` 以及 ```appcmd list site /config /xml > c:\sites.xml``` 
   目的主機: ```appcmd add apppool /in < c:\apppools.xml``` 以及 ```appcmd add site /in < c:\sites.xml```  

資料來源:  
https://itorz324.blogspot.com/2021/06/copying-directory-structures-without-files.html  
https://medium.com/%E9%9B%9C%E9%A3%9F%E6%80%A7%E7%9A%84%E8%B2%93/iis-%E6%90%AC%E8%A8%AD%E5%85%A8%E9%83%A8iis%E7%9A%84%E7%B6%B2%E7%AB%99%E5%8F%8A%E8%A8%AD%E5%AE%9A%E8%87%B3%E5%8F%A6%E4%B8%80%E5%8F%B0%E6%A9%9F%E5%99%A8%E4%B8%8A-95250cef845e

   
