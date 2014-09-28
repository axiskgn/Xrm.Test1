using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Xrm.Test1.WpfUi.Common
{
    public class RelayCommand : ICommand
    {
        #region Fields

        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;
        private readonly object _param;

        #endregion // Fields

        #region Constructors

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        /// <param name="param"></param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null, object param = null)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
            _param = param;
        }

        #endregion // Constructors

        #region ICommand Members

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter)
        {
            if (_param != null)
            {
                _execute(_param);
                return;
            }
            _execute(parameter);
        }

        #endregion // ICommand Members
    }
}
