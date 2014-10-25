using Microsoft.Practices.Unity;

namespace Common.DependencyInjection {
    public interface IModule {
        void Register(IUnityContainer container);
    }
}