/*
 * 
180 = PI
90 = PI / 2
60 = PI / 3
30 = PI / 6
45 = PI / 4

* 일반각이 아닐 때

    - 42 degree
    42 -> (42/180) * PI -> 7/30 * PI

    - 270 degree
    360 : 2PI * r = 270 : x
    270 * (2pr) = 360: x
    x = 270 * (2pr) / 360
    x = 270pr / 180
    x = 9 pr / 6
    x = 3 pr / 2
    x = 3/2 * pr

    호의 길이가 반지름 r 과 같은 각을 -> a 라고 하면.
    (1) 360 : 2pr = a : r
    (2) a = (360 * r) / 2pr
    (3) a = 180 / pi

    1 radian -> 57.29577 degree
    1 degree -> 0.017453 radian

동경 : 움직이는 반직선
시초선 : 시작하는 선 x 축 반직선
sinϴ : 동경의 y 좌표
cosϴ : 동경의 x 좌표
tanϴ : 동경의 기울기
 */


Console.WriteLine(Math.PI / 4);


double rad = Math.PI / 4;
var sin = Math.Sin(rad);
var cos = Math.Cos(rad);
Console.WriteLine($"\n{rad:n4}\nsin: {sin:n4}\ncos: {cos:n4}");

