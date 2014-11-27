using System.Windows.Input;

namespace View {
    public interface ICommands {
        ICommand ExitCommand { get; }
        ICommand ExportCommand { get; }
        ICommand ImportCommand { get; }
    }
}