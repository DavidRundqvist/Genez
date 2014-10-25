using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.DependencyInjection;
using Microsoft.Practices.Unity;

namespace Model {
    public class Module : IModule {
        public void Register(IUnityContainer container) {
            container.RegisterType<PersonRegistry>(new ContainerControlledLifetimeManager());
        }
    }
}