using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Common.DependencyInjection;
using Common.Enumerable;
using Infrastructure.Data;
using Microsoft.Practices.Unity;
using Model;
using Presentation;
using View;

namespace Launch {
    public class Program {
        
        [STAThread]
        static void Main(string[] args) {
            var c = Container.CreateContainer();
            var app = c.Resolve<Application>();

#if DEBUG
            AddTestDatabase(c);
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
