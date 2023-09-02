## ver 1
```csharp
var content="hello~ this is url: https://www.stust.edu.tw";
Regex rex = new Regex(@"(https)://[\w-]+(\.[\w-]+)+([\w.,@?^=%&:/~+#-]*[\w@?^=%&/~+#-])?", RegexOptions.Compiled | RegexOptions.IgnoreCase);

var regContent = rex.Replace(content, delegate(Match match)
{
    return string.Format("<a target='_blank' href='{0}'>{1}</a>", match.ToString(), match.ToString());
});
```

## var 2
```csharp
 var content="hello~ this is url: https://www.stust.edu.tw";
 Regex rex = new Regex(@"(https)://[\w-]+(\.[\w-]+)+([\w.,@?^=%&:/~+#-]*[\w@?^=%&/~+#-])?", RegexOptions.Compiled | RegexOptions.IgnoreCase);

 var regContent = rex.Replace(content, (Match match) =>
 {
    return string.Format("<a target='_blank' href='{0}'>{1}</a>", match.ToString(), match.ToString());
 });       
```