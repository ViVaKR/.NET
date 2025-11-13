namespace Demo;

public static class Exts
{
    // Extension block
    extension<T>(IEnumerable<T> source)
    {
        // Extension property
        public bool IsEmpty => !source.IsEmpty;
    }
}
