# Entity Framework Core
1. 打開專案的終端機,依下列指令安裝三個套件:  
   `dotnet add package Microsoft.EntityFrameworkCore --version 6.0.1`  
   `dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0.1`  
   `dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 6.0.1`  
2. 新版版號會隨著時間演進而更改,建議先至 [https://www.nuget.org/](https://www.nuget.org/) 查詢  
3. 再輸入下列指令:  
   `dotnet new tool-manifest`  
   `dotnet tool install dotnet-ef`  
4. 開始建立資料庫,輸入下列指令  
   `dotnet ef dbcontext scaffold "Data Source=xxx.stust.edu.tw;Initial Catalog=DbName;User ID=YourId;Password=YourPassword" Microsoft.EntityFrameworkCore.SqlServer -o  Models -t stud -t tea -t class -f`  
   這個指令會建立stud,tea,class三張表的物件模型至專案的Models目錄,將來schema有更動或新增表單,都必須再執行一次,更換模型  
5. 至Models打開DbNameContext.cs,會看到有一段是錯誤訊息:  
   ```csharp  
   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            optionsBuilder.UseSqlServer("Data Source=xxx.stust.edu.tw;Initial Catalog=DbName;User ID=YourId;Password=YourPassword");
        }
    }
   ```  
   將#字號那段說明給它註消掉,讓警告錯誤訊息消失  
6. 好了,以上大功告成,可以開始用ef了,開心~ :heart_eyes:  
   ```csharp  
   Models.DbNameContext db = new Models.DbNameContext();
   var stud = db.Studs.Where(c => c.StudNo == "A12345678").FirstOrDefault();
   ```  

