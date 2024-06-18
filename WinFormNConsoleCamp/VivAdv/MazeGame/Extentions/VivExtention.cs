
using System.Reflection;


namespace MazeGame.Extentions
{
    public static class VivExtention
    {
        public static void SetDoubleBuffered<T>(this T panel) where T : Control
        {
            typeof(T).InvokeMember(
                "DoubleBuffered",
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.SetProperty, null, panel, new object[] { true });
        }
    }
}
