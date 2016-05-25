using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.Common.Interfaces
{
    public interface ITweetSearchMethod:ITwitterMethod,IRequestMethod<IList<ITweet>>
    {
        /// <summary>
        /// The query that is to be made..
        /// </summary>
        string Query { get; set; }
        /// <summary>
        /// If U want to search within a specific location/region , Set this Property
        /// </summary>
        string GeoLocation { get; set; }
         
    }
}
