using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using TwaijaComposite.Modules.Common.Services;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Events;
using TwaijaComposite.Modules.Common.Events;
using TwaijaComposite.Modules.Common;

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
            RefreshManager.AssignDispatcher(Application.Current.RootVisual.Dispatcher);
            m_Container.RegisterType<IDispatcher, RefreshManager>();
            m_Container.RegisterType<ShellViewmodel>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IShellRequestHandler, ApplicationPaintEventHandler>(InfrastructureResourceLocator.ThemeChangeRequestHandlerKey, new ContainerControlledLifetimeManager());
            //Register All Themes before this is created.
            resourceManager = new ResourceManager(m_Container.Resolve<IEventAggregator>(),m_Container.ResolveAll<IShellRequestHandler>());
        }
    }
}
