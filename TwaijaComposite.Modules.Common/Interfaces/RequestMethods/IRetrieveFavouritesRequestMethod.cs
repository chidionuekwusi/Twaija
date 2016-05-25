using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.Common.Interfaces
{
    public interface IRetrieveFavouritesMethod:ITwitterMethod,IRequestMethod<IList<ITweet>>
    {
        /// <summary>
        /// Target User Screename
        /// </summary>
        string ScreenName { get; set; }
    }
}
