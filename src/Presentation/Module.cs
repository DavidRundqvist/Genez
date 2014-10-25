﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Common.DependencyInjection;
using Microsoft.Practices.Unity;

namespace Presentation {
    public class Module : IModule{
        public void Register(IUnityContainer container) {
            container.RegisterType<MainWindowPresentation>(new ContainerControlledLifetimeManager());
            container.RegisterType<PersonListPresentation>(new ContainerControlledLifetimeManager());
            container.RegisterFactory(c => new ObservableCollection<PersonPresentation>(), new ContainerControlledLifetimeManager());
        }
    }
}