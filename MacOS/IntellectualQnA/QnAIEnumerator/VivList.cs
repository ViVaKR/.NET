using System.Collections;

namespace QnAIEnumerator;
    
public sealed class VivList<T> (int length): IEnumerable<T>, IEnumerator<T>
{
    private T[] _array = new T[length];
    private int _pos = -1;

    public T this[int idx]
    {
        get => _array[idx];
        set
        {
            if (idx > _array.Length - 1)
                Array.Resize(ref _array, idx + 1);
            
            _array[idx] = value;
        }
    }

    public IEnumerator<T> GetEnumerator() => this;

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public bool MoveNext() => ++_pos < _array.Length;

    public void Reset() => _pos = -1;

    public T Current =>  this[_pos];

    object? IEnumerator.Current => Current;

    private static void ReleaseUnmanagedResources()
    {
        Console.WriteLine("Release Unmanaged Resource");
    }

    private bool _disposed;
    private void Dispose(bool disposing)
    {
        ReleaseUnmanagedResources();

        if (!_disposed)
        {
            if (disposing)
            {
                Console.WriteLine("can clean up other managed objects");
            } 
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~VivList() => Dispose(false);
}