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
    passwords.Add(await password.CreatePasswordAsync());

await File.WriteAllLinesAsync(fi.FullName, passwords, Encoding.UTF8);

fi = new FileInfo(fi.FullName);

if (fi.Exists)
{
    var text = await File.ReadAllTextAsync(fi.FullName, Encoding.UTF8);
    await Console.Out.WriteLineAsync(text);
}

var runtimeInfo = System.Runtime.InteropServices.RuntimeInformation.RuntimeIdentifier;
var arch = System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture;

Console.WriteLine($"RID: {runtimeInfo}, Architecture: {arch} ");

//$ dotnet publish -c Release -r osx-arm64 --self-contained true
