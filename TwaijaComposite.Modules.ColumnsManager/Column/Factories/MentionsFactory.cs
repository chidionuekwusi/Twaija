using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.ColumnsManager.Converter;
using TwaijaComposite.Modules.ColumnsManager.Request;
using TwaijaComposite.Modules.ColumnsManager.Filter;
using TwaijaComposite.Modules.ColumnsManager.Viewmodels;
using System.Windows.Input;
using TwaijaComposite.Modules.Common.Commands;
using TwaijaComposite.Modules.Common.Resources;
using TwaijaComposite.Modules.ColumnsManager.Column.ColumnCommands;

namespace TwaijaComposite.Modules.ColumnsManager.Column.Factories
{
    public class MentionsFactory:IColumnPartsFactory
    {
        private IUnityContainer m_Container;
        public MentionsFactory(IUnityContainer container)
        {
            m_Container = container;
        }

        public string FactoryName
        {
            get {return ColumnTypeKeys.TwitterMentionsKey; }
        }

        public IRequest CreateRequest(Common.DataInterfaces.IUser user, Dictionary<string, object> parameters)
        {
            IModelFactory<TweetViewmodel> selectedModelFactory = null;
            selectedModelFactory = (!parameters.ContainsKey(CreateColumnEventParameters.ModelFactoryKey)) ? null : m_Container.Resolve<IModelFactory<TweetViewmodel>>(parameters[CreateColumnEventParameters.ModelFactoryKey].ToString());
            var request = m_Container.Resolve<MentionsRequest>();
            request.ConfigureTwitterUser(user as ITwitterUser);
            if (selectedModelFactory != null)
            {
                request.Converter.SetFactory<TweetViewmodel>(selectedModelFactory);
            }
            return request;
        }

        public IFilter CreateFilter(Common.DataInterfaces.IUser user)
        {
            var filter = m_Container.Resolve<IFilter>();
            filter.Criterion.Add(new ScreenNameCriteria());
            filter.Criterion.Add(new TextCriteria());
            return filter;
        }

        public IList<System.Windows.Input.ICommand> CreateCommandList(IColumn column)
        {
            List<ICommand> commands = new List<ICommand>();
            commands.Add(new IconifiedCommand(new RefreshColumnCommandHelper(column).Execute) { Hint = "Refresh", PathIcon = IconLocator.Refresh });
            commands.Add(new IconifiedCommand(new ClearColumnCommandHelper(column).Execute) { Hint = "Delete Items", PathIcon = IconLocator.Delete });
            commands.Add(new IconifiedCommand(new FowardColumnCommandHelper(column).Execute) { Hint = "Foward", PathIcon=IconLocator.Foward });
            return commands;
        }

        public string InitializeHeader(Dictionary<string, object> parameters)
        {
            return parameters[CreateColumnEventParameters.TargetScreenNameKey] + "'s Mentions";
        }
    }
}
