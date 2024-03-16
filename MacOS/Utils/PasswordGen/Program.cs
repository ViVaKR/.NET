using System.Text;
using PasswordGen;

string passwordFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "passwords.txt");

var fi = new FileInfo(passwordFile);
if (fi.Exists)
    fi.Delete();

Console.Write("생성할 비밀번호 갯수 : ");

var count = Convert.ToInt32(Console.ReadLine());
var password = new Password(5, 20);

List<string> passwords = [];

for (int i = 0; i < count; i++)
{
    passwords.Add(await password.CreatePasswordAsync());
}

File.WriteAllLines(fi.FullName, passwords, Encoding.UTF8);

if (fi.Exists)
{
    var text = File.ReadAllText(fi.FullName, Encoding.UTF8);
    Console.WriteLine(text);
}
