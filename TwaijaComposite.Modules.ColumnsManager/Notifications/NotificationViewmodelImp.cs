using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using TwaijaComposite.Modules.Common.Services;
using Microsoft.Practices.Unity;
//using Microsoft.Practices.Composite.Presentation.Commands;
using System.Windows.Input;
using System.ComponentModel;
using Microsoft.Practices.Prism.Commands;

namespace TwaijaComposite.Modules.ColumnsManager.Notifications
{
    public class NotificationViewmodelImp:INotificationViewmodel,INotifyPropertyChanged
    {
        [Dependency]
        public IDispatcher dispatcher { get; set; }
        ObservableCollection<Notice> _content = new ObservableCollection<Notice>();
        public IEnumerable<Notice> Content
        {
            get
            {
                return _content;
            }
        }
        private Notice selected;
        public Notice Selected
        {
            get { return selected; }
            set
            {
                selected = value; 
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Selected"));
                }
            }
        }
        public void Add(Notice notice)
        {
            dispatcher.Invoke(new System.Threading.SendOrPostCallback((o) => { var n = o as Notice;
            List<Notice> deleted = new List<Notice>();
            foreach (Notice current in _content)
            {
                if (n.Title == current.Title)
                {
                    foreach (object obj in current.Content)
                    {
                        n.Content.Add(obj);
                    }
                    deleted.Add(current);
                }       
            }
            foreach (Notice deadnotice in deleted)
            {
                _content.Remove(deadnotice);
            }
            if (_content.Count != 0)
            {
                _content.Insert(0, n);
            }
            else
            {
                _content.Add(n);
            }
            Selected = n;
            }), notice);
        }

        public void Clear()
        {
            dispatcher.Invoke(new System.Threading.SendOrPostCallback((o) => { _content.Clear(); }));
        }

        public event EventHandler CloseContent;
        public ICommand CloseCommand
        {
            get
            {
                if (closeCommand == null)
                {
                    closeCommand = new DelegateCommand<object>((s) =>
                    {
                        if (CloseContent != null)
                        {
                            CloseContent(this, null);
                        }
                    });
                }
                return closeCommand; }
            
        }
        DelegateCommand<object> closeCommand;


        public void TriggerContentClosure()
        {
            if (CloseContent != null)
            {
                CloseContent(this, null);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
