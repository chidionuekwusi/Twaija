using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.DataInterfaces;

namespace TwaijaComposite.Modules.Common.Interfaces
{
    public interface IRetrieveTwitterProfile_LargeMethod:ITwitterMethod,IRequestMethod<ITwitterProfile_Large>
    {
        string TargetScreenName { get; set; }
    }
}
