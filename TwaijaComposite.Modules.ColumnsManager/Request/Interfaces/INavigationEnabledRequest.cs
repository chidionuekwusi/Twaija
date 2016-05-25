using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;

namespace TwaijaComposite.Modules.ColumnsManager.Request
{
    public interface INavigationEnabledRequest
    {
        void Foward();
        void Backwards();
        //void Response_Foward(IMessage message);
        //void Response_Backwards(IMessage message);
    }
}
