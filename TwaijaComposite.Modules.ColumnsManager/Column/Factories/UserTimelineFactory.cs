using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.ColumnsManager.Request;
using TwaijaComposite.Modules.ColumnsManager.Filter;
using TwaijaComposite.Modules.ColumnsManager.Viewmodels;
using TwaijaComposite.Modules.Common.Resources;
using System.Windows.Input;
using TwaijaComposite.Modules.Common.Commands;
using TwaijaComposite.Modules.ColumnsManager.Column.ColumnCommands;

namespace TwaijaComposite.Modules.ColumnsManager.Column.Factories
{
   public class UserTimelineFactory:FactoryBase,IColumnPartsFactory
    {

        public string FactoryName
        {
            get { return ColumnTypeKeys.TwitterUserTimelineKey; }
        }

        public IRequest CreateRequest(Common.DataInterfaces.IUser user, Dictionary<string, object> parameters)
        {
            IModelFactory<TweetViewmodel> selectedFactory;
            var helper = TwitterFactoryHelper.Create();
            var request = helper.CreateAndConfigureRequest<TweetViewmodel, UserTimelineRequest>(user, parameters, m_Container, out selectedFactory);       
            request.ScreenName=(parameters[CreateColumnEventParameters.TargetScreenNameKey].ToString());
            if (selectedFactory != null)
            {
                request.Converter.SetFactory<TweetViewmodel>(selectedFactory);
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
            commands.Add(new IconifiedCommand(new FowardColumnCommandHelper(column).Execute){Hint="Foward",PathIcon=IconLocator.Foward});
            commands.Add(new IconifiedCommand(new BackwardColumnCommandHelper(column).Execute) { Hint = "Backward",PathIcon=IconLocator.Backward });
            return commands;
        }

        public string InitializeHeader(Dictionary<string, object> parameters)
        {
            return parameters[CreateColumnEventParameters.TargetScreenNameKey] + "'s Timeline";
        }
    }
}
