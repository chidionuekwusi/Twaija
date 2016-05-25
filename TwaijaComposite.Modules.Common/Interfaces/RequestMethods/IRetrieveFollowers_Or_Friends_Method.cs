using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.DataInterfaces;

namespace TwaijaComposite.Modules.Common.Interfaces
{
    public interface IRetrieveFollowers_Or_Friends_Method:ITwitterMethod,IRequestMethod<IList<ITwitterProfile_Small>>
    {
        /// <summary>
        /// Either ScreenName or UserId should be supplied
        /// </summary>
        string ScreenName { get; set; }
        /// <summary>
        /// Either ScreenName or UserId should be supplied
        /// </summary>
        decimal UserId { get; set; }
        /// <summary>
        /// This determines wether followers or friends are returned the method
        /// </summary>
        TwitterRelationship Relationship { get; set; }
    }
}
