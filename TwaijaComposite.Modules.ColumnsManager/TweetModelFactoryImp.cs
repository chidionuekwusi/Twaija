using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.Common.Commands;
using TwaijaComposite.Modules.Common.Commands.Factories;
using TwaijaComposite.Modules.Common.Preferencing;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.ColumnsManager.Viewmodels;

namespace TwaijaComposite.Modules.ColumnsManager
{
    public class ProfileUserTimelineTweetViewmodelFactory : IModelFactory<TweetViewmodel>
    {
        [Dependency]
        public Preferences pref { get; set; }
        [Dependency("IconifiedNetworkCommandFactory")]
        public INetworkAndMiscCommandsFactory netCommandsFactory { get; set; }
        [Dependency("IconifiedCommandFactory")]
        public ICommandFactory columnCommandFactory { get; set; }
        #region IModelFactory<TweetViewmodel> Members

        public TweetViewmodel Create(object optionalparameter)
        {
              var tweetmodel=new  TweetViewmodel();
            //Configure
              if (optionalparameter != null)
              {
                  try
                  {
                      var tweet = optionalparameter as TwaijaComposite.Modules.Common.ITweet;
                      tweetmodel.Commands.Add(netCommandsFactory.CreateReplyCommand(tweet.ScreenName, tweet.Id));
                      tweetmodel.Commands.Add(netCommandsFactory.CreateRetweetCommand(tweet.Id,tweet.ScreenName+": "+ tweet.Text));
                      tweetmodel.Commands.Add(netCommandsFactory.CreateFavouriteCommand(tweet.Id, tweet.Text));
                      tweetmodel.Commands.Add(netCommandsFactory.CreateUnFavouriteCommand(tweet.Id, tweet.Text));
                      if (tweet.InReplyToStatusId.HasValue && tweet.InReplyToStatusId != default(decimal))
                      {
                          tweetmodel.Commands.Add(columnCommandFactory.CreateConversationCommand(null,tweet.Id));
                      }
                      if (pref.TransparentUsersFacade.Userrepository.Find<IUser>(tweet.ScreenName) != null)
                      {
                          tweetmodel.Commands.Add(netCommandsFactory.CreateDeleteTweetCommand(tweet.Id));
                      }
                  }
                  catch
                  {

                  }
              }
              return tweetmodel;
        }

        #endregion
    }
    /*This class will be responsible for creating and composing 
     the datamodels that will be added to the IColumns,It will use parts 
     resolved from the Dependency container to compose these models*/
   public class TweetModelFactoryImp:IModelFactory<TweetViewmodel>   
    {
       [Dependency]
       public Preferences pref { get; set; }
       [Dependency("IconifiedNetworkCommandFactory")]
       public INetworkAndMiscCommandsFactory netCommandsFactory { get; set; }
       [Dependency("IconifiedCommandFactory")]
       public ICommandFactory columnCommandFactory { get; set; }
        public TweetViewmodel Create(object optionalparameter)
        {
            var tweetmodel=new  TweetViewmodel();
            //Configure
            if (optionalparameter != null)
            {
                try
                {
                    var tweet = optionalparameter as TwaijaComposite.Modules.Common.ITweet;
                 tweetmodel.Commands.Add(columnCommandFactory.CreateOpenTwitterProfileCommand(tweet.ScreenName));
                 tweetmodel.Commands.Add(columnCommandFactory.CreateTweetSearchCommand(tweet.ScreenName));
                 tweetmodel.Commands.Add(netCommandsFactory.CreateFollowCommand(tweet.ScreenName));
                 tweetmodel.Commands.Add(netCommandsFactory.CreateUnFollowCommand(tweet.ScreenName));
                 tweetmodel.Commands.Add(netCommandsFactory.CreateBlockCommand(tweet.ScreenName));
                 tweetmodel.Commands.Add(netCommandsFactory.CreateUnBlockCommand(tweet.ScreenName));
                 tweetmodel.Commands.Add(netCommandsFactory.CreateReplyCommand(tweet.ScreenName, tweet.Id));
                 tweetmodel.Commands.Add(netCommandsFactory.CreateRetweetCommand(tweet.Id, tweet.ScreenName + ": " + tweet.Text));
                 tweetmodel.Commands.Add(netCommandsFactory.CreateFavouriteCommand(tweet.Id, tweet.Text));
                 tweetmodel.Commands.Add(netCommandsFactory.CreateUnFavouriteCommand(tweet.Id, tweet.Text));
                 if (tweet.InReplyToStatusId.HasValue&&tweet.InReplyToStatusId!=default(decimal))
                 {
                     tweetmodel.Commands.Add(columnCommandFactory.CreateConversationCommand(null,tweet.Id));
                 }
                 if (pref.TransparentUsersFacade.Userrepository.Find<IUser>(tweet.ScreenName)!=null)
                 {
                     tweetmodel.Commands.Add(netCommandsFactory.CreateDeleteTweetCommand(tweet.Id)); 
                 }
                 tweetmodel.Commands.Add(netCommandsFactory.CreateDirectMessageReplyCommand(tweet.Id, tweet.ScreenName));
                }
                catch
                {

                }
            }
            
            return tweetmodel;
        }
    }
}
