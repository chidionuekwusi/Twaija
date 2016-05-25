using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.ColumnsManager.Viewmodels;
using TwaijaComposite.Modules.ColumnsManager.Request;
using TwaijaComposite.Modules.ColumnsManager.Filter;
using TwaijaComposite.Modules.Common.Resources;
using System.Windows.Input;
using TwaijaComposite.Modules.Common.Commands;
using TwaijaComposite.Modules.ColumnsManager.Column.ColumnCommands;

namespace TwaijaComposite.Modules.ColumnsManager.Column.Factories
{
    public class UserSearchFactory:FactoryBase,IColumnPartsFactory
    {
        public string FactoryName
        {
            get { return ColumnTypeKeys.TwitterUserSearchKey; }
        }

        public IRequest CreateRequest(Common.DataInterfaces.IUser user, Dictionary<string, object> parameters)
        {
            IModelFactory<TwitterProfile_SmallViewmodel> factory = null;
            var helper = TwitterFactoryHelper.Create();
            var request=helper.CreateAndConfigureRequest<TwitterProfile_SmallViewmodel, UserSearchRequest>(user, parameters, m_Container, out factory);
            request.TargerScreenName = parameters[CreateColumnEventParameters.TargetScreenNameKey].ToString();
            if (factory != null)
            {
                request.Converter.SetFactory<TwitterProfile_SmallViewmodel>(factory);
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
                filter.Criterion.Add(new FollowersCriteria());
                filter.Criterion.Add(new FriendsCriteria());
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
            return "Find:"+ parameters[CreateColumnEventParameters.TargetScreenNameKey];
        }
    }
}
