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

## Multiplication table
```cs
var table = from a in new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }
            from b in new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }
            select string.Format("{0}x{1}={2}", a, b, a * b);

table.ToList().ForEach(s => Console.Write(s));

```