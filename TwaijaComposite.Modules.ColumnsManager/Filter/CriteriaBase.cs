using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using TwaijaComposite.Modules.Common.AttributeTypes;

namespace TwaijaComposite.Modules.ColumnsManager.Filter
{
    public abstract class CriteriaBase<T> : IFilterCriteria, INotifyPropertyChanged where T:class,IPropertyLocator
    {
  
        protected Predicate<object> _condition;
        private bool _included;
        public bool Included
        {
            get
            {
                return _included;
            }
            set
            {
                if (_included != value)
                {
                    _included = value;
                    OnPropertyChanged("Included");
                }
            }
        }

        public bool Filter(object state)
        {
            try
            {
                object[] g = state.GetType().GetCustomAttributes(typeof(T), true);
                if (g != null)
                {
                    var targetname = (g.First() as T).TargetPropertyName;
                    var value = state.GetType().GetProperty(targetname).GetValue(state, null);
                    if (_condition(value))
                    {
                        if (nextCriteria == null)
                        {
                            return true;
                        }
                        else
                        {
                            return nextCriteria.Filter(state);
                        }
                    }

                }
                return false;
            }
            catch
            {
                return false;
            }
        }
  

        public IFilterCriteria nextCriteria
        {
            get;
            set;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
