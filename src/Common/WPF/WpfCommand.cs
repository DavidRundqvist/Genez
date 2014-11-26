using System;
using System.Windows.Input;

namespace Common.WPF {
    public abstract class WpfCommand : ICommand {
        virtual public bool CanExecute(object parameter) {
            return true;
        }

        public abstract void Execute(object parameter);

        public event EventHandler CanExecuteChanged;
    }
}