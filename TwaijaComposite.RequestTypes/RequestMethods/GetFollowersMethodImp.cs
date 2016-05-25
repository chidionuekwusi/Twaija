using System;
using System.Collections.Generic;
using TwaijaComposite.Modules.Common.Interfaces;
using Twitterizer;
using TwaijaComposite.Modules.Common;

namespace TwaijaComposite.RequestAdapterModule
{
    public class RetrieveFollowers_Or_Friends_Method :TwitterMethodBase, IRetrieveFollowers_Or_Friends_Method
    {
        private ITwitterrizerMethodConsole console;
        public RetrieveFollowers_Or_Friends_Method(ITwitterrizerMethodConsole console)
        {
            this.console = console;
        }

        private UserIdCollection currentUsers=new UserIdCollection();
        private UserIdCollection buffer = new UserIdCollection();
        long LastIdInCollection;
        int _currentIndex = 0;
        bool NewIdLookUpRequired = true;
        public string ScreenName
        {
            get;
            set;
        }
        public decimal UserId
        {
            get;
            set;
        }
        IList<Modules.Common.DataInterfaces.ITwitterProfile_Small> Convert(TwitterUserCollection collection)
        {
            List<Modules.Common.DataInterfaces.ITwitterProfile_Small> users = new List<Modules.Common.DataInterfaces.ITwitterProfile_Small>();
            foreach (Twitterizer.TwitterUser user in collection)
            {
                try
                {
                    users.Add(new TwitterSmallProfileAdapter(user));
                }
                catch
                {

                }
            }
            return users;
        }
        void ReEvaluateUserIds()
        {
            if (NewIdLookUpRequired)
            {
                RetrieveIds();
                NewIdLookUpRequired = false;
            }
        }
        TwitterUserCollection  Lookup(Navigation direction)
        {
            ReEvaluateUserIds();
            int total = 0;
            int difference = 0;
            int index = 0;
            int per = 100;
            switch (direction)
            {
                case Navigation.Forward:
                    index = _currentIndex;
                    difference = (currentUsers.Count) - _currentIndex;
                    total = (difference < 100) ? difference : 100;
                    break;
                case Navigation.Back:
            //Does not support traversing backwards for now.       
                    break;
                case Navigation.None:
                    index = 0;
                    total = (currentUsers.Count - 100) < 0 ? (currentUsers.Count) :  100;
                    break;

            }       
            for (int indexer=index; (indexer<(total+index)); indexer++)
            {
                buffer.Add(currentUsers[indexer]);

            }
            var response = console.GetUsers(Token, new LookupUsersOptions() { UserIds = buffer }).ResponseObject;
            if (response != null)
            {
                switch (direction)
                {
                    case Navigation.Forward:
                        _currentIndex += total;
                        //This means its reached the end of the currentUserIds list
                        if (total == difference)
                        {
                            NewIdLookUpRequired = true;
                        }
                        break;
                    case Navigation.Back:
                      //  _currentIndex = total;
                        break;
                    case Navigation.None:
                        _currentIndex = total;
                        break;

                }

                buffer.Clear();
            }
           return response;
        }
        private void RetrieveIds()
        {
            UserIdCollection _buffercurrentUsers = null;
            if (currentUsers.Count == 0)
            {
                switch (Relationship)
                {
                    case Modules.Common.TwitterRelationship.Followers:
                        _buffercurrentUsers =console.GetFollowersIds(Token, new UsersIdsOptions() { ScreenName = ScreenName }).ResponseObject;
                        break;
                    case Modules.Common.TwitterRelationship.Friends:
                        _buffercurrentUsers = console.GetFriendsIds(Token, new UsersIdsOptions() { ScreenName = ScreenName }).ResponseObject;
                        break;
                }
            }
            else
            {
                switch (Relationship)
                {
                    case Modules.Common.TwitterRelationship.Followers:
                        _buffercurrentUsers = console.GetFollowersIds(Token, new UsersIdsOptions() { ScreenName = ScreenName, Cursor = LastIdInCollection }).ResponseObject;
                        break;
                    case Modules.Common.TwitterRelationship.Friends:
                        _buffercurrentUsers = console.GetFriendsIds(Token, new UsersIdsOptions() { ScreenName = ScreenName, Cursor = LastIdInCollection }).ResponseObject;
                        break;
                }
              
            }
            if (_buffercurrentUsers != null)
            {
                currentUsers = _buffercurrentUsers;
                LastIdInCollection = currentUsers.NextCursor;
                buffer.Clear();
            }

        }
        public IList<Modules.Common.DataInterfaces.ITwitterProfile_Small> Create(Modules.Common.Navigation direction)
        {
            IList<Modules.Common.DataInterfaces.ITwitterProfile_Small> response = null;
            try
            {
                response = Convert(Lookup(direction));
            }
            catch (Exception)
            {

            }
            return response;
        }

        public Modules.Common.TwitterRelationship Relationship
        {
            get;
            set;
        }
    }
}
