using System.Windows.Input;

namespace WpfMVVM.MVVM
{
    public class RelayCommand<T>(Action<object> execute, Func<object, bool>? canExecute = null) : ICommand
    {
        private readonly Action<object> execute = execute;

        private readonly Func<object, bool>? canExecute = canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
            => canExecute == null || canExecute(parameter ?? true);

        public void Execute(object? parameter) 
            => execute(parameter ?? "-");
    }
}
