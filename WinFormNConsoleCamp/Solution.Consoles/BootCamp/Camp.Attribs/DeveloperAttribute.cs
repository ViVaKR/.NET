namespace Camp.Attribs;

[AttributeUsage(AttributeTargets.All)]
public class DeveloperAttribute(string name, string level) : Attribute
{
    public virtual string Name { get; set; } = name;
    public virtual string Level { get; set; } = level;
    public virtual bool Reviewed { get; set; } = false;
}
