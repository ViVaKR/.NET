using System.Text.Json.Serialization;

namespace CampRecords;

public record class OneLineRecord(int Id, string Name, int Grade);

public record class Person
{
    public string? Name { get; init; }

    public int Age { get; init; }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public void Deconstruct(out string name, out int age)
    {
        name = Name ?? string.Empty;
        age = Age;
    }
}

public readonly record struct Point(double X, double Y, double Z);

/// <summary>
/// Vector3 record type
/// </summary>
/// <param name="X">Point X</param>
/// <param name="Y">Point Y</param>
/// <param name="Z">Point Z</param>
/// <remarks>
/// The Vector3 type is a positional record containing the
/// properties for the X, Y and Z.
/// </remarks>
public record Vector3(
[property: JsonPropertyName("x")] float X,
[property: JsonPropertyName("y")] float Y,
[property: JsonPropertyName("z")] float Z
)
{
    public string Memo{get;set;} = string.Empty;
}
