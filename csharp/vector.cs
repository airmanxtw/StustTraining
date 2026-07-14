#!package "System.Numerics.Vectors" "4.6.1"

using System.Numerics;

// 2D圖形縮放
static Vector2 Resize(Vector2 image, Vector2 target, float scale)
{
    // 點積範例: [2,3] . [1,0] = 2*1 + 3*0 = 2
    // 用來找出目標向量
    var v = Vector2.Dot(image, target);
    return image / new Vector2(v) * new Vector2(scale);
}

// 結果：
// 目標是寬800，高600的影像，將其縮放到寬度為500的大小
Console.WriteLine($"Resize:{Resize(new Vector2(800, 600), new Vector2(1, 0), 500)}");
// result： [500, 375]

// 目標是高600，將其縮放到高度為300的大小
Console.WriteLine($"Resize:{Resize(new Vector2(800, 600), new Vector2(0, 1), 300)}");
// result： [400, 300]

// 停車費計算
static float CalculateParkingFee(Vector3 vehicle, Vector3 price) =>
    Vector3.Dot(vehicle, price);

// 結果：
// 汽車申請2輛，機車1輛，腳踏車1輛，價格分別為汽車$100，機車$50，腳踏車$20
Console.WriteLine($"Parking Fee:{CalculateParkingFee(new Vector3(2, 1, 1), new Vector3(100, 50, 20))}");
// result： 270