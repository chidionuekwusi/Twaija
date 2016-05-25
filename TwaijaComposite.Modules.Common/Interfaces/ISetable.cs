using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.Common
{
    public interface ISetable<B>
    {
        void Set(B param);
    }
}
