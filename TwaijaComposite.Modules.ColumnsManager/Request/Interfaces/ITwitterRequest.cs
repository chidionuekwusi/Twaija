using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.DataInterfaces;

namespace TwaijaComposite.Modules.ColumnsManager.Request
{
    public interface ITwitterRequest
    {
        void ConfigureTwitterUser(ITwitterUser user);
    }
}
