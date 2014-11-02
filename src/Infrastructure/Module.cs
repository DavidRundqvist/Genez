using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.DependencyInjection;
using Infrastructure.IO;
using Infrastructure.Persistence;
using Microsoft.Practices.Unity;

namespace Infrastructure {
    public class Module : IModule{
        public void Register(IUnityContainer container) {
            container.RegisterType<IFileSystem, FileSystem>();
            container.RegisterType<ISerializer, XmlSerializer>();
            container.RegisterType<RegistryPersistence>(new ContainerControlledLifetimeManager());
        }
    }
}