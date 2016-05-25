using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
//using Microsoft.Practices.Composite.Events;
using TwaijaComposite.Modules.Common.Events;
using TwaijaComposite.Modules.Common;
using Microsoft.Practices.Prism.Events;

namespace TwaijaComposite.Modules.Common.Commands
{
    public class CreateColumnCommandHelperBase:IHelper<CreateColumnEventArgs>
    {
        protected string ColumnType { get; set; }
        public string BuilderType{get;set;}
        public string ColumnImpType { get; set; }
        public string CustomModelFactoryKey { get; set; }
        protected virtual Dictionary<string, object> PrepareOptions()
        {
           var dic= new Dictionary<string, object>();
           if (!string.IsNullOrEmpty(CustomModelFactoryKey))
           {
               dic.Add(TwaijaComposite.Modules.Common.Resources.CreateColumnEventParameters.ModelFactoryKey, CustomModelFactoryKey);
           }
           return dic;
        }
        public virtual CreateColumnEventArgs  SetupArguments()
        {
            return PrepareArguments(PrepareOptions());
        }
        protected virtual CreateColumnEventArgs PrepareArguments(Dictionary<string,object> dictionary)
        {
            var args = new CreateColumnEventArgs() { Parameters = dictionary , ColumnType=ColumnType,ColumnBuildType=BuilderType,ColumnImpType=ColumnImpType};
            return args;
        }
    }

    public class SetTargetScreenNameCommandHelperBase:CreateColumnCommandHelperBase
    {
        public string ScreenName { get; set; }
        protected override Dictionary<string, object> PrepareOptions()
        {
            var dic = base.PrepareOptions();
            dic.Add(TwaijaComposite.Modules.Common.Resources.CreateColumnEventParameters.TargetScreenNameKey, ScreenName);
            return dic;
        }
    }
    public class CreateColumnCommandExecutor : ICommandExecutor<CreateColumnEventArgs>
    {
        [Dependency]
        public IEventAggregator aggr { get; set; }
        public IHelper<CreateColumnEventArgs> Helper
        {
            get;
            set;
        }
        private void ExecuteCommand(object state)
        {
            aggr.GetEvent<CreateColumnEvent>().Publish(Helper.SetupArguments());
        }
        public Action<object> Execute
        {
            get { return ExecuteCommand; }
        }
    }
    public class ProfileCommandExecutor : ICommandExecutor<OpenProfileEventArgs>
    {
        [Dependency]
        public IEventAggregator aggr { get; set; }
        protected void ExecuteCommand(object state)
        {
            aggr.GetEvent<OpenProfileEvent>().Publish(Helper.SetupArguments());
        }
        public Action<object> Execute
        {
            get { return ExecuteCommand ; }
        }

        public IHelper<OpenProfileEventArgs> Helper
        {
            get;
            set;
        }
    }
}
