using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.DataInterfaces;

namespace TwaijaComposite.Modules.Common.Interfaces.Commands
{
    public interface IFollowCommand
    {
        bool Follow(string ScreenName, ITwitterUser user);
    }
    public interface IUnFollowCommand
    {
        bool UnFollow(string ScreenName, ITwitterUser user);
    }
    public interface IFavouriteCommand
    {
        bool Favourite(decimal Id, ITwitterUser user);
    }
    public interface IUnFavouriteCommand
    {
        bool UnFavourite(decimal Id, ITwitterUser user);
    }
    public interface IBlockCommand
    {
        bool Block(string ScreenName, ITwitterUser user);
    }
    public interface IUnBlockCommand
    {
        bool UnBlock(string ScreenName, ITwitterUser user);
    }
    public interface IDeleteTweetCommand
    {
        bool DeleteTweet(decimal Id, ITwitterUser user);
    }
    public interface IRetweetCommand
    {
        bool Retweet(decimal Id, ITwitterUser user);
    }
}
