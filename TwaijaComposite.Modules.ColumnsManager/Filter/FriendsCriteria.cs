using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.AttributeTypes;

namespace TwaijaComposite.Modules.ColumnsManager.Filter
{
    public class FriendsCriteria:CriteriaBase<FriendPropertyLocatorAttribute>
    {
        public FriendsCriteria()
        {
            Operands = new List<string>();
            Operands.Add(">");
            Operands.Add("<");
            Operands.Add("=");
            _condition = CheckMatch;
            selectedOperand = Operands[0];
        }
        private long setValue;
        public long SetValue
        {
            get { return setValue; }
            set
            {
                if (setValue != value)
                {
                    setValue = value;
                    OnPropertyChanged("SetValue");
                }
            }
        }
        public IList<string> Operands
        {
            get;
            set;
        }
        private string selectedOperand;
        public string SelectedOperand
        {
            get { return selectedOperand; }
            set
            {
                if (selectedOperand != value)
                {
                    selectedOperand = value;
                    OnPropertyChanged("SelectedOperand");
                }
            }
        }
        bool CheckMatch(object friendcount)
        {
            if (SelectedOperand == ">")
            {
                if ((Convert.ToInt64(friendcount) > SetValue))
                {
                    return true;
                }
                return false;
                
            }
            if(SelectedOperand=="<")
            {
                if ((Convert.ToInt64(friendcount) < SetValue))
                {
                    return true;
                }
                return false;
            }
            if ((Convert.ToInt64(friendcount) == SetValue))
            {
                return true;
            }
            return false;
        }
    }
}
