namespace DependenceInjector
{
    public class Client
    {
        private ISet? _iset;

        public void Run(ISet iset)
        {
            _iset = iset;
            Console.WriteLine("Start");
            _iset.Print();
        }
    }
}
