using System;
using System.Collections.Generic;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.Common.Interfaces;

namespace TwaijaComposite.Modules.ColumnsManager.Request
{
    public class TrendsRequest:LoopBasedRequestTemplate<AggregateMessage<TrendsViewmodel>,IList<ITrend>>
    {
        IRetrieveTrendsMethod method;
        public string Location { set { method.Location = value; } }
        public int WOEID { set { method.WOEID = value; } }
        public TrendsRequest(IRetrieveTrendsMethod method)
        {
            this.method = method;
        }
        protected override IList<ITrend> Request()
        {
            return method.Create(Common.Navigation.None);
        }
        protected override void Action(Common.IMessage message)
        {
            OnNewMessage(message, ColumnDirective.AddAndClear);
        }
    }
}
