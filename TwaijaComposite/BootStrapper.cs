using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Prism.Modularity;
using TwaijaComposite.Modules.ApplicationEngine;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Clipboard;
using TwaijaComposite.Modules.ProfileViewer;
using TwaijaComposite.Modules.Authentication;
using TwaijaComposite.Modules.PictureViewer;
using System.Windows;
using TwaijaComposite.RequestAdapterModule;
using TwaijaComposite.Modules.ColumnsManager;
using TwaijaComposite.Modules.Options;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.TweetSharpAdapter;
using TwaijaComposite.Modules.LINQToTwitterAdapter;
#if !SILVERLIGHT
//using TwaijaComposite.Modules.TweetSharpAdapter;
#endif
//using TwaijaComposite.Modules.Data;

namespace TwaijaComposite
{
   public  class BootStrapper:UnityBootstrapper
    {
       
       protected override System.Windows.DependencyObject CreateShell()
       {
           var shell = new Shell();
#if SILVERLIGHT
            System.Windows.Application.Current.RootVisual = shell;
#else
           var model = ServiceLocator.Current.GetInstance<IUnityContainer>().Resolve<ShellViewmodel>();
           shell.DataContext = model;
           Application.Current.MainWindow = shell;
           shell.Show();
#endif
           return shell;
       }
       protected override Microsoft.Practices.Prism.Modularity.IModuleCatalog CreateModuleCatalog()
       {
           var catalog = new ModuleCatalog();
           catalog.AddModule(typeof(CommonModule));
           catalog.AddModule(typeof(ShellModule));
           catalog.AddModule(typeof(RequestAdapterMethodsModule));
           catalog.AddModule(typeof(TweetSharpAdapterModule));
           catalog.AddModule(typeof(ClipboardModule));
           catalog.AddModule(typeof(ProfileViewerModule));
           catalog.AddModule(typeof(AuthenticationModule));
           catalog.AddModule(typeof(PictureViewerModule));
           catalog.AddModule(typeof(ColumnsManagerModule));
           catalog.AddModule(typeof(OptionsModule));
           catalog.AddModule(typeof(ApplicationEngineModule));
           catalog.AddModule(typeof(LINQToTwitterAdapterModule));
           return catalog;
       }
       
    }
}
