using System;
//using Microsoft.Practices.Composite.Presentation.Commands;
using TwaijaComposite.Modules.Common.DataInterfaces;
using System.Collections.Generic;
using TwaijaComposite.Modules.Common.Interfaces;
using System.Windows.Input;
using TwaijaComposite.Modules.Common;

namespace TwaijaComposite.Modules.Clipboard.Viewmodels
{
    public interface IClipboardViewmodel
    {
        IPictureTray PictureTray { get; }
        IUser CurrentUser { get; }
        IClipboardState State { get; set; }
        string Text { get; set; }
        string StatusMessage { get; set; }
        string MessageDeliveryStatus { get; set; }
        void PostMessage();
        void SetCurrentOperation(IClipboardOperation operation);
        ICommand PostCommand { get; }
        IPictureService GetPictureService(string Key);
        IPostMessageService GetPostMesssageService(string Key);
        IClipboardOperation CurrentOperation { get; set; }
    }
    public interface IClipboardState
    {
        void CopyToClipboard(IClipboardViewmodel model,string message);
        void PostMessage(IClipboardViewmodel model);
        void SetCurrentOperation(IClipboardViewmodel model,IClipboardOperation operation);
        void CancelCurrentOperation(IClipboardViewmodel model);
    }
}
