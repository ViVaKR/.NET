namespace Camp.Attribs;

[Developer("Viv", "43", Reviewed = true)]
public class App
{
    public static void Run()
    {
        GetAttribute(typeof(App));
    }

    private static void GetAttribute(Type t)
    {
        if (Attribute.GetCustomAttribute(t, typeof(DeveloperAttribute)) is not DeveloperAttribute attr)
            Console.WriteLine("The attribute was not found");
        else
        {
            Console.WriteLine($"Name: {attr.Name}, Level: {attr.Level}, Reviewed: {attr.Reviewed}");
        }
    }
}
