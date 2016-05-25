using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.Interfaces;
using Twitterizer;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.Common;
namespace TwaijaComposite.RequestAdapterModule
{
    public class UserSearchMethodImp:TwitterMethodBase,IUserSearchMethod
    {
        int page = 0;
        public string TargetScreenName
        {
            get;
            set;
        }
        IList<ITwitterProfile_Small> Refresh(Navigation direction)
        {
            IList<ITwitterProfile_Small> users = null;   
            UserSearchOptions options = new UserSearchOptions() { NumberPerPage = 100, Page = page };
            switch (direction)
            {
                case Navigation.Back:
                    options.Page--;
                    break;
                case Navigation.Forward:
                    options.Page++;
                    break;
            }
            users = Convert(Twitterizer.TwitterUser.Search(Token, TargetScreenName, options).ResponseObject);
            if (users != null)
            {
                page++;
            }
            return users;

        }
        IList<ITwitterProfile_Small> Convert(TwitterUserCollection collection)
        {
            IList<ITwitterProfile_Small> users = new List<ITwitterProfile_Small>();
            foreach (Twitterizer.TwitterUser user in collection)
            {
                users.Add(new TwitterSmallProfileAdapter(user));
            }
            return users;
        }
        public IList<Modules.Common.DataInterfaces.ITwitterProfile_Small> Create(Modules.Common.Navigation direction)
        {
            IList<ITwitterProfile_Small> users = null;
            try
            {
                users = Refresh(direction);
                if (users.Count == 0)
                {
                    users = null;
                }
            }
            catch (Exception)
            {

            }
            return users;
        }
    }
}
