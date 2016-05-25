using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.Services;
using System.Threading;
//using Microsoft.Practices.Composite.Events;
using TwaijaComposite.Modules.ColumnsManager.Notifications;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.Common.Behaviors;
using System.Windows.Input;
//using Microsoft.Practices.Composite.Presentation.Commands;
using System.ComponentModel;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Commands;

namespace TwaijaComposite.Modules.ColumnsManager
{
    public class ProfileColumnImp : ColumnImp
    {
        [InjectionConstructor]
        public ProfileColumnImp(IDispatcher disp, IEventAggregator aggr):base(disp,aggr)
        {
        }
        protected override void PublishMessage(IMessage component)
        {
            
        }
    }
    public class ColumnImp:IColumn,INotifyOnHostEntry,INotifyPropertyChanged
    {
        bool _isMainView=true;
        public bool IsMainView
        {
            get { return _isMainView; }
            set
            {
                if (_isMainView != value)
                {
                    _isMainView = value;
                    OnPropertyChanged("IsMainView");
                }
            }
        }
        object selectedObject;
        public object SelectedObject
        {
            get { return selectedObject; }
            set
            {
                if (value != null)
                {
                    IsMainView = false;
                }
                else
                {
                    IsMainView = true;
                }
                selectedObject = value;
                OnPropertyChanged("SelectedObject");
            }
        }
        public DelegateCommand<object> NavigateToMainView
        {
            get
            {
                return new DelegateCommand<object>((s) => { SelectedObject = null; }, (r) => { return true; });
            }
        }
        private int _newMessageSize = 0;
        public int NewMessageSize
        {
            get { return _newMessageSize; }
            set { _newMessageSize = value;
            if (PropertyChanged != null)
            {
                Dispatcher.Invoke((s) => { PropertyChanged(this, new PropertyChangedEventArgs("NewMessageSize")); });
            }
            }
        }
        private bool initialized = false;
        IEventAggregator aggr;
        public ColumnImp()
        {

        }
        [InjectionConstructor]
        public ColumnImp(IDispatcher disp,IEventAggregator aggr)
        {
            Dispatcher = disp;
            RecieveNewMessages = new NewMessageHandler(RecieveNewMessagesMethod);
            this.aggr = aggr;
            _contentholder = _content;
        }
        private void NotifyNew(IMessage message)
        {
            Dispatcher.Invoke(new SendOrPostCallback((data) =>
            {
				
                var newitems = data as IMessage;
                var collection = new List<object>();
                newitems.AddToCollection(collection);
                foreach (object obj in collection)
                {
                    var notifyObject = obj as INotifyOnHostEntry;
                    if (notifyObject != null)
                    {
                      notifyObject.OnHostEntry.Trigger();
                    }
                }
            }), message);
        }
        public IFilter Filter
        {
            get;
            set;
        }
        public IDispatcher Dispatcher
        {
            get;
            set;
        }
        public IRequest Request
        {
            get;
            set;
        }
        protected virtual void PublishMessage(IMessage component)
        {
            var content = new List<object>();
            component.AddToCollection(content);
            if (content.Count != 0)
            {
                aggr.GetEvent<NewNotificationsEvent>().Publish(new Notice() { Content = content, Title = Header, NumberOfObjects = content.Count });
            }
        }
        protected void InputNewItems(IMessage message, ColumnDirective directive)
        {
            NewMessageSize = message.Size;
            switch (directive)
            {
                case ColumnDirective.Add:
                    Dispatcher.Invoke(new SendOrPostCallback((comp) =>
                    {
                        (comp as IMessage).AddToCollection(_content);
                    })
                        , message);
                    break;
                case ColumnDirective.Insert:
                    Dispatcher.Invoke(new SendOrPostCallback((comp) =>
                    {
                        (comp as IMessage).InsertIntoCollection(_content);
                    })
                         , message);
                    NotifyNew(message);
                    break;
                case ColumnDirective.AddAndClear:
                    Dispatcher.Invoke(new SendOrPostCallback((comp) =>
                    {
                        Content = _content;
                        Clear();
                        (comp as IMessage).AddToCollection(_content);
                    })
                   , message);
                    break;
            }
        }
        /*To Change the Defualt behavior of the Column*/
        public DelegateCommand<object> FilterCommand
        {
            get
            {
                return new DelegateCommand<object>((i) =>
                    {
                        if (Filter != null)
                        {
                            IEnumerable<object> filteredout;
                            extractcontent();
                            
                                var result= Filter.Filter(buffer, out filteredout);
                                if (result != null)
                                {
                                    
                                    filterResults = result.ToList();
                                    this.Content = filterResults;
                                }
                        }
                    }, (s) => { return true; });
            }
        }
        public DelegateCommand<object> CancelFilterCommand
        {
            get
            {
                return new DelegateCommand<object>((s) =>
                    {
                        Dispatcher.Invoke((i) =>
                        {
                            Content = _content;
                            filterResults.Clear();
                        });

                    }, (o) => { return true; });
            }
        }
        protected  void RecieveNewMessagesMethod(IMessage message, ColumnDirective directive)
        {
            PublishMessage(message);
            InputNewItems(message, directive);
            if (directive!=ColumnDirective.AddAndClear)
            {
                Dispatcher.Invoke((p) =>
                {
                    AdjustContentBuffer(directive);
                });
            }
            
        }
        object FromTheTop()
        {
            return _content[0];
        }
        object FromTheBottom()
        {
            return _content[_content.Count - 1];
        }
        void Clear(bool ExecOnUIThread=true)
        {
            if (ExecOnUIThread)
            {
                Dispatcher.Invoke((d) => {
                    foreach (object obj in _content)
                    {
                        if (obj is IDisposable)
                        {
                            try
                            {
                                (obj as IDisposable).Dispose();
                            }
                            catch { }
                        }
                    }
                 _content.Clear(); });
                buffer.Clear();
                filterResults.Clear();
                if (_contentholder.Equals(filterResults))
                {
                    Content = _content;
                }
                return;
            }

            foreach (object obj in _content)
            {
                if (obj is IDisposable)
                {
                    try
                    {
                        (obj as IDisposable).Dispose();
                    }
                    catch { }
                }
            }
            _content.Clear();
            buffer.Clear();
            filterResults.Clear();
            if (_contentholder.Equals(filterResults))
            {
                Content = _content;
            }
        }
        /// <summary>
        /// This matains the number of objects in the column as specified by the option file..Executed on the UI Thread
        /// </summary>
        /// <param name="directive"></param>
        void AdjustContentBuffer(ColumnDirective directive)
        {
            object content = null;
            Func<object> action = null;
            switch (directive)
            {
                case ColumnDirective.Insert:
                    action = FromTheBottom;
                    break;
                case ColumnDirective.Add:
                    action = FromTheTop;
                    break;
            }
            List<object> existsInFilter = new List<object>();
            List<IDisposable> alldisposable = new List<IDisposable>();
            while (_content.Count > Filter.MaxNumberOfItemsAllowed)
            {
                content = action();
                _content.Remove(content);
                if (filterResults.Contains(content))
                {
                    existsInFilter.Add(content);
                }
                
                if (content is IDisposable)
                { 
                    alldisposable.Add(content as IDisposable);
                }
            }
            if (existsInFilter.Count > 0)
            {
                foreach (object obj in existsInFilter)
                {
                    filterResults.Remove(obj);
                }
                Content = filterResults.ToList();
            }
            if (alldisposable.Count > 0)
            {
                foreach (IDisposable disposable in alldisposable)
                {
                    disposable.Dispose();
                }
            }

        }
        List<object> filterResults = new List<object>();
        List<object> buffer = new List<object>();
        private ObservableCollection<object> _content=new ObservableCollection<object>();
        IEnumerable _contentholder;
        public IEnumerable Content
        {
            get
            {
                return _contentholder;
            }
            set
            {
                if (value != _contentholder)
                {
                    _contentholder = value;
                    OnPropertyChanged("Content");

                }
            }

        }
        void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
        
