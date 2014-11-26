using System;
using System.IO;
using System.Linq;
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
#if DEBUG
            //AddTestDatabase(c);
#endif
            app.Run(app.MainWindow);
        }

        private static void AddTestDatabase(IUnityContainer unityContainer) {
            var repo = GedcomRepository.Parse(new FileInfo(@"..\..\..\Test\TestData\2009-01-04.ged"));
            var people = unityContainer.Resolve<PersonRegistry>();
            people.Add(repo.GetAllPeople().ToArray());
        }
    }
}
