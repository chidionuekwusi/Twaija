using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.Common.Interfaces
{
    public interface IRequestMethod<T>
    {
        T Create(TwaijaComposite.Modules.Common.Navigation direction);
    }
}
