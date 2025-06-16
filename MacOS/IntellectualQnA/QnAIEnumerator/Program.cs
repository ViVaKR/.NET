
using QnAIEnumerator;

Console.WriteLine("Hello, World");

const int length = 100;

 var list = new VivList<Member>(length);

for (var i = 0; i < length + 1; i++)
    list[i] = new Member(i + 1, $"Member_{i + 1}");

foreach (var member in list)
{
    var s = member.ToString();
    Console.WriteLine(s);
}
