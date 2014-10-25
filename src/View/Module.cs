using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.DependencyInjection;
using Microsoft.Practices.Unity;
using Presentation;

namespace View {
    public class Module : IModule{
        public void Register(IUnityContainer container) {
            container.RegisterFactory(c => {
                                          var window = new MainWindow();
                                          window.DataContext = c.Resolve<MainWindowPresentation>();
                                          return window;
                                      });
        }
    }
}