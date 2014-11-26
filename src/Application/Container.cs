using Common.DependencyInjection;
using Microsoft.Practices.Unity;

namespace Application {
    public class Container {
        public static IUnityContainer CreateContainer() {

            var container = new UnityContainer();
            var modules = new IModule[] {
                                            new Module(), 
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