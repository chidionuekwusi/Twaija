using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.ViewModels;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Events;
using TwaijaComposite.Modules.Common.Preferencing;
using TwaijaComposite.Modules.Common.Events;
using TwaijaComposite.Modules.Common;

namespace TwaijaComposite.Modules.Options.Viewmodels
{
    
    public class AppearanceViewmodel:ViewModelBase,IAppearanceViewmodel
    {
        GeneralPreferences pref;
       // [InjectionConstructor]
        public AppearanceViewmodel(IEventAggregator aggr,Preferences pref)
        {
            this.aggr = aggr;
            this.pref = pref.GeneralOptions;
            Themes = new List<ThemeViewmodel>();
            Themes.Add(new ThemeViewmodel() { DisplayName="White",Id=InfrastructureResourceLocator.WhiteTheme });
            Themes.Add(new ThemeViewmodel() { DisplayName = "Default", Id = InfrastructureResourceLocator.DefaultTheme });
        
        
        
        }

        public IEventAggregator aggr { get; set; }
        public List<ThemeViewmodel> Themes { get; set; }
        private ThemeViewmodel _selectedTheme;

        public ThemeViewmodel SelectedTheme
        {
            get { return _selectedTheme; }
            set {
                if (_selectedTheme != value)
                {
                    _selectedTheme = value;
                    var parameters = new ShellRequestArguments() { SuggestedHandler = InfrastructureResourceLocator.ThemeChangeRequestHandlerKey };
                    parameters.Parameters.Add(InfrastructureResourceLocator.SuggestedThemeKey, value.Id);
                    var eve=aggr.GetEvent<ShellRequestEvent>();
                    eve.Publish(parameters);
                    pref.SelectedTheme = value;

                }
            }
        }
        
        public string Header
        {
            get { return "appearance"; }
        }
    }
}
