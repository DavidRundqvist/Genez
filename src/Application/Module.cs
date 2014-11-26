using System.Collections.Generic;
using Application.Commands;
using Common.DependencyInjection;
using Common.WPF.Presentation;
using Microsoft.Practices.Unity;
using View;
using View.MainWindow;

namespace Application {
    public class Module : IModule{
        public void Register(IUnityContainer container) {
            container.RegisterFactory<System.Windows.Application>(c => {
                                          var app = new System.Windows.Application();
                                          var mainWindow = c.Resolve<MainWindow>();
                                          app.MainWindow = mainWindow;
                                          return app;
                                      }, new ContainerControlledLifetimeManager());

            container.RegisterType<ICommands, AllCommands>();

        }
    }
}