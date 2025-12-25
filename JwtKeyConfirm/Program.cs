using System.Security.Cryptography;
using System.Text;

var data = "Hello World";

// 다양한 길이의 키로 테스트
var key64 = new string('A', 64);    // 64바이트
var key129 = new string('B', 129);  // 129바이트
var key168 = new string('C', 168);  // 168바이트

Console.WriteLine("=== HMAC-SHA512 테스트 ===\n");

// 모두 정상 작동!
var hmac1 = ComputeHmac(data, key64);
Console.WriteLine($"64바이트 키: {hmac1.Length}자 해시 ✅");

var hmac2 = ComputeHmac(data, key129);
Console.WriteLine($"129바이트 키: {hmac2.Length}자 해시 ✅");

var hmac3 = ComputeHmac(data, key168);
Console.WriteLine($"168바이트 키: {hmac3.Length}자 해시 ✅");

Console.WriteLine("\n모두 정상 작동! 문제없음! 💯");

static string ComputeHmac(string data, string key)
{
    using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key));
    var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
    return Convert.ToBase64String(hash);
}
