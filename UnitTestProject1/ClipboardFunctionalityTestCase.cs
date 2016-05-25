using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TwaijaComposite.Modules.Clipboard.Viewmodels;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Clipboard;
using System.Collections.Generic;
using TwaijaComposite.RequestAdapterModule;
using TwaijaComposite.Modules.Common.AttributeTypes;

namespace TwaijaUnitTestCases
{
    [InterfaceMethodImplemented(MethodName = "PostMessageAndPicture")]
    public class PictureServiceMock:IPictureService,IClipboardOperationAware
    {
        public bool PostMessageAndPictureReturnValue { get; set; }
        public event MessageSentHandler MessageSent;

        public event MessageFailedHandler MessageFailed;

        public virtual bool PostPicture(Uri pictureLocation, out string outputpicturelocation)
        {
            throw new NotImplementedException();
        }

        public virtual bool PostMessageAndPicture(IUser user, Uri PictureLocation, string message)
        {
            if (Operation != null)
            {
                Operation.Processed = true;
            }
            return PostMessageAndPictureReturnValue;
        }

        public string Name
        {
            get { return "Mock Service"; }
        }

        public Uri ThumbnailImage
        {
            get { return new Uri("http://www.google.com"); }
        }

        public IClipboardOperation Operation
        {
            get;
            set;
        }
    }
    [TestClass]
    public class ClipboardFunctionalityTestCase
    {
        /// <summary>
        /// Creates a Clipboard with an empty PictureTray
        /// </summary>
        /// <returns></returns>
        Mock<IClipboardViewmodel> CreateClipboard()
        {
            List<string> possibleOperations=new List<string>();
            Mock<IClipboardOperation> operation = new Mock<IClipboardOperation>();
            Mock<IClipboardViewmodel> clipboard = new Mock<IClipboardViewmodel>();
            Mock<IPostMessageService> postmessageservice = new Mock<IPostMessageService>();
            PictureTrayImp pictray = new PictureTrayImp();
            pictray.EmptyTray();
            Mock<IUser> user = new Mock<IUser>();
            user.Setup(p => p.PossiblePostalOperations).Returns(possibleOperations);
            clipboard.SetupAllProperties();
            user.Setup(p => p.DefaultPostalServiceKey).Returns("TwitterKey");
            postmessageservice.Setup(p => p.PostMessage(user.Object, It.IsAny<string>())).Returns(true).Verifiable();
            clipboard.Setup(p => p.CurrentUser).Returns(user.Object);
            clipboard.Setup(p => p.PictureTray).Returns(pictray);
            return clipboard;
        }
        [TestMethod]
        public void PostBasicMessageTest()
        {

            //arrange
            var clipboard = CreateClipboard();
            Mock<IPostMessageService> postmessageservice = new Mock<IPostMessageService>();
            string message="young blaad";
            string deliverymessage = string.Empty;
            clipboard.Object.Text = message;
            postmessageservice.Setup(p => p.PostMessage(clipboard.Object.CurrentUser, It.IsAny<string>())).Returns(true).Verifiable();
            clipboard.Setup(p => p.GetPostMesssageService(clipboard.Object.CurrentUser.DefaultPostalServiceKey)).Returns(postmessageservice.Object).Verifiable();
            clipboard.Setup(p => p.CurrentOperation).Returns<IClipboardOperation>(null);

            //act
            IdleState state = new IdleState();
            state.PostMessage(clipboard.Object);

            //assert
            postmessageservice.Verify(p=>p.PostMessage(clipboard.Object.CurrentUser,message),Times.Once());
            Assert.AreEqual("Message Sent", clipboard.Object.MessageDeliveryStatus);
            Assert.AreEqual(string.Empty, clipboard.Object.Text);
            Assert.AreSame (typeof(IdleState), clipboard.Object.State.GetType());
            Assert.AreEqual(string.Empty, clipboard.Object.StatusMessage);
        }

