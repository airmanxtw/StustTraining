## vb
```vb
rem vb
rem 巢狀
Dim ip = IPProcess.getRealIP(IPProcess.getXFORWARDEDFOR(Page.Request))

Dim ip2 = {Page.Request} _
          .Select(Function(q) IPProcess.getXFORWARDEDFOR(q)) _
          .Select(Function(x) IPProcess.getRealIP(x)).First()

rem linq (lazy loading)
Dim ip3 = From req In {Page.Request}
          Let xfor = IPProcess.getXFORWARDEDFOR(req)
          Let ip = IPProcess.getRealIP(xfor)
          Select ip

Console.Write(ip3.First())
```

## c#
```cs
/// c# 
var ip = new[] { Page.Request }
                .Select(IPProvider.getXFORWARDEDFOR)
                .Select(IPProvider.getRealIP).First();


var ip2 = new[] { Page.Request }
         .Select(IPProvider.getXFORWARDEDFOR)
         .Select(IPProvider.getRealIP);

Func<string,string> getTitle = (string p) => string.Format("IP位置是:{0}",ip);

var ip3 = ip2.Select(getTitle);

Console.Write(ip3.First());

```