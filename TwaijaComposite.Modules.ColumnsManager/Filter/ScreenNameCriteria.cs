using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using TwaijaComposite.Modules.Common.AttributeTypes;

namespace TwaijaComposite.Modules.ColumnsManager.Filter
{
    public class ScreenNameCriteria : CriteriaBase<ScreenNamePropertyLocatorAttribute>
    {
        string _screenName;
        public string ScreenName
        {
            get { return _screenName; }
            set
            {
                if (_screenName != value)
                {
                    _screenName = value;
                    OnPropertyChanged("ScreenName");
                }
            }
        }
         bool CheckMatch(object name)
        {
            if (name!=null && !(string.IsNullOrEmpty(ScreenName)))
            {
                if (name.ToString().ToLower().Contains(ScreenName.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }
        public ScreenNameCriteria()
        {
            _condition = CheckMatch;
        }
      
    }
}
