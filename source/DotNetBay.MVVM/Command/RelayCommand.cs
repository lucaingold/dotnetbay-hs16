using System;
using System.Diagnostics;
using System.Windows.Input;

namespace DotNetBay.MVVM.Command
{
    public class RelayCommand<Window> : ICommand
    {
        #region Members

        readonly Func<Boolean> canExecute;

        readonly Action execute;

        #endregion

        #region Constructors

        public RelayCommand(Action execute) : this(execute, null)
        {

        }

        public RelayCommand(Action execute, Func<Boolean> canExecute)
        {
            if (execute == null)

                throw new ArgumentNullException("execute");

            this.execute = execute;

            this.canExecute = canExecute;
        }

        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (this.canExecute != null)
                    CommandManager.RequerySuggested += value;
            }

            remove
            {
                if (this.canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }

        [DebuggerStepThrough]

        public Boolean CanExecute(Object parameter)

        {

            return this.canExecute == null ? true : this.canExecute();

        }

        public void Execute(Object parameter)

        {

            this.execute();

        }
        #endregion
    }
}
