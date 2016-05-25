using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.Common.Interfaces
{
    public interface IPostMessageServiceRepository
    {
        IList<string> AvailiableServices { get;  }
        IPostMessageService GetService(string Key);
        bool ServiceExists(string Key);

    }
}
