//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.Practices.Composite.Modularity;
using TwaijaComposite.Modules.Common;
using Microsoft.Practices.Unity;
//using Microsoft.Practices.Composite.Regions;
using TwaijaComposite.Modules.Common.Services;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common.Preferencing;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace TwaijaComposite.Modules.Authentication
{
#if !SILVERLIGHT
    [ModuleDependency("RequestAdapterMethodsModule")]
    [Module(ModuleName="AuthenticationModule")]
#endif
    public class AuthenticationModule:IModule
    {
        #region fields
        protected readonly IUnityContainer m_Container;
        protected readonly IRegionManager m_RegionManager;
        #endregion
        public AuthenticationModule(IUnityContainer container, IRegionManager manager)
        {
            m_Container = container;
            m_RegionManager = manager;
        }
        #region IModule Members

        public void Initialize()
        {
            RegisterViewsAndViewModels();
        }

        private void RegisterViewsAndViewModels()
        {
            //m_Container.RegisterType<IAuthenticateStrategy,MockAuthenticationStrategy>("Mock");
            m_Container.RegisterType<IAuthenticateStrategy, PinAuthenticationStrategy>();
            //m_Container.RegisterType<IAuthenticateStrategy, FacebookAuthenticationStrategy>("Facebook");
            m_Container.RegisterType<Authentication.ViewModels.AuthenticationViewModel>(new ContainerControlledLifetimeManager(), new InjectionConstructor(new ResolvedParameter<Preferences>(), new ResolvedParameter<INavigationService>(ServiceKeys.MainRegionNavigationService),new ResolvedParameter<IAuthenticateStrategy>(),m_Container.ResolveAll<IAuthenticateStrategy>()));
            m_Container.RegisterType<object, AuthenticationView>(ViewNames.AuthenticationView);

        }
        #endregion
    }
}
