using System;
using System.Windows;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Prism.Modularity;
using TwaijaComposite.Modules.ApplicationEngine;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Clipboard;
using TwaijaComposite.Modules.ProfileViewer;
using TwaijaComposite.Modules.Authentication;
using TwaijaComposite.Modules.PictureViewer;
using TwaijaComposite.RequestAdapterModule;
using TwaijaComposite.Modules.ColumnsManager;
using TwaijaComposite.Modules.Options;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;


namespace TwaijaComposite
{
   public  class BootStrapper:UnityBootstrapper
    {
       protected override System.Windows.DependencyObject CreateShell()
       {
           var shell = new Shell();
         
#if SILVERLIGHT
            var model=ServiceLocator.Current.GetInstance<IUnityContainer>().Resolve<ShellViewmodel>();
            shell.DataContext = model;
            model.RequestClose += shell.HandleRequestClose;
            System.Windows.Application.Current.RootVisual = shell;
#else
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
           catalog.AddModule(typeof(ClipboardModule));
           catalog.AddModule(typeof(ProfileViewerModule));
           catalog.AddModule(typeof(AuthenticationModule));
           catalog.AddModule(typeof(PictureViewerModule));
           catalog.AddModule(typeof(ColumnsManagerModule));
           catalog.AddModule(typeof(OptionsModule));
           catalog.AddModule(typeof(ApplicationEngineModule));

           return catalog;
       }
       
    }
}
