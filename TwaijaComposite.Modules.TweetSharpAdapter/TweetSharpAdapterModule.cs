using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwaijaComposite.Modules.Common;

namespace TwaijaComposite.Modules.TweetSharpAdapter
{
    public class TweetSharpAdapterModule:IModule
    {
        private IUnityContainer m_Container;
        public TweetSharpAdapterModule(IUnityContainer container)
        {
            m_Container = container;
        }
        public void Initialize()
        {
            m_Container.RegisterType<ITwitterUpdateAccountImageMethod, UpdateTwitterAccountImageMethodImp>();
        }
    }
}
