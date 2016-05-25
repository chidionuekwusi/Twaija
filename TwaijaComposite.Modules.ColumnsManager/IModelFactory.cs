using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.ColumnsManager
{
    public interface IModelFactory<T> where T:new()
    {
        T Create(object optionalparamter);
    }
}
