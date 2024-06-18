
using System.Text;

using var fs = new FileStream("MyStream.txt", FileMode.OpenOrCreate);
fs.WriteByte(Convert.ToByte('A'));
byte[] bytes = Encoding.UTF8.GetBytes(" is a first character.");
fs.Write(bytes, 0, bytes.Length);


fs.Seek(0, SeekOrigin.Begin);
Console.WriteLine($"First character is -> {Convert.ToChar(fs.ReadByte())}");

byte[] readBuf = new byte[fs.Length -1];

fs.Read(readBuf, 0, Convert.ToInt32(fs.Length) -1);

Console.WriteLine(Encoding.UTF8.GetString(readBuf));
