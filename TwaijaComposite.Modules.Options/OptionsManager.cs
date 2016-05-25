using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.Preferencing;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.Common.Events;
using Microsoft.Practices.Prism.Events;

namespace TwaijaComposite.Modules.Options
{
    public class OptionsController:IController
    {
        [Dependency]
        public Preferences General { get; set; }
        [Dependency]
        public IEventAggregator aggr { get; set; }
        public void Initialize()
        {
            if (General.GeneralOptions.SelectedTheme != null)
            {
                var parameters = new ShellRequestArguments() { SuggestedHandler = InfrastructureResourceLocator.ThemeChangeRequestHandlerKey };
                parameters.Parameters.Add(InfrastructureResourceLocator.SuggestedThemeKey, General.GeneralOptions.SelectedTheme.Id);
                var eve = aggr.GetEvent<ShellRequestEvent>();
                eve.Publish(parameters);
            }
        }
    }
}
