using System.Collections.Generic;
using System.Windows.Input;
using Common.WPF.Presentation;
using View.Properties;

namespace View.MainWindow {
    public class DesignCommands : ICommands {
        public ICommand ExitCommand { get; private set; }

        public ICommand SaveCommand {
            get { throw new System.NotImplementedException(); }
        }
    }
}