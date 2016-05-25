using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.Interfaces;
using System.IO;
using TwaijaComposite.Modules.Common.DataInterfaces;

namespace TwaijaComposite.RequestAdapterModule.Mock
{
    public class MockMethods : TwitterMethodBase, IHomeTimeline,IMentionsMethod, IUserTimlineMethod, IRetrieveTwitterProfile_LargeMethod, IRetrieveFollowers_Or_Friends_Method
    {
        public List<string> Text;
        int currentline = 0;
        public MockMethods()
        {
            Text=new List<string>();
            using (var text = File.OpenText("C:/Users/Chidi/Desktop/License.txt"))
            {
                string line = string.Empty;
                while (!string.IsNullOrEmpty(line = text.ReadLine()))
                {
                    if (!(line.Count() > 140))
                    {
                        Text.Add(line);
                    }
                    else
                    {
                        Text.Add(line.Substring(0, 139));
                    }
                }
            }
        }
        public IList<Modules.Common.ITweet> Create(Modules.Common.Navigation direction)
        {
            List<Modules.Common.ITweet> tweets = new List<Modules.Common.ITweet>(); 
            int x=currentline;
            for (int u = currentline; u < (x + 5); u++)
            {

                tweets.Add(new TweetAdapter() { Id=7286738274, ScreenName = "TheKillerBean", Name="slow dogg", Text = Text[u],
#if !SILVERLIGHT
                                                Thumbnail = new Uri("/TwaijaComposite.Modules.ColumnsManager;component/bird_48_blue.png",UriKind.Relative)
#else
                  
                                                Thumbnail = new Uri("/TwaijaComposite.Modules.ColumnManager.Silverlight;component/bird_48_blue.png",UriKind.Relative)
#endif
                });
                currentline++;
            }
            return tweets;
        }

        public string ScreenName
        {
            get;
            set;
        }

        public int NumberOfObjectsToRetrieve
        {
            get;
            set;
        }

        public string TargetScreenName
        {
            get;
            set;
        }

        Modules.Common.DataInterfaces.ITwitterProfile_Large IRequestMethod<Modules.Common.DataInterfaces.ITwitterProfile_Large>.Create(Modules.Common.Navigation direction)
        {
            //return null;
            return new TwitterProfile_LargeAdapter() {Location="Lagos,Nigeria", CreatedDate = DateTime.Now, IsFollowing = true, IsFollowedBy = false, Language = "en", NumberOfFollowers = 1000, NumberOfFriends = 1500, 
                
                ProfileImageLocation = "/TwaijaComposite.Modules.ColumnsManager;component/Views/427285_large_64da.jpg" ,Verified=true

                , ScreenName = this.TargetScreenName, Description = "This is a mock bio , but u know that already.", Website = "http://www.thestruggleisrealerthanucaneverknowtillyouhit25.com" };
        }


        public decimal UserId
        {
            get;
            set;
        }

        public Modules.Common.TwitterRelationship Relationship
        {
            get;
            set;
        }

        IList<Modules.Common.DataInterfaces.ITwitterProfile_Small> IRequestMethod<IList<Modules.Common.DataInterfaces.ITwitterProfile_Small>>.Create(Modules.Common.Navigation direction)
        {
            List<ITwitterProfile_Small> list = new List<ITwitterProfile_Small>();
            list.Add(new TwitterSmallProfileAdapter(new Twitterizer.TwitterUser() { CreatedDate = DateTime.Now, ScreenName = "DrugDealer", Description = "i have 100 followers and 6 friends", NumberOfFollowers = 100, NumberOfFriends=6 }));
            list.Add(new TwitterSmallProfileAdapter(new Twitterizer.TwitterUser() { CreatedDate = DateTime.Now, ScreenName = "MzIndependent", Description = "i have 201 followers and 180 friends", NumberOfFollowers = 201 ,NumberOfFriends=180}));
            list.Add(new TwitterSmallProfileAdapter(new Twitterizer.TwitterUser() { CreatedDate = DateTime.Now, ScreenName = "DrugDealer", Description = "i have 500 followers and 310 friends", NumberOfFriends = 310, NumberOfFollowers = 500 }));
            list.Add(new TwitterSmallProfileAdapter(new Twitterizer.TwitterUser() { CreatedDate = DateTime.Now, ScreenName = "MzIndependent", Description = "i have 500 followers and 245 friends", NumberOfFollowers = 500,NumberOfFriends=245 }));
            list.Add(new TwitterSmallProfileAdapter(new Twitterizer.TwitterUser(){ CreatedDate=DateTime.Now, ScreenName="DrugDealer", Description="i have 230 friends and 200 followers",NumberOfFriends=230,NumberOfFollowers=200}));
            list.Add(new TwitterSmallProfileAdapter(new Twitterizer.TwitterUser() { CreatedDate = DateTime.Now, ScreenName = "MzIndependent", Description = "i have 850 followers and 5 friends", NumberOfFollowers = 850, NumberOfFriends = 5 }));
            return list;
        }


        public void Reset()
        {
            //throw new NotImplementedException();
        }
    }
}
