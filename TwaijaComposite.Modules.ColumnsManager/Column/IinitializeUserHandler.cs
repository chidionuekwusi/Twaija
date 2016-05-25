using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.Common.Commands;
using TwaijaComposite.Modules.Common;

namespace TwaijaComposite.Modules.ColumnsManager.Column
{
    public interface IinitializeUserHandler : IContainsObjectRepositoryKey<Type>
    {
        void Initialize(IUser user,IColumnController manager);
    }
    public class InitializeUserHandlerRepository : ObjectRepository<IinitializeUserHandler, Type>
    {
        public InitializeUserHandlerRepository(IEnumerable<IinitializeUserHandler> list):base(list)
        {
        }
    }
    public class TwitterUserInitializeHandler : IinitializeUserHandler
    {
        public void Initialize(IUser user,IColumnController manager)
        {
            var tl = new CreateHomeTimelineCommandHelper() { ScreenName = user.ScreenName }.SetupArguments();
            var mentions = new CreateMentionsCommandHelper() { ScreenName = user.ScreenName }.SetupArguments();
            var dms = new CreateDirectMessagesCommandHelper() { ScreenName = user.ScreenName }.SetupArguments();
            tl.User = user;
            mentions.User = user;
            dms.User = user;
            manager.HandleCreateColumnEvent(tl);
            manager.HandleCreateColumnEvent(mentions);
            manager.HandleCreateColumnEvent(dms);
        }

        public Type Key
        {
            get { return typeof(TwitterUser); }
        }
    }
}
