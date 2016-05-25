using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.ColumnsManager.Request;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.ColumnsManager.Filter;
using TwaijaComposite.Modules.ColumnsManager.Viewmodels;
using TwaijaComposite.Modules.Common.Commands;
using System.Windows.Input;
using TwaijaComposite.Modules.Common.Resources;
using TwaijaComposite.Modules.ColumnsManager.Column.ColumnCommands;

namespace TwaijaComposite.Modules.ColumnsManager.Column.Factories
{
    public class TweetSearchFactory:FactoryBase,IColumnPartsFactory
    {
        public string FactoryName
        {
            get { return TwaijaComposite.Modules.Common.Resources.ColumnTypeKeys.TwitterTweetSearchKey; }
        }

        public IRequest CreateRequest(Common.DataInterfaces.IUser user, Dictionary<string, object> parameters)
        {
            IModelFactory<TweetViewmodel> factory = null;
            factory = (!parameters.ContainsKey(TwaijaComposite.Modules.Common.Resources.CreateColumnEventParameters.ModelFactoryKey)) ? null : m_Container.Resolve(typeof(IModelFactory<TweetViewmodel>), parameters[CreateColumnEventParameters.ModelFactoryKey].ToString()) as IModelFactory<TweetViewmodel>;
            var request = m_Container.Resolve(typeof(TweetSearchRequest),string.Empty) as TweetSearchRequest;
            if (parameters.ContainsKey(TwaijaComposite.Modules.Common.Resources.CreateColumnEventParameters.GeographicalLocationKey))
            {
                request.Geo = parameters[TwaijaComposite.Modules.Common.Resources.CreateColumnEventParameters.GeographicalLocationKey].ToString();
            }
            if (parameters.ContainsKey(TwaijaComposite.Modules.Common.Resources.CreateColumnEventParameters.TargetScreenNameKey))
            {
                request.User = user as ITwitterUser;
            }
            request.Query = parameters[TwaijaComposite.Modules.Common.Resources.CreateColumnEventParameters.TextKey].ToString();
            if (factory != null)
            {
                request.Converter.SetFactory<TweetViewmodel>(factory);
            }
           return request;
        }

        public IFilter CreateFilter(Common.DataInterfaces.IUser user)
        {
            IFilter filter = null;
            try
            {
                filter = m_Container.Resolve(typeof(IFilter), string.Empty) as IFilter;
                filter.Criterion.Add(new ScreenNameCriteria());
                filter.Criterion.Add(new TextCriteria());
            }
            catch
            {
            }
            return filter;
        }

        public IList<System.Windows.Input.ICommand> CreateCommandList(IColumn column)
        {
            List<ICommand> commands = new List<ICommand>();
            commands.Add(new IconifiedCommand(new RefreshColumnCommandHelper(column).Execute) { Hint = "Refresh", PathIcon = IconLocator.Refresh });
            commands.Add(new IconifiedCommand(new ClearColumnCommandHelper(column).Execute) { Hint = "Delete Items", PathIcon = IconLocator.Delete });
            return commands;
        }

        public string InitializeHeader(Dictionary<string, object> parameters)
        {
            return "Find:" + parameters[TwaijaComposite.Modules.Common.Resources.CreateColumnEventParameters.TextKey];
        }
    }
}
