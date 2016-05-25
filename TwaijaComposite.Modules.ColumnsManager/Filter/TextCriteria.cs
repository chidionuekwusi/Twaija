using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using TwaijaComposite.Modules.Common.AttributeTypes;

namespace TwaijaComposite.Modules.ColumnsManager.Filter
{
    public class TextCriteria:CriteriaBase<TextPropertyLocatorAttribute>
    {
        public TextCriteria()
        {
            _condition = CheckMatch;
        }
        private string _text=string.Empty;
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged("Text");
            }
        }
        bool CheckMatch(object text)
        {
            if (!string.IsNullOrEmpty(text as string) && !string.IsNullOrEmpty(Text))
            {
                if (text.ToString().ToLower().Contains(Text.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
