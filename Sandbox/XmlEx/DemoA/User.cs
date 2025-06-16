using System.Xml.Serialization;

namespace DemoA;

public class User
{
    [XmlElement(ElementName = "id")]
    public int Id { get; set; }

    [XmlElement(ElementName = "name")]
    public string? Name { get; set; }
}
