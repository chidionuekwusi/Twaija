
using System.Collections.Generic;
using TwaijaComposite.Modules.Common.DataInterfaces;

namespace TwaijaComposite.Modules.Common.Interfaces
{
    public interface IHomeTimeline:ITwitterMethod,IRequestMethod<IList<ITweet>>
    {

    }
}
