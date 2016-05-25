using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;
using TwaijaComposite.Modules.ProfileViewer.ProfileManager;
using TwaijaComposite.Modules.ProfileViewer.ProfileEventHandlers;
using TwaijaComposite.Modules.Common;
using Microsoft.Practices.Prism.Events;

namespace TwaijaComposite.Modules.ProfileViewer
{
#if !SILVERLIGHT
    [Module(ModuleName = "ProfileViewerModule")]
#endif
    public class ProfileViewerModule:IModule
    {
          #region fields
       protected readonly IUnityContainer m_Container;
       protected readonly IRegionManager m_RegionManager;
       #endregion



       #region Contructor
       public ProfileViewerModule(IUnityContainer container, IRegionManager manager)
       {
           this.m_Container = container;
           m_RegionManager = manager;
       }
       #endregion
        #region IModule Members

        public void Initialize()
       {
            m_Container.RegisterType<ProfileManagerView>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IProfileEventHandler, TwitterProfileHandler>(TwaijaComposite.Modules.Common.Resources.ProfileHandlerTypeKeys.TwitterProfileKey,new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IProfileEventHandler, TwitterUserSearchHandler>(TwaijaComposite.Modules.Common.Resources.ProfileHandlerTypeKeys.TwitterUserSearchKey, new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IProfileEventHandler, TwitterConversationHandler>(TwaijaComposite.Modules.Common.Resources.ProfileHandlerTypeKeys.TwitterConversationThreadKey, new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IProfileController, Manager>(new ContainerControlledLifetimeManager(),new InjectionConstructor(new ResolvedParameter<ProfileManagerView>(), m_Container.ResolveAll<IProfileEventHandler>(),new ResolvedParameter<IRegionManager>(),new ResolvedParameter<IEventAggregator>()));
            m_Container.RegisterInstance<IController>("ProfileManager",m_Container.Resolve<IProfileController>());
            m_Container.RegisterType<ProfileManagerViewmodel>(new ContainerControlledLifetimeManager());
            m_Container.RegisterInstance<Manager>(m_Container.Resolve<IController>("ProfileManager") as Manager);
           // m_RegionManager.RegisterViewWithRegion(RegionNames.ProfileSpace, typeof(ProfileManagerView));
        }

        #endregion
    }
}
