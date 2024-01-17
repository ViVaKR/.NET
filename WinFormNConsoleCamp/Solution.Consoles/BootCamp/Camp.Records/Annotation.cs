using System.Security.Principal;

namespace CampRecords;

/// <summary>
/// This class performs and e.g. Annotation
/// </summary>
public class Annotation
{
    
    /// <summary>
    /// &lt; Test &#61; &#97; Runner &gt;
    /// </summary>
    /// <param name="number"></param>
    /// <param name="name"></param>
    public void Run(int number, string name)
    {
        
        Console.WriteLine($"{number} {name}");
    }
}
