using System.Collections;

namespace BootCamp;

public class PeopleEnum(Person[] list) : IEnumerator
{
    /// <summary>
    /// (2)
    /// </summary>
    public Person[] _people = list;

    private int position = -1;


    public Person Current
    {
        get
        {
            try
            {
                return _people[position];
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException();
            }
        }
    }

    /// <summary>
    /// (4)
    /// </summary>
    object IEnumerator.Current => Current;

    /// <summary>
    /// (3)
    /// </summary>
    /// <returns></returns>
    bool IEnumerator.MoveNext()
    {
        position++;
        return position < _people.Length;
    }

    void IEnumerator.Reset()
    {
        position = -1;
    }
}
