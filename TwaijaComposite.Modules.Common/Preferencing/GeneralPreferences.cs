using System;
using System.Collections.Generic;
using System.ComponentModel;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common.Events;

namespace TwaijaComposite.Modules.Common.Preferencing
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public class GeneralPreferences
    {
        public GeneralPreferences()
        {
            ColumnsCreated = new List<string>();
            MaximumNumberOfItemsInASingleColumn = 100;
        }
        public bool IsNotificationEnabled { get; set; }
        public ThemeViewmodel SelectedTheme { get; set; }
        private bool prompt=true;
        public List<string> ColumnsCreated { get; set; }
        public bool PromptUserSelectionDialog { get
        { return prompt; }
            set { prompt = value; }
        }
        public bool ShowMessageSentDialog { get; set; }
        int maxNumberOfItemsInASingleColumn;
        public int MaximumNumberOfItemsInASingleColumn
        {
            get
            {
                return maxNumberOfItemsInASingleColumn;
            }
            set
            {
                if (value != maxNumberOfItemsInASingleColumn)
                {
                    maxNumberOfItemsInASingleColumn = value;
                    OnPropertyChanged("MaximumNumberOfItemsInASingleColumn");
                }
            }
        }
        public bool IsOldRetweetStyle { get; set; }
        private Position selectedNotificationWindowPostion=Position.BottomRight;    
        public Position SelectedNotificationWindowPosition
        {
            get { return selectedNotificationWindowPostion; }
            set { selectedNotificationWindowPostion = value; }
        }
        public void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
        
#if !SILVERLIGHT
    [field:NonSerialized]
#endif
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
