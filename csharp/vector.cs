#!package "System.Numerics.Vectors" "4.6.1"

using System.Numerics;

static Vector2 Resize(Vector2 image, Vector2 target, float scale)
{
    // 點積範例: [2,3] . [1,0] = 2*1 + 3*0 = 2
    // 用來找出目標向量
    var v = Vector2.Dot(image, target);
    return image / new Vector2(v, v) * new Vector2(scale, scale);
}

// 目標是寬800，高600的影像，將其縮放到寬度為500的大小
System.Console.WriteLine($"Resize:{Resize(new Vector2(800, 600), new Vector2(1, 0), 500)}");

// 目標是高600，將其縮放到高度為300的大小
System.Console.WriteLine($"Resize:{Resize(new Vector2(800, 600), new Vector2(0, 1), 300)}");