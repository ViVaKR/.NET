
using System.Xml.Serialization;
using DemoA;

XmlSerializer serializer = new(typeof(UserList));

UserList userList = new()
{
    Items =
    [
        new() { Id = 1, Name = "Alice" },
        new() { Id = 2, Name = "Bob" },
        new() { Id = 3, Name = "Charlie" }
    ]

};

serializer.Serialize(Console.Out, userList);
using var writer = new StreamWriter("user_list.xml");
serializer.Serialize(writer, userList);


UserList? list = serializer.Deserialize(new StreamReader("user_list.xml")) as UserList;

if (list is not null)
{
    foreach (User user in list.Items)
    {
        Console.WriteLine($"Id: {user.Id}, Name: {user.Name}");
    }
}

