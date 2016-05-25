using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.Practices.Composite.Regions;
using System.Windows;
using Microsoft.Practices.ServiceLocation;
using System.Windows.Controls;
//using Microsoft.Practices.Composite.Presentation.Regions;
using System.Windows.Controls.Primitives;
using Microsoft.Practices.Prism.Regions;

namespace TwaijaComposite.Modules.Common.Behaviors
{
   public  class RegionAdapterLocator
    {
       static Dictionary<Type, IRegionAdapter> mappings;
       static IServiceLocator m_Locator;
       public RegionAdapterLocator()
       {
           
       }
       static RegionAdapterLocator()
       {
           m_Locator = ServiceLocator.Current;
           mappings = new Dictionary<Type, IRegionAdapter>();
           mappings.Add(typeof(Selector), m_Locator.GetInstance<SelectorRegionAdapter>());
           mappings.Add(typeof(ContentControl), m_Locator.GetInstance<ContentControlRegionAdapter>());
           mappings.Add(typeof(ItemsControl), m_Locator.GetInstance<ItemsControlRegionAdapter>());

       }
      public IRegionAdapter Locate(DependencyObject target)
      {
          return GetRegionAdapter(target.GetType());
      }
      public IRegionAdapter GetRegionAdapter(Type currentType)
      {
          while (currentType != null)
          {
              if (mappings.ContainsKey(currentType))
              {
                  return mappings[currentType];
              }
              currentType = currentType.BaseType;          
          }
          throw new Exception("Couldnt find AdaPTER");
      }
    }
}
