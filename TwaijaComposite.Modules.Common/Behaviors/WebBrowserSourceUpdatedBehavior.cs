using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace TwaijaComposite.Modules.Common.Behaviors
{
   public class WebBrowserSourceUpdatedBehavior
    {
       public ICommand Command { get; set; }
       public WebBrowserSourceUpdatedBehavior(WebBrowser browser)
       {
#if SILVERLIGHT
           browser.LoadCompleted += new System.Windows.Navigation.LoadCompletedEventHandler(browser_LoadCompleted);
#else
          // browser.Navigating += new System.Windows.Navigation.NavigatingCancelEventHandler(browser_Navigating);
           browser.Navigated += new System.Windows.Navigation.NavigatedEventHandler(browser_Navigated);
#endif
       }
#if SILVERLIGHT
       void browser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
       {
           Command.Execute(e.Uri);
       }
#endif
       void browser_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
       {
           Command.Execute(e.Uri);
       }

       //void browser_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
       //{
       //    Command.Execute(e.Uri);
       //}

   
    }
}
