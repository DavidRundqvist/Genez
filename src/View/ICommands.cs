using System.Windows.Input;

namespace View {
    public interface ICommands {
        ICommand ExitCommand { get; }
        ICommand SaveCommand { get; }
    }
}