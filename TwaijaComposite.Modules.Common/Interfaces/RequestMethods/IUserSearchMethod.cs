﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.DataInterfaces;

namespace TwaijaComposite.Modules.Common.Interfaces
{
    public interface IUserSearchMethod:ITwitterMethod,IRequestMethod<IList<ITwitterProfile_Small>>
    {
        string TargetScreenName { get; set; }
    }
}
