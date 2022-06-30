# metadata example
```csharp
namespace langCenterApi.Models;
using System.ComponentModel.DataAnnotations;
[MetadataType(typeof(ResourceMetadata))]
public partial class LangcenterStudyResource
{

    /// <summary>
    /// 資源裡已開放的題目數
    /// </summary>
    /// <value></value>
    public int questionCount {
        get{
            return this.LangcenterStudyQuestions.Where(c => c.Enable).Count();
        }
    }    
}

internal class ResourceMetadata
{
    [Newtonsoft.Json.JsonIgnore]
    public virtual ICollection<LangcenterStudyQuestion> LangcenterStudyQuestions { get; set; }
}
```