using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;
using TwaijaComposite.Modules.Options.Viewmodels;
using TwaijaComposite.Modules.Options.Views;
using TwaijaComposite.Modules.Common.Services;
using TwaijaComposite.Modules.Common;


namespace TwaijaComposite.Modules.Options
{
    public class OptionsModule:IModule
    {
        #region fields

        IUnityContainer m_Container;
        IRegionManager m_RegionManager;

        #endregion
        public OptionsModule(IUnityContainer container, IRegionManager manager)
        {
            m_Container = container;
            m_RegionManager = manager;
        }
        public void Initialize()
        {/*Create EditableUser Repository and Populate with Types and Factories*/
            m_Container.RegisterType<TwitterEditableUser>();
            m_Container.RegisterType<TwitterEditableUserFactory>(new ContainerControlledLifetimeManager());
            EditableUserFactoryRepository factoryRepository = new EditableUserFactoryRepository();
            factoryRepository.Repository.Add(typeof(TwitterUser), m_Container.Resolve<TwitterEditableUserFactory>());
            m_Container.RegisterInstance<EditableUserFactoryRepository>(factoryRepository);
            m_Container.RegisterType<IAccountViewmodel,AccountViewmodel>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IAppearanceViewmodel,AppearanceViewmodel>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IGeneralViewmodel, GeneralViewModel>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<INotificationsViewmodel,NotificationsViewmodel>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IServicesViewmodel,ServicesViewmodel>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<ITwitterOptionsViewmodel,TwitterOptionsViewmodel>(new ContainerControlledLifetimeManager());
            //This registers the view that will be navigated to by the Navigation Service, using the registered View name...
            m_Container.RegisterType<object, Layout>(ViewNames.OptionsView, new ContainerControlledLifetimeManager());
            //m_RegionManager.RegisterViewWithRegion(RegionNames.OptionsSpace, typeof(ServicesView));
            m_RegionManager.RegisterViewWithRegion(RegionNames.OptionsSpace, typeof(GeneralView));
            m_RegionManager.RegisterViewWithRegion(RegionNames.OptionsSpace, typeof(AccountsView));
            m_RegionManager.RegisterViewWithRegion(RegionNames.OptionsSpace,typeof(TwitterOptionsView));
            //m_RegionManager.RegisterViewWithRegion(RegionNames.OptionsSpace,typeof(NotificationsView));
          // m_RegionManager.RegisterViewWithRegion(RegionNames.OptionsSpace, typeof(AppearanceView));
            m_Container.RegisterType<IController, OptionsController>("OptionsManager",new ContainerControlledLifetimeManager());
        }
    }
}
