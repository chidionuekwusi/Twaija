
using Microsoft.Practices.Unity;

namespace TwaijaComposite.Modules.ColumnsManager.Column.Factories
{
   public abstract class FactoryBase
    {
       [Dependency]
       public IUnityContainer m_Container { get; set; }
 
    }
}
