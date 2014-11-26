using Common.WPF;

namespace Application.Commands {
    public class ExitCommand : WpfCommand {
        private readonly System.Windows.Application _app;
        public ExitCommand(System.Windows.Application app) {
            _app = app;
        }

        public override void Execute(object parameter) {
            _app.Shutdown();
        }
    }
}