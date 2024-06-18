
namespace DependenceInjector
{
    public class ConstructorInjection (IText text)
    {
        private readonly IText _text = text;

        public void Print()
        {
            _text.Print();
        }
    }
}
