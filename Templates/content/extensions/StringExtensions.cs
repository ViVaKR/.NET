public static class StringExtensions
{
    public static string Reverse(this string value)
    {
        var array = value.ToCharArray();
        Array.Reverse(array);
        return new string(array);
    }
}
