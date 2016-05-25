using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.ColumnsManager.Request;
using TwaijaComposite.Modules.ColumnsManager.Filter;
using TwaijaComposite.Modules.ColumnsManager.Viewmodels;
using System.Windows.Input;
using TwaijaComposite.Modules.Common.Commands;
using TwaijaComposite.Modules.Common.Resources;
using TwaijaComposite.Modules.ColumnsManager.Column.ColumnCommands;

namespace TwaijaComposite.Modules.ColumnsManager.Column.Factories
{
    public class ConversationFactory:FactoryBase,IColumnPartsFactory
    {

        public string FactoryName
        {
            get { return ColumnTypeKeys.TwitterConversationKey; }
        }

        public IRequest CreateRequest(Common.DataInterfaces.IUser user, Dictionary<string, object> parameters)
        {
            IModelFactory<TweetViewmodel> factory = null;
            var helper = TwitterFactoryHelper.Create();
            var request = helper.CreateAndConfigureRequest<TweetViewmodel, ConversationRequest>(user, parameters, m_Container, out factory);
            if (parameters.ContainsKey(CreateColumnEventParameters.TweetKey))
            {
                request.FirstTweet = parameters[CreateColumnEventParameters.TweetKey] as ITweet;
            }
            else
            {
                request.TweetId = Convert.ToDecimal(parameters[CreateColumnEventParameters.TweetIdKey]);
            }
            if (factory != null)
            {
                request.Converter.SetFactory<TweetViewmodel>(factory);
            }
            return request;
        }

        public IFilter CreateFilter(Common.DataInterfaces.IUser user)
        {
            try
            {
                var filter = m_Container.Resolve(typeof(IFilter),string.Empty) as IFilter;
                filter.Criterion.Add(new ScreenNameCriteria());
                filter.Criterion.Add(new TextCriteria());
                return filter;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public IList<System.Windows.Input.ICommand> CreateCommandList(IColumn column)
        {
            List<ICommand> commands = new List<ICommand>();
            commands.Add(new IconifiedCommand(new RefreshColumnCommandHelper(column).Execute) { Hint = "Refresh" });
            commands.Add(new IconifiedCommand(new ClearColumnCommandHelper(column).Execute) { Hint = "Clear Column" });
            return commands;
        }

        public string InitializeHeader(Dictionary<string, object> parameters)
        {
            return "Conversation";
        }
    }
}
