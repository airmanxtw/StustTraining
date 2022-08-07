# asp.net mvc 4 webapi jwt authentication
1. 安裝 jose-jwt 4.0
2. 建立一個model 
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