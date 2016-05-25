using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.ColumnsManager.Viewmodels;
using TwaijaComposite.Modules.ColumnsManager.Request;
using TwaijaComposite.Modules.Common.Resources;
using System.Windows.Input;
using TwaijaComposite.Modules.Common.Commands;
using TwaijaComposite.Modules.ColumnsManager.Column.ColumnCommands;

namespace TwaijaComposite.Modules.ColumnsManager.Column.Factories
{
    public class TwitterProfileFactory:FactoryBase,IColumnPartsFactory
    {
        public string FactoryName
        {
            get { return ColumnTypeKeys.TwitterProfileKey; }
        }

        public IRequest CreateRequest(Common.DataInterfaces.IUser user, Dictionary<string, object> parameters)
        {
            IModelFactory<TwitterProfile_LargeViewmodel> factory = null;
            var helper = TwitterFactoryHelper.Create();
            var request= helper.CreateAndConfigureRequest<TwitterProfile_LargeViewmodel, TwitterProfile_LargeRequest>(user, parameters, m_Container, out factory);
           request.ScreenName = parameters[CreateColumnEventParameters.TargetScreenNameKey].ToString();
           if (factory != null)
           {
               request.Converter.SetFactory<TwitterProfile_LargeViewmodel>(factory);
           }
           return request;
        }

        public IFilter CreateFilter(Common.DataInterfaces.IUser user)
        {
            return null;
        }

        public IList<System.Windows.Input.ICommand> CreateCommandList(IColumn column)
        {
            List<ICommand> commands = new List<ICommand>();
            commands.Add(new IconifiedCommand(new RefreshColumnCommandHelper(column).Execute) { Hint="Refresh",PathIcon=IconLocator.Refresh});
            return commands;
       
        }

        public string InitializeHeader(Dictionary<string, object> parameters)
        {
            return "@" + parameters[CreateColumnEventParameters.TargetScreenNameKey] + "'s Profile";
        }
    }
}
