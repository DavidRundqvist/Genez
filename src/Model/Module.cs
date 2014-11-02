using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.DependencyInjection;
using Microsoft.Practices.Unity;

namespace Model {
    public class Module : IModule {
        public void Register(IUnityContainer container) {
#if DEBUG
            container.RegisterType<PersonRegistry, DesignPersonRegistry>(new ContainerControlledLifetimeManager());
#else
            container.RegisterType<PersonRegistry>(new ContainerControlledLifetimeManager());
#endif
        }
    }
}