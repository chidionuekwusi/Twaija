using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.Common.Services;
using System.Windows;
using Microsoft.Practices.Prism.Events;
using TwaijaComposite.Modules.Common;
using System.Configuration;

namespace TwaijaComposite
{
    public class ShellModule : IModule
    {
        #region fields

        IUnityContainer m_Container;
        ResourceManager resourceManager;
        #endregion
        public ShellModule(IUnityContainer container)
        {
            m_Container = container;
        }
        public void Initialize()
        {
         
            RegisterTypes();
        }

        private void RegisterTypes()
        {
            RefreshManager.AssignDispatcher(Application.Current.Dispatcher);          
            m_Container.RegisterType<IDispatcher, RefreshManager>();


            RefreshManager.AssignDispatcher(Application.Current.Dispatcher);
            m_Container.RegisterType<IDispatcher, RefreshManager>();
            m_Container.RegisterType<ShellViewmodel>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IShellRequestHandler, ApplicationPaintEventHandler>(InfrastructureResourceLocator.ThemeChangeRequestHandlerKey, new ContainerControlledLifetimeManager());
            //Register All Themes before this is created.
            resourceManager = new ResourceManager(m_Container.Resolve<IEventAggregator>(), m_Container.ResolveAll<IShellRequestHandler>());
        }
    }
}
