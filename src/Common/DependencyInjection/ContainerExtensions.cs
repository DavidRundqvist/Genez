using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace Common.DependencyInjection {
    public static class ContainerExtensions {

        public static void RegisterAll<T>(this IUnityContainer self, IDictionary<string, Func<IUnityContainer, T>> types)
        {
            foreach (var type in types)
            {
                self.RegisterFactory<T>(type.Key, type.Value);               
            }            
        }

        public static IEnumerable<T> ResolveAll<T>(this IUnityContainer self, IEnumerable<string> names)
        {
            return from name in names select self.Resolve<T>(name);
        }




        #region Factories
        public static void RegisterFactory<T>(this IUnityContainer self, Func<IUnityContainer, T> factory)
        {            
            self.RegisterType<T>(new InjectionFactory(c => factory(c)));
        }
        public static void RegisterFactory<T>(this IUnityContainer self, string name, Func<IUnityContainer, T> factory)
        {
            self.RegisterType<T>(name, new InjectionFactory(c => factory(c)));
        }
        public static void RegisterFactory<T>(this IUnityContainer self, Func<IUnityContainer, T> factory, LifetimeManager manager)
        {
            self.RegisterType<T>(manager, new InjectionFactory(c => factory(c)));
        }
        public static void RegisterFactory<T>(this IUnityContainer self, string name, Func<IUnityContainer, T> factory, LifetimeManager manager)
        {
            self.RegisterType<T>(name, manager, new InjectionFactory(c => factory(c)));
        }
        #endregion
    }
    
}
