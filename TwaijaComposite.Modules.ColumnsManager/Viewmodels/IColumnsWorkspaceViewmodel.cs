using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace TwaijaComposite.Modules.ColumnsManager.Viewmodels
{
   public interface IColumnsWorkspaceViewmodel
    {
       IList<object> Content { get; }
       void Add(object data);
       void Remove(object data);
    }
}
