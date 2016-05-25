using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Options.Viewmodels;
using System.Windows.Input;
using TwaijaComposite.Modules.Common.Preferencing;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.ViewModels;
using Microsoft.Practices.Prism.Events;
using TwaijaComposite.Modules.Common.Events;

namespace TwaijaComposite.Modules.Options.Viewmodels
{
    public class GeneralViewModel : ViewModelBase,IGeneralViewmodel
    {
       public Preferences Data {get;set;}
       public List<Position> AvailablePositions { get; private set; }
       public List<int> MaximumNumberOfItemsInASingleColumnAvailableOptions { get; private set; }
       public GeneralViewModel(Preferences data, IEventAggregator aggr)
        {
            this.Data=data;
            this.aggr = aggr;
            Initialize();
        }
        void Initialize()
        {
            AvailablePositions = new List<Position>();
            MaximumNumberOfItemsInASingleColumnAvailableOptions = new List<int>();
            Themes = new List<ThemeViewmodel>();
            Themes.Add(new ThemeViewmodel() { DisplayName = "White", Id = InfrastructureResourceLocator.WhiteTheme ,ColorValue="White" });
            Themes.Add(new ThemeViewmodel() {  DisplayName = "Default", Id = InfrastructureResourceLocator.DefaultTheme ,ColorValue="#FF252525" });
            Themes.Add(new ThemeViewmodel() { DisplayName = "Purple", Id = InfrastructureResourceLocator.PurpleTheme, ColorValue="Purple" });
            //Themes.Add(new ThemeViewmodel() { DisplayName = "Blue", Id = InfrastructureResourceLocator.BlueTheme, ColorValue = "DarkBlue" });
            //Themes.Add(new ThemeViewmodel() { DisplayName="Maroon",Id=InfrastructureResourceLocator.RedTheme,ColorValue="Maroon"});
            AvailablePositions.Add(Position.TopLeft);
            AvailablePositions.Add(Position.BottomLeft);
            AvailablePositions.Add(Position.TopRight);
            AvailablePositions.Add(Position.BottomRight);  
            MaximumNumberOfItemsInASingleColumnAvailableOptions.Add(50);
            MaximumNumberOfItemsInASingleColumnAvailableOptions.Add(80);
            MaximumNumberOfItemsInASingleColumnAvailableOptions.Add(100);
        }
        public string Header
        { 
            get { return "general"; }
        }
        public IEventAggregator aggr { get; set; }
        public List<ThemeViewmodel> Themes { get; set; }
        private ThemeViewmodel _selectedTheme;

        public ThemeViewmodel SelectedTheme
        {
            get { return _selectedTheme; }
            set
            {
                if (_selectedTheme != value)
                {
                    _selectedTheme = value;
                    var parameters = new ShellRequestArguments() { SuggestedHandler = InfrastructureResourceLocator.ThemeChangeRequestHandlerKey };
                    parameters.Parameters.Add(InfrastructureResourceLocator.SuggestedThemeKey, value.Id);
                    var eve = aggr.GetEvent<ShellRequestEvent>();
                    eve.Publish(parameters);
                    Data.GeneralOptions.SelectedTheme = value;

                }
            }
        }
        public int SelectedMaxNumberOfItemsInASingleColumn
        {
            get { return Data.GeneralOptions.MaximumNumberOfItemsInASingleColumn; }
            set
            {
                if (Data.GeneralOptions.MaximumNumberOfItemsInASingleColumn != value)
                {
                    Data.GeneralOptions.MaximumNumberOfItemsInASingleColumn = value;
                    OnPropertyChanged("SelectedMaxNumberOfItemsInASingleColumn");
                }
            }
        }
        public bool IsOldRetweetStyle
        {
            get { return Data.GeneralOptions.IsOldRetweetStyle; }
            set
            {
                if (Data.GeneralOptions.IsOldRetweetStyle != value)
                {
                    Data.GeneralOptions.IsOldRetweetStyle = value;
                    OnPropertyChanged("IsOldRetweetStyle");
                }
            }
        }
        public bool IsNotificationsEnabled
        {
            get { return Data.GeneralOptions.IsNotificationEnabled; }
            set
            {
                if (Data.GeneralOptions.IsNotificationEnabled != value)
                {
                    Data.GeneralOptions.IsNotificationEnabled = value;
                    OnPropertyChanged("IsNotificationsEnabled");
                }
            }
        }
        public bool PromptUserSelectionDialog
        {
            get { return Data.GeneralOptions.PromptUserSelectionDialog; }
            set
            {
                if (Data.GeneralOptions.PromptUserSelectionDialog != value)
                {
                    Data.GeneralOptions.PromptUserSelectionDialog = value;
                    OnPropertyChanged("PromptUserSelectionDialog");
                }
            }
        }
        public Position SelectedPosition
        {
            get { return Data.GeneralOptions.SelectedNotificationWindowPosition; }
            set
            {
                if (Data.GeneralOptions.SelectedNotificationWindowPosition != value)
                {
                    Data.GeneralOptions.SelectedNotificationWindowPosition = value;
                    OnPropertyChanged("SelectedPosition");
                }
            }
        }

        public bool ShowMessageSentDialog
        {
            get { return Data.GeneralOptions.ShowMessageSentDialog; }
            set
            {
                if (Data.GeneralOptions.ShowMessageSentDialog != value)
                {
                    Data.GeneralOptions.ShowMessageSentDialog = value;
                    OnPropertyChanged("ShowMessageSentDialog ");
                }
            }
        }
    }
}
