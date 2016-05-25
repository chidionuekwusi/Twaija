using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using TwaijaComposite.Modules.Common;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Events;

namespace TwaijaComposite.Modules.PictureViewer
{
#if !SILVERLIGHT
    [Module(ModuleName = "PictureViewerModule")]
#endif
    public class PictureViewerModule:IModule
    {
        #region fields
        protected readonly IUnityContainer m_Container;
        protected readonly IRegionManager m_RegionManager;
        #endregion

        #region Constructor
       public PictureViewerModule(IUnityContainer container, IRegionManager manager)       
        {
            m_Container=container;
            m_RegionManager=manager;
            //var eventagg = m_Container.Resolve<IEventAggregator>();
            //var eve = eventagg.GetEvent<TwaijaComposite.Modules.Common.Events.ViewRequestedEvent>();
            //eve.Publish(new TwaijaComposite.Modules.Common.Events.ViewRequestedEventArgs() { context = this, RegionRequested = "", ViewRequested = "" });
           
        }
        #endregion

        #region IModule Members

        public  void Initialize()
        {
        
        }

        #endregion
    }
}
