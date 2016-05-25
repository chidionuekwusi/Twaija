using System;
using System.Collections.Generic;
using System.Text;
using TwaijaComposite.Modules.Clipboard.Viewmodels;
using TwaijaComposite.Modules.Common.AttributeTypes;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.Common.Exceptions;
using TwaijaComposite.Modules.Common.Interfaces;

namespace TwaijaComposite.Modules.Clipboard
{
    public class IdleState:IClipboardState
    {
        public void CopyToClipboard(IClipboardViewmodel model, string message)
        {
            model.Text = message;
        }

        public virtual void PostMessage(IClipboardViewmodel model)
        {
            string messageFailed = "message failed";
            string wrongtype="Sorry, wrong user type for this operation";
            model.State = new PostingMessageState();
            try
            {
                var user = model.CurrentUser;
                if (!model.PictureTray.IsEmpty)
                {
                    var service = model.GetPictureService(user.DefaultPictureServiceKey);
                    if (model.CurrentOperation != null)
                    {
                        if (!model.CurrentOperation.Processed)
                        {
                            if (service is IClipboardOperationAware)
                            {
                                (service as IClipboardOperationAware).Operation = model.CurrentOperation;
                            }
                        }
                    }
                    var methodName = (service.GetType().GetCustomAttributes(typeof(InterfaceMethodImplemented), false)[0] as InterfaceMethodImplemented).MethodName;
                    switch (methodName)
                    {
                        case "PostPicture":
                            var message = model.Text;
                            string url = string.Empty;
                            model.StatusMessage = "Posting Picture to picture Service...";
                            if (service.PostPicture(model.PictureTray.Picture, out url))
                            {
                                message += " " + url;
                                model.StatusMessage = "Posting Message with embedded Url...";
                                if (model.GetPostMesssageService(user.DefaultPostalServiceKey).PostMessage(user, message))
                                {
                                    MessageSent(model);
                                    model.PictureTray.EmptyTray();
                                }
                                else
                                {
                                    model.MessageDeliveryStatus = messageFailed;
                                }
                            }
                            else
                            {
                                model.MessageDeliveryStatus = messageFailed;
                            }
                            break;
                        case "PostMessageAndPicture":
                            model.StatusMessage = "Sending Message and Picture...";
                            if (service.PostMessageAndPicture(model.CurrentUser, model.PictureTray.Picture, model.Text))
                            {
                                MessageSent(model);
                                model.PictureTray.EmptyTray();
                            }
                            else
                            {
                                model.MessageDeliveryStatus = messageFailed;
                            }
                            break;
                    }
                }
                else
                {

                    if (model.CurrentOperation != null)
                    {
                        if (!model.CurrentOperation.Processed)
                        {
                            IPostMessageService service = model.GetPostMesssageService(model.CurrentOperation.PostMessageServiceKey);
                            if (service != null)
                            {
                                if (service.PostMessage(user, model.Text, model.CurrentOperation.Parameter))
                                {
                                    MessageSent(model);
                                }
                                else
                                {
                                    model.MessageDeliveryStatus = messageFailed;

                                }
                            }
                            else
                            {
                                model.MessageDeliveryStatus = wrongtype;
                            }
                        }
                    }
                    else
                    {
                        if (model.GetPostMesssageService(user.DefaultPostalServiceKey).PostMessage(user, model.Text))
                        {
                            MessageSent(model);
                        }
                        else
                        {
                            model.MessageDeliveryStatus = messageFailed;
                        }
                    }
                }
            }
            catch (WrongUserTypeException e)
            {
                StringBuilder build = new StringBuilder(wrongtype);
                build.Append(",expected a ");
                build.Append(e.ExpectedType.Name);
                model.MessageDeliveryStatus = build.ToString();
            }
            catch (Exception)
            {
                model.MessageDeliveryStatus = messageFailed;
            }
            model.State = new IdleState();
           
        }
        void MessageSent(IClipboardViewmodel model)
        {
            model.MessageDeliveryStatus = "message sent";
            model.StatusMessage = string.Empty;
            model.Text = string.Empty;
            if (model.CurrentOperation != null)
            {
                model.CurrentOperation.Processed = true;
                model.CurrentOperation = null;
            }
            
        }
        public void SetCurrentOperation(IClipboardViewmodel model, IClipboardOperation operation)
        {
            if (operation != null)
            {
                if (model.CurrentUser.PossiblePostalOperations.Contains(operation.OperationKey))
                {
                    model.StatusMessage = operation.OperationDescription;
                    model.CurrentOperation = operation;
                }
                else
                {
                    model.StatusMessage = "The current user does not support that sort of operation";
                }
            }
        }


        public void CancelCurrentOperation(IClipboardViewmodel model)
        {
            model.StatusMessage = string.Empty;
            if (model.CurrentOperation != null)
            {
                model.CurrentOperation.Processed = true;
            }
            model.CurrentOperation=null;
        }
    }
}
