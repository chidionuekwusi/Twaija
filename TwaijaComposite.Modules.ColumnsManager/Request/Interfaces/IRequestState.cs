using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.ColumnsManager
{
    public interface IRequestState<T> where T:class
    {
        void Start(T context);
        void Restart(T context);
        void Stop(T context);
        void Dispose(T context);
    }
   
}
