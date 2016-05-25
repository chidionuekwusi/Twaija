using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.Practices.Composite.Events;
using TwaijaComposite.Modules.Common.Preferencing;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.Common.Events;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common.ViewModels;
using System.Windows.Input;
// Microsoft.Practices.Composite.Presentation.Commands;
using System.Threading;
using TwaijaComposite.Modules.Common.Services;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Commands;
using TwaijaComposite.Modules.Common;
#if SILVERLIGHT
using GalaSoft.MvvmLight.Command;
#endif

namespace TwaijaComposite.Modules.Clipboard.Viewmodels
{
    public class ClipboardViewmodel:ViewModelBase,IClipboardViewmodel
    {
        [Dependency]
        public IDispatcher Dispatcher { get; set; }
        private readonly IEventAggregator m_EventAggr;
        private readonly Preferences pref;
        private readonly IPictureServicesRepository picRepository;
        private readonly IPostMessageServiceRepository postRepository;
        public ClipboardViewmodel(IEventAggregator aggr, Preferences pref,IPictureServicesRepository services,IPostMessageServiceRepository postservices,IPictureTray tray)
        {
            picRepository = services;
            postRepository = postservices;
            m_EventAggr = aggr;
            this.pref = pref;
            aggr.GetEvent<CopyToClipboardEvent>().Subscribe(CopyToClipboard);
            aggr.GetEvent<SetClipboardOperationEvent>().Subscribe(SetCurrentOperation);
            _tray = tray;
            State = new IdleState();
        }
        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                
                _text = value;
                Dispatcher.Invoke((o) =>
                {
                    OnPropertyChanged("Text");
                });
            }
        }
        private string _statusMessage;
        public string StatusMessage
        {
            get { return _statusMessage; }
            set
            {
                _statusMessage = value;
                Dispatcher.Invoke(new SendOrPostCallback((s) =>
                {
                    OnPropertyChanged("StatusMessage");
                }));
            }
        }
        private DelegateCommand<object> _postCommand;
        public ICommand PostCommand
        {
            get
            {
                if (_postCommand == null)
                {
                    _postCommand = new DelegateCommand<object>((u) =>
                    { PostMessage(); });
                }
                return _postCommand;
            }
        }
         void CopyToClipboard(string message)
        {
            State.CopyToClipboard(this, message);
        }

        public void PostMessage()
        {
            ThreadPool.QueueUserWorkItem((state) =>
            {
                State.PostMessage(this);
                RefreshCancelButton();
            }, null);
        }
        void RefreshCancelButton()
        {
            Dispatcher.Invoke((o) =>
            {
                CancelOperationCommand.RaiseCanExecuteChanged();
            });
        }
        public IClipboardState State
        {
            get;
            set;
        }
        private IPictureTray _tray;
        public IPictureTray PictureTray
        {
            get { return _tray; }
        }

        public IUser CurrentUser
        {
            get { return pref.TransparentUsersFacade.Userrepository.SelectedUser; }
        }

        public Common.Interfaces.IPictureService GetPictureService(string Key)
        {
            if(picRepository.ServiceExists(Key))
            return picRepository.GetService(Key);

            return null;
        }

        public IPostMessageService GetPostMesssageService(string Key)
        {
            if(postRepository.ServiceExists(Key))
            return postRepository.GetService(Key);

            return null;
        }


        public void SetCurrentOperation(IClipboardOperation operation)
        {
            State.SetCurrentOperation(this, operation);
        }
        private IClipboardOperation _currentOperation;

        public IClipboardOperation CurrentOperation
        {
            get { return _currentOperation; }
            set { _currentOperation = value;
                CancelOperationCommand.RaiseCanExecuteChanged(); }
        }
        

        public void CancelCurrentOperation()
        {
            State.CancelCurrentOperation(this);
        }
#if SILVERLIGHT
        RelayCommand<object> cancel;
#else
        DelegateCommand<object> cancel;
#endif
#if SILVERLIGHT
        public RelayCommand<object> CancelOperationCommand
        {
            get
            {
                if (cancel == null)
                {
                    cancel = new RelayCommand<object>((s) => { State.CancelCurrentOperation(this); }, (u) => { return (CurrentOperation != null) ? true : false; });

                }
                return cancel;
            }
        }
#else
        public DelegateCommand<object> CancelOperationCommand
        {
            get
            {
                if (cancel == null)
                {
                    cancel = new DelegateCommand<object>((s) => { State.CancelCurrentOperation(this);}, (u) => { return (CurrentOperation != null) ? true : false; });
                }
                return cancel;
            }
        }
#endif

        string messagedeliverystatus=string.Empty;
        public string MessageDeliveryStatus
        {
            get { return messagedeliverystatus; }
            set
            {
                    messagedeliverystatus = value;
                    OnPropertyChanged("MessageDeliveryStatus");
            }
          
        }
    }
}
