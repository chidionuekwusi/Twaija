using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.Common.Interfaces
{
    public interface ITheme
    {
        string Name { get; }
        string Id { get; }
        void Paint();
    }

}
