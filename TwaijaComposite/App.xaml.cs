using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace TwaijaComposite
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
           
        }

        protected override void OnStartup(StartupEventArgs e)
        {          
            base.OnStartup(e);
            this.Resources.MergedDictionaries.Add(ResourceLocator.ApplicationBrushDictionary);
            new BootStrapper().Run();
        }
    }
}
