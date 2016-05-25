using System;
using TwaijaComposite.Modules.Clipboard.Viewmodels;

namespace TwaijaComposite.Modules.Clipboard
{
   public class PostingMessageState:IClipboardState
    {
        public void CopyToClipboard(IClipboardViewmodel model, string message)
        {
            //Do nothing because The Clipboard is busy.
            model.MessageDeliveryStatus = "Currently posting a message....";
        }

        public void PostMessage(IClipboardViewmodel model)
        {
            //Do nothing because the Clipboard is busy
            model.MessageDeliveryStatus = "Currently posting a message....";
        }

        public void SetCurrentOperation(IClipboardViewmodel model, IClipboardOperation operation)
        {
            model.MessageDeliveryStatus = "The Clipboard is busy..";
        }


        public void CancelCurrentOperation(IClipboardViewmodel model)
        {
            model.MessageDeliveryStatus = "The Clipboard is busy..";
        }
    }
}
