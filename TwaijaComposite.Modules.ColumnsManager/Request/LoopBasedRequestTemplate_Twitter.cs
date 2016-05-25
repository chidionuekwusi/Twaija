using System;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.DataInterfaces;

namespace TwaijaComposite.Modules.ColumnsManager.Request
{
    public class LoopBasedRequestTemplate_Twitter<T,B>:LoopBasedRequestTemplate<T,B>,ITwitterRequest where T:IMessage
    {
        ITwitterUser _user;
        public virtual void ConfigureTwitterUser(ITwitterUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("User cannot be null");
            }
            this._user = user;
        }
    }
}
