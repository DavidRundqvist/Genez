using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.DependencyInjection;
using Microsoft.Practices.Unity;
using View.Global;
using View.MainWindow;
using View.PersonList;

namespace View {
    public class Module : IModule{
        public void Register(IUnityContainer container) {
            container.RegisterFactory(c => {
                                          var window = new MainWindow.MainWindow();
                                          window.DataContext = c.Resolve<MainWindowPresentation>();
                                          return window;
                                      });
            container.RegisterType<MainWindowPresentation>(new ContainerControlledLifetimeManager());
            container.RegisterType<AllPeople>(new ContainerControlledLifetimeManager());
            container.RegisterType<SelectedPeople>(new ContainerControlledLifetimeManager());

        }
    }
}