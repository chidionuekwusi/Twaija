using System;
using System.Collections.Generic;
using TwaijaComposite.Modules.Common.DataInterfaces;

namespace TwaijaComposite.Modules.Common.Interfaces
{
    public interface IDirectMessageMethod:ITwitterMethod
    {
        IList<ITwitterDirectMessage> Create();
    }
}
