using System;
using System.IO;
using System.Linq;
using Application.Commands;
using Infrastructure.Data;
using Infrastructure.Persistence;
using Microsoft.Practices.Unity;
using Model;

namespace Application {
    public class Program {
        
        [STAThread]
        static void Main(string[] args) {
            var c = Container.CreateContainer();
            var app = c.Resolve<System.Windows.Application>();
            c.Resolve<RegistryPersistence>().AttachTo(c.Resolve<PersonRegistry>());
            app.Startup += (s, e) => c.Resolve<LoadDiskRepositoryCommand>().Execute(null);

            app.Run(app.MainWindow);
        }
    }
}
