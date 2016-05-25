//using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Unity;
//using Microsoft.Practices.Composite.Regions;
using TwaijaComposite.Modules.Common.Services;
//using Microsoft.Practices.Composite.Events;
//using TwaijaComposite.Modules.Common.Events;
using TwaijaComposite.Modules.Common.Interfaces;
//using TwaijaComposite.Modules.Clipboard.PictureServiceImps;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Clipboard.Viewmodels;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace TwaijaComposite.Modules.Clipboard
{
#if !SILVERLIGHT
    [Module(ModuleName="ClipboardModule")]
#endif
    public class ClipboardModule:IModule
    {

          #region fields
       protected readonly IUnityContainer m_Container;
       protected readonly IRegionManager m_RegionManager;
       protected readonly string ModuleName = "ClipboardModule";
       #endregion

       #region Contructor
       public ClipboardModule(IUnityContainer container, IRegionManager manager)
       {
           this.m_Container = container;
           m_RegionManager = manager;
       }
       #endregion
        #region IModule Members

        public void Initialize()
        {
            RegisterViewsAndViewModels();
        }

        private void RegisterViewsAndViewModels()
        {
            m_Container.RegisterType<IPictureTray, PictureTrayImp>();
            m_Container.RegisterType<IClipboardViewmodel, ClipboardViewmodel>();
            m_Container.RegisterType<ClipboardView>(new ContainerControlledLifetimeManager());
            m_RegionManager.RegisterViewWithRegion(RegionNames.ClipboardSpace, ()=>m_Container.Resolve<ClipboardView>());
        }

        #endregion
    }
}