        void extractcontent()
        {
            Dispatcher.Invoke((u) => { buffer= _content.ToList(); });
        }
        private List<object> _contentBuffer = new List<object>();
        public IList<System.Windows.Input.ICommand> Commands
        {
            get;
            set;
        }

        public NewMessageHandler RecieveNewMessages
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

        public void Initialize()
        {
            if (!initialized)
            {
                Request.Start();
                initialized = true;
            }
        }


        public event EventHandler Close;

        public void Dispose()
        {
            Clear();
            if (Filter != null)
            {
                Filter.Dispose();
            }
            if (this.Request != null)
            {
               // Request.Stop();
                Request.Dispose();
            }
        }

        public string IdentityString
        {
            get;
            set;
        }
private DelegateCommand<object> _closeCommand;
public ICommand CloseCommand
{
    get
    {
        if (_closeCommand == null)
        {
            _closeCommand = new DelegateCommand<object>((e) =>
            {
                if (Close != null)
                {
                    Close(this, null);
                }
            });
        }
        return _closeCommand;
    }
}
        private TwaijaComposite.Modules.Common.Behaviors.AnimationTrigger onWorkspaceEntry=new AnimationTrigger();
        public AnimationTrigger OnHostEntry
        {
            get
            {
                return onWorkspaceEntry;
            }
            set
            {
               onWorkspaceEntry= value;
            }
        }


        public void AddContent(object content)
        {
            Dispatcher.Invoke((d) => { _content.Add(d); },content);
        }

        public void RemoveContent(object content)
        {
            Dispatcher.Invoke((d) => { _content.Remove(d); },content);
        }

        public void InsertContent(object content)
        {
            Dispatcher.Invoke((d) => { _content.Insert(0, d); },content);
        }

        public void ClearContent()
        {
            Dispatcher.Invoke((d) => { Clear();});
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public Common.DataInterfaces.IUser Owner
        {
            get;
            set;
        }
    }
    
}