        [TestMethod]
        public void CanSetClipboardOperation()
        {
            //arrange
            var clipboard = CreateClipboard();
            clipboard.Object.CurrentUser.PossiblePostalOperations.Add("RetweetKey");
            Mock<IClipboardOperation> clipboardoperation=new Mock<IClipboardOperation>();
            clipboardoperation.Setup(p => p.OperationKey).Returns("RetweetKey");

            //act
            IdleState state = new IdleState();
            state.SetCurrentOperation(clipboard.Object, clipboardoperation.Object);
           

            //assert
            Assert.AreSame(clipboardoperation.Object, clipboard.Object.CurrentOperation);
        }
        [TestMethod]
        public void CanCancelClipboardOperation()
        {
            //arrange
            var clipboard = CreateClipboard();
            clipboard.Object.CurrentUser.PossiblePostalOperations.Add("RetweetKey");
            Mock<IClipboardOperation> clipboardoperation = new Mock<IClipboardOperation>();
            clipboardoperation.Setup(p => p.OperationKey).Returns("RetweetKey");

            //act
            IdleState state = new IdleState();
            state.SetCurrentOperation(clipboard.Object, clipboardoperation.Object);
            

            //assert
            Assert.AreSame(clipboardoperation.Object, clipboard.Object.CurrentOperation);


            state.CancelCurrentOperation(clipboard.Object);

            Assert.IsNull(clipboard.Object.CurrentOperation);
        }
        [TestMethod]
        public void PostPictureAndMessageTest()
        {
            //arrange
            var clipboard = CreateClipboard();
            var service = new PictureServiceMock();
            service.PostMessageAndPictureReturnValue = true;
            clipboard.Setup(p => p.GetPictureService(It.IsAny<string>())).Returns(service).Verifiable();
            clipboard.Object.PictureTray.Picture = new Uri("http://www.google.com");
            clipboard.Object.Text = "Post this picture for me";

            //act
            IdleState state = new IdleState();
            state.PostMessage(clipboard.Object);

            //Assert
            clipboard.Verify();
            Assert.IsTrue(clipboard.Object.PictureTray.IsEmpty);
            Assert.IsNull(clipboard.Object.PictureTray.Picture);
            Assert.AreEqual(string.Empty, clipboard.Object.Text);
            Assert.AreEqual("Message Sent", clipboard.Object.MessageDeliveryStatus);
            Assert.AreSame (typeof(IdleState), clipboard.Object.State.GetType());
            Assert.AreEqual(string.Empty, clipboard.Object.StatusMessage);

        }
        [TestMethod]
        public void PostPictureAndMessageWithClipboardOperationAwarePictureService()
        {
            //arrange
            var clipboard = CreateClipboard();
            var service = new PictureServiceMock();
            Mock<IClipboardOperation> ReplyOperation = new Mock<IClipboardOperation>();
            ReplyOperation.SetupAllProperties();
            service.PostMessageAndPictureReturnValue = true;
            clipboard.Setup(p => p.GetPictureService(It.IsAny<string>())).Returns(service).Verifiable();
            clipboard.Object.PictureTray.Picture = new Uri("http://www.google.com");
            clipboard.Object.Text = "Post this picture for me";
            clipboard.Setup(p=>p.CurrentOperation).Returns(ReplyOperation.Object);
            //act
            IdleState state = new IdleState();
            clipboard.Object.State = state;
            clipboard.Object.SetCurrentOperation(ReplyOperation.Object);
            state.PostMessage(clipboard.Object);

            //Assert
            clipboard.Verify();
            Assert.IsTrue(clipboard.Object.PictureTray.IsEmpty);
            Assert.IsTrue(clipboard.Object.CurrentOperation.Processed);
            Assert.AreEqual(string.Empty, clipboard.Object.Text);
            Assert.AreEqual("Message Sent", clipboard.Object.MessageDeliveryStatus);
            Assert.AreSame(typeof(IdleState), clipboard.Object.State.GetType());
            Assert.AreEqual(string.Empty, clipboard.Object.StatusMessage);


        }

            
    }
}
