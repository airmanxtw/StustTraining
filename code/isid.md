```csharp
public static bool isIDv2(string id)
    {
        if (id.Length == 10)
        {
            Dictionary<string, int> map = new Dictionary<string, int> 
            {
                {"A",10},{"B",11},{"C",12},{"D",13},{"E",14},{"F",15},{"G",16},{"H",17},{"J",18},
                {"K",19},{"L",20},{"M",21},{"N",22},{"P",23},{"Q",24},{"R",25},{"S",26},{"T",27},
                {"U",28},{"V",29},{"X",30},{"Y",31},{"W",32},{"Z",33},{"I",34},{"O",35}
            };
            
            bool keep = true;

            int[] a = new int[10];
            int[] b = new int[10] { 1, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            int[] ab = new int[10];
            for (var index = 0; index < id.Length - 1; index++)
            {

                if (index == 0)
                {
                    if(map.Keys.Contains(id.Substring(0,1).ToUpper()))
                    {
                        string head = map[id.Substring(0, 1).ToUpper()].ToString();
                        a[0] = int.Parse(head.Substring(0, 1));
                        a[1] = int.Parse(head.Substring(1, 1));  
                    }
                    else
                    {
                        keep = false;
                        break;
                    }
                }              
                else
                {
                    if(isNumber(id.Substring(index,1)))
                    {
                        a[index + 1] =int.Parse(id.Substring(index, 1));
                    }
                    else
                    {
                        keep = false;
                        break;
                    }
                }
            }

            if (keep)
            {
                for (var i = 0; i < 10; i++)                
                    ab[i] = (a[i] * b[i]) % 10;

                string checkcode = Math.Abs((10 - ab.Sum()) % 10).ToString();
                return id.Substring(10 - 1, 1) == checkcode ? true : false;
            }
            else
            {
                return false;
            }
            
        }
        else
        {
            return false;
        }

    }
```

