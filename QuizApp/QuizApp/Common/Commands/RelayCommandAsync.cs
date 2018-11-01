using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuizApp.Common.Commands
{
    public class RelayCommandAsync<T> : ICommand
    {
        private readonly Func<T, Task> _executedMethod;
        private readonly Func<T, bool> _canExecuteMethod;

        public event EventHandler CanExecuteChanged;

        public RelayCommandAsync(Func<T, Task> execute) : this(execute, null) { }

        public RelayCommandAsync(Func<T, Task> execute, Func<T, bool> canExecute)
        {
            _executedMethod = execute ?? throw new ArgumentNullException("execute");
            _canExecuteMethod = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecuteMethod == null || _canExecuteMethod((T)parameter);
        public async void Execute(object parameter) => await _executedMethod((T)parameter);
        public void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}