
using TwaijaComposite.Modules.Common.Interfaces;
using System.Collections.Generic;
using System;
namespace TwaijaComposite.Modules.Common.DataInterfaces
{
    public interface IUser
    {
        Uri DisplayImage { get; set; }
        string ScreenName { get; set; }
        string DefaultPostalServiceKey {get;}
        string DefaultPictureServiceKey { get; }
        IList<string> PossiblePostalOperations { get; }
        IUserMemento CreateUserMemento();
    }
    public interface IUserMemento
    {
        IUser CreateUser();
    }
}
