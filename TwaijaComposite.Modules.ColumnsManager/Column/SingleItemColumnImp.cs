using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.Services;
using TwaijaComposite.Modules.Common;
using System.ComponentModel;
using Microsoft.Practices.Unity;

namespace TwaijaComposite.Modules.ColumnsManager
{
    public class SingleItemColumnImp:IColumn,INotifyPropertyChanged
    {
        public SingleItemColumnImp()
        {

        }
        IDispatcher dispatcher;
        [InjectionConstructor]
        public SingleItemColumnImp(IDispatcher dispatcher)
        {
            RecieveNewMessages = new NewMessageHandler(RecieveSingleMessageMethod);
            RecieveFilterMessages = new NewMessageHandler(RecieveFilterMessageMethod);
            this.dispatcher = dispatcher;
        }
        public void RecieveFilterMessageMethod(IMessage message, ColumnDirective directive)
        {

        }
        protected void RecieveSingleMessageMethod(IMessage message,ColumnDirective directive)
        {
            ClearContent();
            List<object> buffer = new List<object>();
            message.AddToCollection(buffer);
            dispatcher.Invoke((s) =>
            {
                Content = s as System.Collections.IEnumerable;
            }, buffer);      
        }
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public IFilter Filter
        {
            get;
            set;
        }

        public IRequest Request
        {
            get;
            set;
        }

        public NewMessageHandler RecieveNewMessages
        {
            get;
            set;
        }

        public void Initialize()
        {
            Request.Start();
        }


        public IList<System.Windows.Input.ICommand> Commands
        {
            get;
            set;
        }

        public string Header
        {
            get;
            set;
        }

        public Uri Icon
        {
            get;
            set;
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public event EventHandler Close;

        public void Dispose()
        {
            if (_content.Count > 0)
            {
                var disposable = _content[0] as IDisposable;
                _content.Clear();
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
        }

        public string IdentityString
        {
            get;
            set;
        }



        private List<object> _content=new List<object>();
        public System.Collections.IEnumerable Content
        {
            get { return _content; }
            set
            {
                _content = value as List<object>;
                OnPropertyChanged("Content");
            }
        }

        public void AddContent(object content)
        {
            //throw new NotImplementedException();
        }

        public void RemoveContent(object key)
        {
            //throw new NotImplementedException();
        }

        public void InsertContent(object content)
        {
           // throw new NotImplementedException();
        }

        public void ClearContent()
        {
            Dispose();
            //Content = new List<object>();
        }


        public NewMessageHandler RecieveFilterMessages
        {
            get;
            set;
        }

        public Common.DataInterfaces.IUser Owner
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
