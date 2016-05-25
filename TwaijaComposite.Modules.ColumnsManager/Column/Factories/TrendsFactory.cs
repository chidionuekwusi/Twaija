using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.ColumnsManager.Request;
using TwaijaComposite.Modules.Common.Resources;

namespace TwaijaComposite.Modules.ColumnsManager.Column.Factories
{
    public class TrendsFactory:FactoryBase,IColumnPartsFactory
    {
        public string FactoryName
        {
            get { return ColumnTypeKeys.TwitterTrendsKey; }
        }

        public IRequest CreateRequest(Common.DataInterfaces.IUser user, Dictionary<string, object> parameters)
        {
            TrendsRequest request = m_Container.Resolve(typeof(TrendsRequest),string.Empty) as TrendsRequest;
            request.Location = parameters[CreateColumnEventParameters.GeographicalLocationKey].ToString();
            request.WOEID =Convert.ToInt32(parameters[CreateColumnEventParameters.WOEIDKey]);
            return request;
        }

        public IFilter CreateFilter(Common.DataInterfaces.IUser user)
        {
            return null;
        }

        public IList<System.Windows.Input.ICommand> CreateCommandList(IColumn column)
        {
            return null;
        }

        public string InitializeHeader(Dictionary<string, object> parameters)
        {
            return "Trends:" + parameters[CreateColumnEventParameters.GeographicalLocationKey];
        }
    }
}
