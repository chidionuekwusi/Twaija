using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using TwaijaComposite.Modules.Common.Services;
using System.Threading;
using TwaijaComposite.Modules.Common;

namespace TwaijaComposite.Modules.ColumnsManager.Viewmodels
{
    public class ColumnsWorkspaceViewmodel:IColumnsWorkspaceViewmodel
    {
        private IDispatcher Dispatcher;
        public ColumnsWorkspaceViewmodel
            (IDispatcher dispatcher)
        {
            Dispatcher = dispatcher;
        }
        private ObservableCollection<object> _content;
        public IList<object> Content
        {
            get
            {
                if (_content == null)
                {
                    _content = new ObservableCollection<object>();
                    _content.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(_content_CollectionChanged);
                }
                return _content;
            }
        }

        void _content_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {

        }

        public void Add(object data)
        {
            Dispatcher.Invoke(new SendOrPostCallback((p) => 
            { 
                Content.Add(p);
            }), data);
            Notify(data);
        }
        public void Notify(object data)
        {
            Dispatcher.Invoke(new SendOrPostCallback((p) =>
            {
                var notifyObject = p as INotifyOnHostEntry;
                if (notifyObject != null)
                {
                    lock (notifyObject)
                    {
                        if (notifyObject.OnHostEntry != null)
                        {
                            notifyObject.OnHostEntry.Trigger();
                        }
                    }
                }
            }), data); 
        }
        public void Remove(object data)
        {
            Dispatcher.Invoke(new SendOrPostCallback((p) => { Content.Remove(p); }), data);  
        }

    }
}
