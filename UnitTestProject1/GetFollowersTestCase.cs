using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TwaijaComposite.RequestAdapterModule;
using Twitterizer;
using TwaijaComposite.Modules.Common.DataInterfaces;
namespace TwaijaUnitTestCases
{
    /// <summary>
    /// Followers are meant to be retrieved 100 at a time , if the total number of followers/friends a User has is less than hundred
    /// then it should retrieve that amount of followers.
    /// </summary>
    [TestClass]
    public class GetFollowersTestCase
    {
        [TestMethod]
        public void FollowersUnder100Test()
        {
            
            
          
            //arrange
            UserIdCollection collection=new UserIdCollection();
            TwitterUserCollection users = new TwitterUserCollection();
            for(int u=0;u<175;u++)
            {
                collection.Add(u);
                users.Add(new TwitterUser() {ScreenName="Chidi's follower" });
            }
            Mock<ITwitterrizerMethodConsole> console = new Mock<ITwitterrizerMethodConsole>(MockBehavior.Loose);
            console.Setup(p => p.GetFollowersIds(It.IsAny<OAuthTokens>(), It.IsAny<UsersIdsOptions>())).Returns(new TwitterResponse<UserIdCollection>(){ ResponseObject=collection});
            console.Setup(p => p.GetUsers(It.IsAny<OAuthTokens>(), It.Is<LookupUsersOptions>(o => o.UserIds.Count == 100))).Returns(new TwitterResponse<TwitterUserCollection>() { ResponseObject = users }).Verifiable();
            console.Setup(p => p.GetUsers(It.IsAny<OAuthTokens>(), It.Is<LookupUsersOptions>(o => o.UserIds.Count == 75))).Returns(new TwitterResponse<TwitterUserCollection>() { ResponseObject = users }).Verifiable();
           
            Mock<ITwitterUser> user = new Mock<ITwitterUser>();
            user.SetupAllProperties();
            user.Object.Token = new TwaijaComposite.Modules.Common.TwitterToken();
            user.Object.Token.ConsumerKey = "qlemnmlakfnmlaknfm";
            user.Object.Token.ConsumerSecret = "safsafkjhkajgfjh";
            user.Object.Token.TokenKey = "sajbfbbehbfyyy";
            user.Object.Token.TokenSecret = "ealkjalnfkajlefhkjaeb#";
            user.Object.ScreenName = "Chidi Mock";
//act
            RetrieveFollowers_Or_Friends_Method method = new RetrieveFollowers_Or_Friends_Method(console.Object);
            method.Relationship = TwaijaComposite.Modules.Common.TwitterRelationship.Followers;
            method.User = user.Object;
            method.Create(TwaijaComposite.Modules.Common.Navigation.None);
            method.Create(TwaijaComposite.Modules.Common.Navigation.Forward);
            method.Create(TwaijaComposite.Modules.Common.Navigation.Back);

//assert
            //console.Verify(p => p.GetFollowers(It.IsAny<OAuthTokens>(),It.Is<LookupUsersOptions>(o => o.UserIds.Count == 100)), Times.Exactly(2));
            //console.Verify(p =>p.GetFollowers(It.IsAny<OAuthTokens>(), It.Is<LookupUsersOptions>(o => o.UserIds.Count == 50)), Times.Once());
            console.Verify();
           
        }
    }
}
