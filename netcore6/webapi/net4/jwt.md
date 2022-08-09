# asp.net mvc 4 webapi jwt authentication
1. 安裝 jose-jwt 4.0
2. 建立一個登入者的model 
   ```csharp
    public class JwtModel
    {
        public string userid { get; set; }
        public double exp { get; set; }
        public DateTime expDate {
            get
            {
                return (new DateTime(1970, 1, 1)).AddSeconds(exp).ToLocalTime();
            }
        }
        public List<string> roles { get; set; }
    }
   ``` 
3. 建立一個編碼及解碼JWT的Class 
   ```csharp
    public class jwtProvider2
    {
        private string pKey = "<RSAKeyValue><Modulus>t9n3nerIULwfGDdnMcWOhSFbv4menaAbjXw2Jc3zrW5IyFVd/mZcr6sl3Tt+Io+k7ORL3izw4ccGrpmYtUvz3U7yT9OdhpMU7XpHDQzgpftYlzCa0M6/gi7E1CetBMaRN1YTzx4/GUFu08c9W2/jJDl8fj0QycQtaz9PzVCTQqUhCCusPyYz7Kz2m4F1keCG/5lhGgMOT89KvLrVZJ1VPEjTGZwckRNtEwIAjEXU7vYH5jI4vnjsDSvR3cd0FUFAb93fYUh2J4RnBmoSz4wHqBk1wkkdNpauW2tG5XViSlvAPxacLY26YXL7f3DLs5o4xr0jh7mfOc3FPz94zYpqIps=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        private string rKey = "<RSAKeyValue><Modulus>t9n3nerIULwfGDdnMcWOhSFbv4menaAbjXw2Jc3zrW5IyFVd/mZcr6sl3Tt+Io+k7ORL3izw4ccGrpmYtUvz3U7yT9OdhpMU7XpHDQzgpftYlzCa0M6/gi7E1CetBMaRN1YTzx4/GUFu08c9W2/jJDl8fj0QycQtaz9PzVCTQqUhCCusPyYz7Kz2m4F1keCG/5lhGgMOT89KvLrVZJ1VPEjTGZwckRNtEwIAjEXU7vYH5jI4vnjsDSvR3cd0FUFAb93fYUh2J4RnBmoSz4wHqBk1wkkdNpauW2tG5XViSlvAPxacLY26YXL7f3DLs5o4xr0jh7mfOc3FPz94zYpqIps=</Modulus><Exponent>AQAB</Exponent><P>DqDPBcsuSq5PKeMJ3IKiO5VfIAy8fczE+F8nyCthaPiuafdOW1Ysza+r6M2+XmtKAkKe16uX1Mkqs8njdh4y6Ouv2Y95QYWYSlnkkq1R36PLNthEs6Og+yNMSdoyqb5qSHhRjRqz+zqCjWB2OYUEZLzLdUqYhtZa6LS9Hw5auiof</P><Q>DJF9p2sqqaeEi8zawfirDyQbHe+IUuWCPoCh6ywotm7nQywtLcz4zWr6eU7drO7dgDAcB/1RCbMWWEwMMkLs+mCUQyPnaSGo+UYYDNFHhTBHZ3ntzsB2V7HilVQukIe7s+PfdmKqN7KFgZYeQaBELlYCwxjj+4nQYmq9UAeB7rAF</Q><DP>BoRT1sPvKqw/bmzMfuXf+uWQDIpAC1eHxRM3ePECo65unZzk1sIskbIax0up7QylWx5s/65dpRm+IXznDj9j9OrTW/yUS8GF2FH2B/aZTCX/qb5t1Q4n8NqH6TsBaSgA16pqwo9L8bfeY7e+099T1sFZbza9fwzOUW/79XMCrn+H</DP><DQ>CxwtlGpiezhvLdm341H7fNXw9qSAwK7LVI8HE0mk76fX3QRXJ66SnZucsNFrmexJd4CPtuxRz540+Xa4LVIGBEqNpVA+xyuPIFO6/NGfRHhqFmmtM1/k/R5tao3Q38hsv9eG+TK8v6Ga7POT6XVP8BnuNSJyni+0OtuaX/1S+Xk1</DQ><InverseQ>AdvjM1lDnFofC3EP6kRB/22kgGYCDtnC2yO9lsMUP0z4Ah0K3YjGCZQOQrYMvEY1GJjPm0lNEmGsRvM3q1C77grhGPPDGANF1TmGQOBiLN0hSXlwNw5yB9pS4XCx7L8souDQrfj398Fr6JLvr6SCnliPb1XkXGbj6hdP1B0CaZwm</InverseQ><D>cK+3luI1304+3M/wf1AwV6cnnhGS19gvqxvL8IX4an+jOq8gSnKhMsb7EUuKt1JF6zDO06HGDwy3KsNYFx8znJzr5kO1VKvH8vq0m+ods36IQTr4WS2vsfJKkCK1aKzwivNGdvd6A1fYvxNqpyXAGg7mIFX7eT+1+vs2RYiyifGtzvW8ab+rzyDgKsnaB6s9EVm6Y1ucVnb2fcCKd2sj3nfjt6Ul2AYhYVF+ncB6nquqnigzwQSnXACaMzAwWfrtJIn1CjR9xeLgysIhISdm6AAWSAyC3qxTA8b4Zv2CvXna07KYUL0BEDkNO6GV+nVO1CeHlpGioj51Dg3uM9fbFTk=</D></RSAKeyValue>";
        RSACryptoServiceProvider _rsa = new RSACryptoServiceProvider();                

        public string encode(string userid, List<string> roles)
        {                        
            var payload = new Dictionary<string, object>()
            {
                { "userid",userid},
                { "exp",  (TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddMinutes(30)) -   new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds },
                {"roles",roles}
            };
         
            var privateKey = RSA.Create();
            privateKey.FromXmlString(rKey);

            string token = Jose.JWT.Encode(payload, privateKey, JwsAlgorithm.RS512);
            return token;
        }

        public Models.JwtModel  decode(string token)
        {
            
             var publicKey = RSA.Create();
             publicKey.FromXmlString(pKey);
             var result = Jose.JWT.Decode(token, publicKey);
             var obj = JsonConvert.DeserializeObject<Models.JwtModel>(result);
             return obj;
        }

    }
   ``` 
