using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TwaijaComposite.Modules.Common.Behaviors
{
    public static class WebBrowserBehaviours
    {
                        

        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for CommandProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(WebBrowserBehaviours), new PropertyMetadata(default(ICommand),new PropertyChangedCallback(CommandPropertyChanged)));



        public static WebBrowserSourceUpdatedBehavior GetSourceUpdatedBehaviour(DependencyObject obj)
        {
            return (WebBrowserSourceUpdatedBehavior)obj.GetValue(SourceUpdatedBehaviourProperty);
        }

        public static void SetSourceUpdatedBehaviour(DependencyObject obj, WebBrowserSourceUpdatedBehavior value)
        {
            obj.SetValue(SourceUpdatedBehaviourProperty, value);
        }

        // Using a DependencyProperty as the backing store for SourceUpdatedBehaviourProperty.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty SourceUpdatedBehaviourProperty =
            DependencyProperty.RegisterAttached("SourceUpdatedBehaviour", typeof(WebBrowserSourceUpdatedBehavior), typeof(WebBrowserBehaviours), new PropertyMetadata(default(WebBrowserSourceUpdatedBehavior)));

        
            public static readonly DependencyProperty BindableSourceProperty =
                DependencyProperty.RegisterAttached("BindableSource", typeof(object), typeof(WebBrowserBehaviours), new PropertyMetadata(null, BindableSourcePropertyChanged));

            public static object GetBindableSource(DependencyObject obj)
            {
                return (string)obj.GetValue(BindableSourceProperty);
            }

            public static void SetBindableSource(DependencyObject obj, object value)
            {
                obj.SetValue(BindableSourceProperty, value);
            }

            public static void BindableSourcePropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                WebBrowser browser = o as WebBrowser;
                if (browser == null) return;

                Uri uri = null;

                if (e.NewValue is string)
                {
                    var uriString = e.NewValue as string;
                    uri = string.IsNullOrEmpty(uriString) ? null : new Uri(uriString);
                }
                else if (e.NewValue is Uri)
                {
                    uri = e.NewValue as Uri;
                }

                browser.Source = uri;
            }
            public static void CommandPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                WebBrowser browser = o as WebBrowser;
                if (browser != null)
                {
                    WebBrowserSourceUpdatedBehavior behavior = GetOrCreateBehavior(browser);
                    behavior.Command = e.NewValue as ICommand;
                }
            }
            private static WebBrowserSourceUpdatedBehavior GetOrCreateBehavior(WebBrowser browser)
            {
                WebBrowserSourceUpdatedBehavior behavior = browser.GetValue(SourceUpdatedBehaviourProperty) as WebBrowserSourceUpdatedBehavior;
                if (behavior == null)
                {
                    behavior = new WebBrowserSourceUpdatedBehavior(browser);
                    browser.SetValue(SourceUpdatedBehaviourProperty, behavior);
                }

                return behavior;
            }
        }
    
}
