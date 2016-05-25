using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.DataInterfaces;

namespace TwaijaComposite.Modules.Common.Interfaces
{
    public interface IPictureService
    {
        event MessageSentHandler MessageSent;
        event MessageFailedHandler MessageFailed;
        bool PostPicture(Uri pictureLocation,out string outputpicturelocation);
        bool PostMessageAndPicture(IUser user,Uri PictureLocation, string message);
        string Name { get; }
        Uri ThumbnailImage { get; }
    }
}
