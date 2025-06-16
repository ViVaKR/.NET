using System.Xml.Serialization;

namespace DemoA;

[XmlRoot(ElementName = "user_list")]
public class UserList
{
    [XmlElement(ElementName = "user")]
    public List<User> Items { get; set; }

    public UserList()
    {
        Items = [];
    }
}
