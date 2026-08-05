using CallStack;

var num = 123;
var age = 34;

Console.WriteLine($"Hello, World! {num} - {age}", num, age);


var demo = new Demo();
var result = Demo.ReverseString("HelloWorld");

Console.WriteLine(result);

var rs1 = Demo.IsPalindrome("kayak"); // racecar, 

Console.WriteLine(rs1);
var rs2 = Demo.DecimalToBinary(233, string.Empty); // 11101001
Console.WriteLine(rs2);
var rs3 = Demo.SumOfNumbers(10);
Console.WriteLine(rs3);

var arr = new int[] { 5, 29, 30, 99, 12, 56, 92, 7, 6, 52 };

Array.Sort(arr);

int target = 12;
var rs4 = Demo.BinarySearch(arr, 0, arr.Length - 1, target);