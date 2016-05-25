using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.Common.Interfaces;
namespace TwaijaComposite.Modules.ColumnsManager.Request
{
    public abstract class TimerBasedRequestTemplate_Twitter<T,B>:TimerBasedRequestTemplate<T,B>,ITwitterRequest where  T:IMessage
    {
        protected ITwitterUser _user;        
        public virtual void ConfigureTwitterUser(ITwitterUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("User cannot be null");
            }
            _user = user ;
            RefreshTime = user.ColumnRefreshTime;
            InitialDelayTime = 0;
        }
    }
}
