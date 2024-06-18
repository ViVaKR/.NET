using System.Collections;

namespace BootCamp;

public class People : IEnumerable
{
    private readonly Person[] _people;

    public People(Person[] pArray)
    {
        _people = new Person[pArray.Length];
        for (int i = 0; i < pArray.Length; i++)
        {
            _people[i] = pArray[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// (1)
    /// </summary>
    /// <returns></returns>
    public IEnumerator GetEnumerator() => new PeopleEnum(_people);
}
