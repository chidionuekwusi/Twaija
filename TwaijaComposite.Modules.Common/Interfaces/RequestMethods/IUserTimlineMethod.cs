using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common;

namespace TwaijaComposite.Modules.Common.Interfaces
{
    public interface IUserTimlineMethod:ITwitterMethod,IRequestMethod<IList<ITweet>>
    {
        /// <summary>
        /// Set the Target ScreenName
        /// </summary>
        string ScreenName { get; set; }
        /// <summary>
        /// Set the Target Number of objects to retrieve
        /// </summary>
        int NumberOfObjectsToRetrieve { get; set; }
    }
}
