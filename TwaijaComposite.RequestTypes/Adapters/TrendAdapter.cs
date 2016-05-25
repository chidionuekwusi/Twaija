using System;
using TwaijaComposite.Modules.Common.DataInterfaces;
using Twitterizer;

namespace TwaijaComposite.RequestAdapterModule
{
    public class TrendAdapter:ITrend
    {
        public TrendAdapter(TwitterTrend trend)
        {
            Name = trend.Name;
            Address = trend.Address;
            Event = trend.Events;
            PromotedContent = trend.PromotedContent;
            SearchQuery = trend.SearchQuery;
        }

        public string Address
        {
            get;
            set;
        }

        public string Event
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string PromotedContent
        {
            get;
            set;
        }

        public string SearchQuery
        {
            get;
            set;
        }
    }
}
