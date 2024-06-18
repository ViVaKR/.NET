namespace CampDeconstructor;

public class Person(int id, string name, int age)
{
    private readonly int _id = id;
    private readonly string _name = name;
    private readonly int _age = age;

    /// <summary>
    /// ? Class Deconstructor
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="age"></param>
    public void Deconstruct(out int id, out string name, out int age)
    {
        id = _id;
        name = _name;
        age = _age;
    }
}
