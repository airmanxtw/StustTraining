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
        private string pKey = "<RSAKeyValue><Modulus>4OMwdkZhujGtXSlZsoY3zak/Jw8DhV+jPXPvU9AiLzxn2RTbpfuAxpxfipmGQIrHiALzNfbs4RpHzgHQTMm+jEPzx++NIFpydi6lMZ0jmzKF7F2iWKnJGKmKn6cD4LflTUUGjC6faZIjk+9c3YNTa1TjQIOMNRD8yNp2PkfEDhc=</Modulus><Exponent>AQAB</Exponent><P>9/jjuP8iniR6qrLPXDCKltP0G/oTedVSrniRv6x3BIfu/zfdsEt58R9pql3FgUner1uYQZ69sr+xFa6ZGQFhNQ==</P><Q>6Cr7IUasGYIHPLH8f8jKAtQ1Iq7/SWV/MJw4tCL/yyv7Aw5h73U9k5ljKGafqs9tg5mIOprHM0ikP+UtBabHmw==</Q><DP>OdKNt2u29M1o1TkQ5VxkVLtj5sovlG9L8mcnBVz/+8x+zICIbz2KV9GNYC6xiW5iQN3I6TRM48uTTY0DIjOSKQ==</DP><DQ>6CoS9zO05c0hb5CM7zvaxvluKUDWCI92oSWXjotxP7q7SFMZnFuhN/grMDtvb1/+I5tknzYn/SCnHz4Nx5kAAQ==</DQ><InverseQ>nLpw76AXKb761nLqL/Mw2b0lgDtM3MhqFzmEKGf0NfxYCT28ymjJH//wsTf1m9GCT9D2U2w0yDCvpD8LwDAbMQ==</InverseQ><D>NHCFsm7DBxniZSDpS5nElW5rzX3Qwl8Ev6WzHwYfkogxZwtYFKJK6wU3uigGiDJUSMD3WZQUtIDeUYlWuzEn/zk7GAzgLocZrAKZX6DlbMTkoUUtt0EF2DgJtgdUp4V/TrgYA0IlZ7t9NKTUYOtmIDBXSJysiWvayL0p2f8sBBE=</D></RSAKeyValue>";
        private string rKey = "<RSAKeyValue><Modulus>4OMwdkZhujGtXSlZsoY3zak/Jw8DhV+jPXPvU9AiLzxn2RTbpfuAxpxfipmGQIrHiALzNfbs4RpHzgHQTMm+jEPzx++NIFpydi6lMZ0jmzKF7F2iWKnJGKmKn6cD4LflTUUGjC6faZIjk+9c3YNTa1TjQIOMNRD8yNp2PkfEDhc=</Modulus><Exponent>AQAB</Exponent><P>9/jjuP8iniR6qrLPXDCKltP0G/oTedVSrniRv6x3BIfu/zfdsEt58R9pql3FgUner1uYQZ69sr+xFa6ZGQFhNQ==</P><Q>6Cr7IUasGYIHPLH8f8jKAtQ1Iq7/SWV/MJw4tCL/yyv7Aw5h73U9k5ljKGafqs9tg5mIOprHM0ikP+UtBabHmw==</Q><DP>OdKNt2u29M1o1TkQ5VxkVLtj5sovlG9L8mcnBVz/+8x+zICIbz2KV9GNYC6xiW5iQN3I6TRM48uTTY0DIjOSKQ==</DP><DQ>6CoS9zO05c0hb5CM7zvaxvluKUDWCI92oSWXjotxP7q7SFMZnFuhN/grMDtvb1/+I5tknzYn/SCnHz4Nx5kAAQ==</DQ><InverseQ>nLpw76AXKb761nLqL/Mw2b0lgDtM3MhqFzmEKGf0NfxYCT28ymjJH//wsTf1m9GCT9D2U2w0yDCvpD8LwDAbMQ==</InverseQ><D>NHCFsm7DBxniZSDpS5nElW5rzX3Qwl8Ev6WzHwYfkogxZwtYFKJK6wU3uigGiDJUSMD3WZQUtIDeUYlWuzEn/zk7GAzgLocZrAKZX6DlbMTkoUUtt0EF2DgJtgdUp4V/TrgYA0IlZ7t9NKTUYOtmIDBXSJysiWvayL0p2f8sBBE=</D></RSAKeyValue>";
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

        public string encode(string userid, List<string> roles)
        {
            var payload = new Dictionary<string, object>()
            {
                { "userid",userid},
                { "exp",  (TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddMinutes(30)) -   new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds },
                {"roles",roles}
            };
         
            var publicKey = RSA.Create();
            publicKey.FromXmlString(pKey);

            string token = Jose.JWT.Encode(payload, publicKey, JwsAlgorithm.RS512);
            return token;
        }

        public Models.JwtModel  decode(string token)
        {
            
             var privateKey = RSA.Create();
             privateKey.FromXmlString(rKey);
             var result=Jose.JWT.Decode(token, privateKey);
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
