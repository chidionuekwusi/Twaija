using System;
using System.Collections.Generic;
using TwaijaComposite.Modules.Common;
using System.ComponentModel;
using TwaijaComposite.Modules.Common.Preferencing;
//using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Commands;
namespace TwaijaComposite.Modules.ColumnsManager.Filter
{
    
    public class FilterImp:IFilter,INotifyPropertyChanged
    {
        #region properties and fields
        public event NewMessageHandler NewMessage = (w, o) => { };
        public event PropertyChangedEventHandler PropertyChanged;
        private PropertyChangedManager p_Manager;
        public int MaxNumberOfItemsAllowed { get; set; }
        private  Preferences pref;
        private bool filterIncoming;
        public bool FilterIncoming
        {
            get { return filterIncoming; }
            set
            {
                if (filterIncoming != value)
                {
                    filterIncoming = value;
                    OnPropertyChanged("FilterIncoming");
                }
            }
        }
        private List<IFilterCriteria> _criterion;
        public IList<IFilterCriteria> Criterion
        {
            get
            {
                if (_criterion == null)
                {
                    _criterion = new List<IFilterCriteria>();
                }
                return _criterion;
            }
        }
        private DelegateCommand<object> _includeOrUndoCommand;
        public DelegateCommand<object> IncludeOrUndoCommand
        {
            get
            {
                if (_includeOrUndoCommand == null)
                {
                    _includeOrUndoCommand = new DelegateCommand<object>((p) =>
                    {
                        if (Selected.Included)
                        {
                            Selected.Included = false;
                            CanAdd = true;
                            return;
                        }
                        Selected.Included = true;
                        CanAdd = false;
                    });
                }
                return _includeOrUndoCommand;
            }
        }
        private IFilterCriteria _selected;
        public IFilterCriteria Selected
        {
            get
            {
                if (Criterion != null)
                {
                    if (_selected == null)
                    {
                        _selected = Criterion[0];
                    }
                }
                return _selected;
            }
            set
            {
                if (_selected != value)
                {
                    _selected = value;
                    OnPropertyChanged("Selected");
                    if (Selected.Included)
                    {
                        CanAdd = false;
                    }
                    else
                    {
                        CanAdd = true;
                    }
                }
            }
        }
        private bool canAdd = true;
        public bool CanAdd
        {
            get { return canAdd; }
            set
            {
                canAdd = value;
                OnPropertyChanged("CanAdd");
            }
        }
        #endregion

        public FilterImp()
        {

        }

        [InjectionConstructor]
        public FilterImp(Preferences pref)
        {
            p_Manager = new PropertyChangedManager(this);
            this.pref = pref;
            MaxNumberOfItemsAllowed = pref.GeneralOptions.MaximumNumberOfItemsInASingleColumn;
            p_Manager["MaximumNumberOfItemsInASingleColumn"] = new PropertyChangeReaction("MaxNumberOfItemsAllowed");
            pref.GeneralOptions.PropertyChanged += p_Manager.PropertyChangedHandler;
        }

        #region methods

        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        void OnNewMessage(IMessage message, ColumnDirective directive)
        {
            NewMessage(message, directive);
        }
        IFilterCriteria CreateChain()
        {
            IFilterCriteria buffer = null;
            IFilterCriteria firstcriteria = null;
            foreach (IFilterCriteria crit in Criterion)
            {

                if (crit.Included)
                {
                    if (buffer == null)
                    {
                        buffer = crit;
                        firstcriteria = crit;
                    }
                    else
                    {
                        buffer.nextCriteria = crit;
                        buffer = crit;
                    }
                }
            }
            return firstcriteria;
        }
        public void Dispose()
        {
            
        }
        public IEnumerable<object> Filter(IEnumerable<object> content, out IEnumerable<object> filteredoutcontent)
        {
            if (content == null)
            {
                throw new ArgumentNullException("content must be passed to the filter");
            }
            var chain = CreateChain();
            filteredoutcontent = new List<object>();
            if (chain != null)
            {
                List<object> matches = new List<object>();
               
                var fcontent = (List<object>)filteredoutcontent;
                foreach (object o in content)
                {
                    if (chain.Filter(o))
                    {
                        matches.Add(o);
                    }
                    else
                    {
                        fcontent.Add(o);
                    }
                }
                return matches;
               
            }
            return null;
        }

        #endregion
    }
}
