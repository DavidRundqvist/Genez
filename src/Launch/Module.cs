using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Common;
using Common.DependencyInjection;
using Microsoft.Practices.Unity;
using View;
using View.MainWindow;


namespace Launch {
    public class Module : IModule{
        public void Register(IUnityContainer container) {
            container.RegisterFactory<Application>(c => {
                                          var app = new Application();
                                          var mainWindow = c.Resolve<MainWindow>();
                                          app.MainWindow = mainWindow;
                                          return app;
                                      });

        }
    }
}