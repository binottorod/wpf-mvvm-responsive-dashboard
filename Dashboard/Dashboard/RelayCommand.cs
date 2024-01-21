using System;
using System.Windows.Input;

namespace Dashboard
{
    /// <summary>
    /// RelayCommand allow you to inject the command's logic via delegates passed into its constructor. This method
    /// enables ViewModel classes to implement commands in a concise manner.
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Fields

        private Action<object> execute;
        private Func<object, bool> canExecute;
        #endregion

        #region Constructor

        public RelayCommand(Action<object> execute)
        {
            this.execute = execute;
            this.canExecute = null;
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        #endregion

        /// <summary>
        /// CanExecuteChanged delegates the event subscription to the Commandmanager.RequerySuggested event.
        /// This ensures that the WPF commanding infra asks all RelayCommand objects if they can execute whenever
        /// it asks the built-in commands.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