4. 繼承AuthorizeAttribute實作一個自訂Authorize
   ```csharp
    public class AppAuthorizeAttribute : AuthorizeAttribute
    {
        public AppAuthorizeAttribute()
        {
           
        }
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            try
            {
                var token = HttpContext.Current.Request.Headers["Authorization"].ToString();
                jwtProvider2 jp = new jwtProvider2();
                var jwt = jp.decode(token);
                var roles = this.Roles.Split(new char[1] { ',' }).Select(s => s.Trim()).ToList();                
                return jwt.expDate > DateTime.Now && (string.IsNullOrEmpty(this.Roles) || roles.Intersect(jwt.roles).Count()>0)  ? true : false;
            }
            catch(Exception)
            {
                return false;
            }            
        }               
    }
   ``` 
5. 對ApiController做一個Extension,目的是要在ApiControll裡可以拿到user物件
   ```csharp
    public static class MyExtensions
    {
        public static Models.JwtModel  GetAuthorization(this ApiController ac)
        {
            try
            {
                string token = ac.Request.Headers.Authorization.ToString();
                jwtProvider2 jp = new jwtProvider2();
                return jp.decode(token);
            }
            catch (Exception)
            {
                return null;
            }                        
        }
    }
   ``` 
6. 做一個登入的api
   ```csharp
    [AllowAnonymous]
    // POST api/login
    public HttpResponseMessage Post([FromBody]Models.LoginModel  login)
    {
        if (login.userid == "airmanx" && login.password == "12345")
        {
            Api.jwtProvider2 j2 = new Api.jwtProvider2();
            List<string> roles = new List<string>() { "admin", "user" };
            var token = j2.encode(login.userid, roles);                
            return Request.CreateResponse(HttpStatusCode.OK, token);
        }
        else
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "userid or password error");
        }
    }
   ``` 
7. 取資料的api
   ```csharp
    [AppAuthorize(Roles="admin")]
    // GET api/values
    public string  Get()
    {
        return string.Format("{0}抓到資料", this.GetAuthorization().userid);
    }
   ``` 
8. 登入的javascript
   ```javascript
   $("#Button1").click(function () {
        var login={userid:'airmanx',password:'1234'};
        axios.post("http://localhost:56457/api/login",{
            userid:'airmanx',
            password:'12345'
        })
        .then(function(res){
            $("#token").val(res.data);
        })
        .catch(function(err){                    
            alert(err.response.data.Message);
        });
        return false;
    });
   ``` 
9. 取資料的javascript
   ```javascript
   $("#Button2").click(function () {                
        var token=$("#token").val();
        axios.get("http://localhost:56457/api/values",{
            headers:{
                "Authorization":token
            }
        })
        .then(function(res){
            alert(res.data);
        })
        .catch(function(err){                    
            alert(err.response.data.Message);
        });
        return false;
    });
   ``` 
10. 在web.config裡的system.webServer裡加入CORS
    ```xml
    <httpProtocol>
      <customHeaders>
        <clear />
        <add name="Access-Control-Allow-Origin" value="*" />
        <add name="Access-Control-Allow-Headers" value="*"/>
        <add name="Access-Control-Allow-Methods" value="GET,POST,PUT,DELETE,OPTIONS"/>
      </customHeaders>
    </httpProtocol>
    ```
