# microsoft 第三方驗證
1. 先註冊應用程式 https://dotblogs.com.tw/supershowwei/2022/11/20/integrate-microsoft-login-in-asp-net-core
2. 選擇應用程式類型,需注意登入的範圍,是個人的,還是組織的(例如office.stust.edu.tw)
3. 可參考這篇文章 https://dotblogs.com.tw/supershowwei/2022/11/20/integrate-microsoft-login-in-asp-net-core
4. 組授權網址時需注意tenant的參數,有common、organizations、consumers 及租用戶識別碼,可參考 https://github.com/MicrosoftDocs/azure-docs.zh-tw/blob/master/articles/active-directory/develop/active-directory-v2-protocols.md#endpoints
5. 例如要限制office.stust.edu.tw的帳號才可登入的話: https://login.microsoftonline.com/office.stust.edu.tw/oauth2/v2.0/authorize?client_id={client_id}&state=12345abcde&response_type=code&scope=profile%20openid%20email
6. 登入成功取得code,參考第3點的連結,用post的方式取得user的訊息