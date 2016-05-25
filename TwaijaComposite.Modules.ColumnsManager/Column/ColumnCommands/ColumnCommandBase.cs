using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.ColumnsManager.Request;
using System.Threading;

namespace TwaijaComposite.Modules.ColumnsManager.Column.ColumnCommands
{
   public abstract class ColumnCommandHelperBase:IDisposable
    {
       protected IColumn _target;
       public ColumnCommandHelperBase(IColumn target)
       {
           _target = target;
       }
       public abstract void Execute(object state);
       public virtual void Dispose()
       {
           _target = null;
       }
}
   public class RefreshColumnCommandHelper:ColumnCommandHelperBase
   {
       public RefreshColumnCommandHelper(IColumn target)
           : base(target)
       {
       }
       public override void Execute(object state)
       {
           _target.Request.Restart();
       }
   }
   public class ClearColumnCommandHelper : ColumnCommandHelperBase
   {
       public ClearColumnCommandHelper(IColumn target):base(target)
       {
       }
       public override void Execute(object state)
       {
           ThreadPool.QueueUserWorkItem((s) =>
           {
               _target.ClearContent();
           });
       }
   }
   public class FowardColumnCommandHelper : ColumnCommandHelperBase
   {
       INavigationEnabledRequest navigatable;
       public FowardColumnCommandHelper(IColumn target)
           : base(target)
       {
           if (!(target.Request is INavigationEnabledRequest))
           {
               throw new ArgumentException("An INavigationEnabledRequest is needed");
           }
           navigatable = target.Request as INavigationEnabledRequest;
       }
       public override void Execute(object state)
       {
           ThreadPool.QueueUserWorkItem((s) =>
           {
               navigatable.Foward();
           });
       }
   }
   public class BackwardColumnCommandHelper : ColumnCommandHelperBase
   {
       INavigationEnabledRequest navigatable;
       public BackwardColumnCommandHelper(IColumn target)
           : base(target)
       {
           if (!(target.Request is INavigationEnabledRequest))
           {
               throw new ArgumentException("An INavigationEnabledRequest is needed");
           }
           navigatable = target.Request as INavigationEnabledRequest;
       }
       public override void Execute(object state)
       {
           ThreadPool.QueueUserWorkItem((s) =>
           {
               navigatable.Backwards();
           });
       }
   }
}
