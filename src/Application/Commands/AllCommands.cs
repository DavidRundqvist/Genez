using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Common.Coding;
using Common.WPF.Presentation;
using Microsoft.Practices.Unity;
using View;

namespace Application.Commands {
    public class AllCommands : ICommands {
        private readonly IUnityContainer _container;
        public AllCommands(IUnityContainer container) {
            _container = container;
        }

        public ICommand ExitCommand { get { return _container.Resolve<ExitCommand>(); } }
        public ICommand SaveCommand { get { return _container.Resolve<SaveCommand>(); } }
    }
}