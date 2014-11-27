using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Common.DependencyInjection;
using Infrastructure.Data;

using Infrastructure.Persistence;
using Microsoft.Practices.Unity;
using Model;

namespace Infrastructure {
    public class Module : IModule{
        public void Register(IUnityContainer container) {
            container.RegisterInstance(new DirectoryInfo(@"..\..\..\Test\TestData\Registry"));
            container.RegisterType<RegistryPersistence>(new ContainerControlledLifetimeManager());
            container.RegisterType<IRepository, DiskRepository>();
        }
    }
}