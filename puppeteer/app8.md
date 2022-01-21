# app8.js install
1. 先安裝node.js環境 <a href="https://nodejs.org/en/" target="_blank">安裝</a>  
2. 將zip解到某個目錄之中  
3. 打開終端機的指令輸入  
   `cd PuppeteerApp`  
   `npm install`  
4. 待套件安裝完成後，打開app8.js檔案，將這兩行程式放入自己的帳號及密碼後存檔  
   ```javascript
   await page.type("#UserName", "userId");
   ``` 
   ```javascript
   await page.type("#Password", "userPassword");
   ```   
5. 到終端機指令開始執行程式  
   `node app8`  
6. 產生的檔案會放至於`scores`目錄中，如果程式有重跑，請清空目錄後再執行 :information_desk_person: enjoy!
