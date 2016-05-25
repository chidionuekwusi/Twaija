using System;
using System.Collections.Generic;
using TwaijaComposite.Modules.Common.Interfaces;
using Twitterizer;
using TwaijaComposite.Modules.Common.DataInterfaces;

namespace TwaijaComposite.RequestAdapterModule
{
    public class RetrieveTrendsMethodImp : IRetrieveTrendsMethod
    {

        public int WOEID
        {
            get;
            set;
        }

        public string Location
        {
            get;
            set;
        }
        IList<ITrend> Convert(TwitterTrendCollection collection)
        {
            IList<ITrend> trends = new List<ITrend>();
            foreach (TwitterTrend trend in collection)
            {
                try
                {
                    trends.Add(new TrendAdapter(trend));
                }
                catch
                {
                }
            }
            return trends;
        }
        public IList<Modules.Common.DataInterfaces.ITrend> Create(Modules.Common.Navigation direction)
        {
             IList<ITrend> trends = null;
             try
             {
                 var rawTrends = TwitterTrend.Trends(WOEID).ResponseObject;
                 trends = Convert(rawTrends);
             }
             catch (Exception)
             {

             }
             return trends;
        }
    }
}
