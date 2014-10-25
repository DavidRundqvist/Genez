using System.Collections.Generic;
using Common.DependencyInjection;
using Microsoft.Practices.Unity;

namespace Launch {
    public class Container {
        public static IUnityContainer CreateContainer() {

            var container = new UnityContainer();
            var modules = new IModule[] {
                                            new Launch.Module(), 
                                            new Presentation.Module(), 
                                            new View.Module()
                                        };

            foreach (var module in modules) {
                module.Register(container);
            }

            return container;
        }
    }
}