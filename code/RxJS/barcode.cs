public class accountApi : IaccountApi
{


    // * 轉換表
    private int getval(string c)
    {
        Dictionary<string, int> table = new Dictionary<string, int>()
            {
                {"0",0},{"1",1},{"2",2},{"3",3},{"4",4},{"5",5},{"6",6},{"7",7},{"8",8},{"9",9},
                {"A",1},{"B",2},{"C",3},{"D",4},{"E",5},{"F",6},{"G",7},{"H",8},{"I",9},
                {"J",1},{"K",2},{"L",3},{"M",4},{"N",5},{"O",6},{"P",7},{"Q",8},{"R",9},
                {"S",2},{"T",3},{"U",4},{"V",5},{"W",6},{"X",7},{"Y",8},{"Z",9}
            };
        return table[c];
    }

    // * 加總值
    // * even 是否為偶數位置
    private int getsum(string barcode, bool even)
    {
        var barchars = barcode.ToCharArray();
        int total = 0;
        for (int i = 1; i <= barchars.Length; i++)
        {
            if (i % 2 == (even ? 0 : 1))
                total += getval(barchars[i - 1].ToString());
        }
        return total;
    }

    // * 取得餘數檢查碼
    private string getcheckcode(int remainder, string p1, string p2)
    {
        return remainder == 0 ? p1 : remainder == 10 ? p2 : remainder.ToString();
    }


    // * 取得兩位檢查碼
    public string calcCheckSum(List<string> barcodes)
    {
        int total1 = 0;
        int total2 = 0;
        string check1 = string.Empty;
        string check2 = string.Empty;
        foreach (var bar in barcodes)
        {
            total1 += getsum(bar, false);
            total2 += getsum(bar, true);
        }

        check1 = getcheckcode(total1 % 11, "A", "B");
        check2 = getcheckcode(total2 % 11, "X", "Y");

        return $"{check1}{check2}";
    }

    // * 取得三維條碼
    public Models.Barcodes barcode(DateTime deadline, string account, int amt)
    {
        Models.Barcodes barcodes = new Models.Barcodes()
        {
            barcode1 = string.Format("{0}{1:MMdd}AG1", (deadline.Year - 1911).ToString().Substring(1, 2), deadline),
            barcode2 = account.PadRight(16, '0'),
            barcode3 = string.Format("9937{0:000000000}", amt)
        };
        string CheckCode = calcCheckSum(barcodes.GetBarcodes());
        barcodes.barcode3 = barcodes.barcode3.Insert(4, CheckCode);
        return barcodes;
    }
}


// barcodes.GetBarcodes()
public class Barcodes
{

    public string barcode1 { get; set; }
    public string barcode2 { get; set; }
    public string barcode3 { get; set; }

    public List<string> GetBarcodes()
    {
        return new List<string>()
            {
                barcode1,barcode2,barcode3
            };
    }
}