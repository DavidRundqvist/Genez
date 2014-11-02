using System.Collections.Generic;
using System.Net.Configuration;
using System.Runtime.Remoting.Messaging;
using Common.DependencyInjection;
using Microsoft.Practices.Unity;

namespace Launch {
    public class Container {
        public static IUnityContainer CreateContainer() {

            var container = new UnityContainer();
            var modules = new IModule[] {
                                            new Launch.Module(), 
                                            new View.Module(),
                                            new Model.Module(),
                                            new Infrastructure.Module()
                                        };

            foreach (var module in modules) {
                module.Register(container);
            }

            return container;
        }
    }
}