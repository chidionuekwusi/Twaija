using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace TwaijaComposite.Modules.ColumnsManager.Column
{
   public class ColumnSkeletonFactoryImp:IColumnSkeletonFactory
    {
       private readonly IUnityContainer m_Container;
       public ColumnSkeletonFactoryImp(IUnityContainer container)
       {
           m_Container = container;
       }
       public IColumn Create(string implementationName)
       {
           IColumn columnimp = null;
           if (string.IsNullOrEmpty(implementationName))
           {
               try
               {
                   columnimp = m_Container.Resolve<IColumn>();
               }
               catch (Exception e)
               {
                   throw new Exception("Thrown from Column manager, attempting to resolve Column Implementation", e);
               }
           }
           else
           {
               try
               {
                   columnimp = m_Container.Resolve<IColumn>(implementationName);
               }
               catch (Exception d)
               {
                   throw new Exception("Thrown from ColumnSkeletonfactory implemetation, attempting to resolve Column Implementation with name" + implementationName, d);
               }
           }
           return columnimp;
       }
    }
}
