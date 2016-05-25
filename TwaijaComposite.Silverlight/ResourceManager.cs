using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Practices.Prism.Events;
using TwaijaComposite.Modules.Common.Events;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.Interfaces;
using System.Configuration;

namespace TwaijaComposite
{
    public class ResourceManager
    {
        Dictionary<string, IShellRequestHandler> handlers = new Dictionary<string, IShellRequestHandler>();
        public ResourceManager(IEventAggregator agg,IEnumerable<IShellRequestHandler> Handlers)
        {
            var eve = agg.GetEvent<ShellRequestEvent>();
            eve.Subscribe(new Action<ShellRequestArguments>(ResolveRequestHandler),ThreadOption.UIThread,true);
            foreach (IShellRequestHandler handler in Handlers)
            {
                handlers.Add(handler.id, handler);
            }
        }

        public void ResolveRequestHandler(ShellRequestArguments args)
        {
            try
            {
                if (handlers.ContainsKey(args.SuggestedHandler))
                {
                    handlers[args.SuggestedHandler].Handler(args);
                }
            }
            catch
            {
            }
        }
    }

    public interface IShellRequestHandler
    {
        string id { get; }
        Action<ShellRequestArguments> Handler { get; set; }
    }
    public class BlueTheme : ITheme
    {
        private BlueTheme()
        {
        }
        static BlueTheme soleinstance;
        static BlueTheme() { soleinstance = new BlueTheme(); }
        public static BlueTheme Create()
        {
            return soleinstance;
        }
        public string Name
        {
            get { return InfrastructureResourceLocator.BlueTheme; }
        }

        public void Paint()
        {
            var darkblue = new SolidColorBrush(Colors.DodgerBlue);
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.maincoloralternate] = new SolidColorBrush(Colors.Black);
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.maincoloralternateforeground] = new SolidColorBrush(Colors.White);
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.mainbackgroundbrushkey] = new SolidColorBrush(Colors.DodgerBlue);
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.toolbarbackgroundkey] = new SolidColorBrush(Colors.DodgerBlue);
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.mainborderbrushkey] = darkblue;
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.mainviewuserlistforegroundbrushkey] = new SolidColorBrush(Colors.Black);
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.columnitemforegroundbrushkey] = new SolidColorBrush(Colors.Black);
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.columnworkspacebackgroundbrushkey] = darkblue;
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.mainforegroundbrushkey] = new SolidColorBrush(Colors.Black);
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.tweetbackgroundbrushkey] = darkblue;
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.mainbuttonbackgroundbrushkey] = new SolidColorBrush(Colors.Black);
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.clipboardbackgroundbrushkey] = darkblue;
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.clipboardmainforegroundbrushkey] = new SolidColorBrush(Colors.White);
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.clipboardcounterforegroundbrushkey] = new SolidColorBrush(Colors.White);
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.clipboardsecondaryforegroundbrushkey] = new SolidColorBrush(Colors.White);

        }

        public string Id
        {
            get { return InfrastructureResourceLocator.BlueTheme; }
        }
    }
    public class RedTheme : ITheme
    {
        private RedTheme()
        {
        }
        static RedTheme soleinstance;
        static RedTheme() { soleinstance = new RedTheme(); }
        public static RedTheme Create()
        {
            return soleinstance;
        }
        public string Name
        {
            get { return InfrastructureResourceLocator.RedTheme; }
        }

        public void Paint()
        {
            var darkwhite = new SolidColorBrush(new Color() { A = 255, G = 200, B = 200, R = 200 });
            var darkestwhite = new SolidColorBrush(new Color() { A = 255, G = 170, B = 170, R = 170 });
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.maincoloralternate] = new SolidColorBrush(Colors.Black);
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.maincoloralternateforeground] = new SolidColorBrush(Colors.White);
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.mainbackgroundbrushkey] = new SolidColorBrush(Colors.White);
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.toolbarbackgroundkey] = darkwhite;
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.mainborderbrushkey] = darkwhite;
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.mainviewuserlistforegroundbrushkey] = new SolidColorBrush(Colors.Black);
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.columnitemforegroundbrushkey] = new SolidColorBrush(Colors.Black);
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.columnworkspacebackgroundbrushkey] = darkwhite;
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.mainforegroundbrushkey] = new SolidColorBrush(Colors.Black);
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.tweetbackgroundbrushkey] = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.mainbuttonbackgroundbrushkey] = new SolidColorBrush(Colors.Black);
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.clipboardbackgroundbrushkey] = darkwhite;
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.clipboardmainforegroundbrushkey] = new SolidColorBrush(Colors.Black);
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.clipboardcounterforegroundbrushkey] = new SolidColorBrush(Colors.Black);
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.clipboardsecondaryforegroundbrushkey] = new SolidColorBrush(Colors.Black);

        }

        public string Id
        {
            get { return InfrastructureResourceLocator.RedTheme; }
        }
    }
    public class WhiteTheme : ITheme
    {
        private WhiteTheme()
        {
        }
        static WhiteTheme soleinstance;
        static WhiteTheme() { soleinstance = new WhiteTheme(); }
        public static WhiteTheme Create()
        {
            return soleinstance;
        }
        public string Name
        {
            get {return InfrastructureResourceLocator.WhiteTheme; }
        }

        public void Paint()
        {
          var darkwhite = new SolidColorBrush(new Color() { A = 255, G = 200, B = 200, R = 200 });
          var darkestwhite =new SolidColorBrush( new Color() { A = 255, G = 170, B = 170, R = 170 });
          ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.maincoloralternate] = new SolidColorBrush(Colors.Black);
          ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.maincoloralternateforeground] = new SolidColorBrush(Colors.White);
          ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.mainbackgroundbrushkey]=new SolidColorBrush(Colors.White);
          ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.toolbarbackgroundkey]  =darkwhite;
          ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.mainborderbrushkey] = darkwhite;
          ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.mainviewuserlistforegroundbrushkey]= new SolidColorBrush(Colors.Black);
          ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.columnitemforegroundbrushkey] =new SolidColorBrush(Colors.Black);
          ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.columnworkspacebackgroundbrushkey] =darkwhite;
          ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.mainforegroundbrushkey] = new SolidColorBrush(Colors.Black);
          ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.tweetbackgroundbrushkey] =new SolidColorBrush(Color.FromArgb(255,255,255,255));
          ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.mainbuttonbackgroundbrushkey] =new SolidColorBrush( Colors.Black);
          ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.clipboardbackgroundbrushkey] =darkwhite;
          ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.clipboardmainforegroundbrushkey] =new SolidColorBrush(Colors.Black);
          ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.clipboardcounterforegroundbrushkey] = new SolidColorBrush(Colors.Black);
          ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.clipboardsecondaryforegroundbrushkey] =new SolidColorBrush( Colors.Black);
        
        }

        public string Id
        {
            get { return InfrastructureResourceLocator.WhiteTheme;  }
        }
    }
    public class PurpleTheme : ITheme
    {
        private PurpleTheme()
        {
        }
        static PurpleTheme soleinstance;
        static PurpleTheme() { soleinstance = new PurpleTheme(); }
        public static PurpleTheme Create()
        {
            return soleinstance;
        }
        public string Name
        {
            get { return InfrastructureResourceLocator.PurpleTheme; }
        }

        public void Paint()
        {
            var darkblue = new SolidColorBrush(new Color() { A = 255, G = 100, B = 170, R = 100 });
            var darkestblue = new SolidColorBrush(new Color() { A = 255, G = 50, B = 120, R = 50 });
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.maincoloralternate] = new SolidColorBrush(Colors.White);
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.maincoloralternateforeground] = new SolidColorBrush(Colors.Black);
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.mainbackgroundbrushkey] = (darkblue);
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.toolbarbackgroundkey] = darkblue;
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.mainborderbrushkey] = darkblue;
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.mainviewuserlistforegroundbrushkey] = new SolidColorBrush(Colors.White);
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.columnitemforegroundbrushkey] = new SolidColorBrush(Colors.White);
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.columnworkspacebackgroundbrushkey] = darkestblue;
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.mainforegroundbrushkey] = new SolidColorBrush(Colors.White);
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.tweetbackgroundbrushkey] = new SolidColorBrush(Color.FromArgb(255,60,60,120));
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.mainbuttonbackgroundbrushkey] = new SolidColorBrush(Colors.White);
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.clipboardbackgroundbrushkey] = darkestblue;
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.clipboardmainforegroundbrushkey] = new SolidColorBrush(Colors.White);
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.clipboardcounterforegroundbrushkey] = new SolidColorBrush(Colors.White);
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.clipboardsecondaryforegroundbrushkey] = new SolidColorBrush(Colors.White);
        }

        public string Id
        {
            get { return InfrastructureResourceLocator.PurpleTheme; }
        }
    }
    public class DefaultTheme : ITheme
    {
        private DefaultTheme()
        {
        }
        static DefaultTheme soleinstance;
        static DefaultTheme() { soleinstance = new DefaultTheme(); }
        public static DefaultTheme Create()
        {
            return soleinstance;
        }
        public string Name
        {
            get { return InfrastructureResourceLocator.DefaultTheme; }
        }

        public void Paint()
        {
#if !SILVERLIGHT
            var clipcounter = new LinearGradientBrush(new GradientStopCollection() {new GradientStop(Color.FromArgb(65,255,255,255),0),new GradientStop(Color.FromArgb(65,62,62,62),0.879) });
#endif  
            var mainbrush =new SolidColorBrush( Color.FromArgb(255, 39, 39, 39));
            var toolbar =new SolidColorBrush(Color.FromArgb(53, 92, 92, 92));
            var border = new SolidColorBrush(Color.FromArgb(255, 29, 29, 29));
            var columnitemforegroundbrush = new SolidColorBrush(Color.FromArgb(255, 167, 167, 167));
            var tweet = new SolidColorBrush(Color.FromArgb(255, 27, 27, 27));
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.maincoloralternate] = new SolidColorBrush(Colors.White);
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.maincoloralternateforeground] = new SolidColorBrush(Colors.Black);
          ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.mainbackgroundbrushkey]  =mainbrush;
          ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.toolbarbackgroundkey] = toolbar;
          ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.mainborderbrushkey] = border;
           ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.mainviewuserlistforegroundbrushkey] = new SolidColorBrush(Colors.White);
         ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.columnitemforegroundbrushkey]= columnitemforegroundbrush;
          ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.columnworkspacebackgroundbrushkey] = border;
             ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.mainforegroundbrushkey]  = new SolidColorBrush(Colors.LightGray);
           ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.tweetbackgroundbrushkey] = tweet;
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.mainbuttonbackgroundbrushkey] =new SolidColorBrush( Colors.DarkGray);
             ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.clipboardbackgroundbrushkey]=border;
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.clipboardmainforegroundbrushkey]= new SolidColorBrush(Colors.LightGray);
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.clipboardsecondaryforegroundbrushkey]= new SolidColorBrush(Colors.Orange);
#if !SILVERLIGHT
            ResourceLocator.ApplicationBrushDictionary[InfrastructureResourceLocator.clipboardcounterforegroundbrushkey] = clipcounter;
#endif
        }


        public string Id
        {
            get { return InfrastructureResourceLocator.WhiteTheme; }
        }
    }
    public class ApplicationPaintEventHandler : IShellRequestHandler
    {
        Dictionary<string, ITheme> Themes = new Dictionary<string, ITheme>();
        public ApplicationPaintEventHandler()
        {
            Handler = ProcessRequest;
            ITheme theme = WhiteTheme.Create();
            ITheme theme2 = DefaultTheme.Create();
            ITheme theme3=PurpleTheme.Create();
            //ITheme theme4 = BlueTheme.Create();
            //ITheme theme5 = RedTheme.Create();
            Themes.Add(theme.Name, theme);
            Themes.Add(theme2.Name, theme2);
            Themes.Add(theme3.Name, theme3);
            //Themes.Add(theme4.Name, theme4);
            //Themes.Add(theme5.Name, theme5);
        }
        public void ProcessRequest(ShellRequestArguments args)
        {
            if (args.Parameters.ContainsKey(InfrastructureResourceLocator.SuggestedThemeKey))
            {
                string suggestedTheme = (string) args.Parameters[InfrastructureResourceLocator.SuggestedThemeKey];
                if (Themes.ContainsKey(suggestedTheme))
                {
                    Themes[suggestedTheme].Paint();
                }
            }
        }
        public string id
        {
            get { return InfrastructureResourceLocator.ThemeChangeRequestHandlerKey; }
        }

        public Action<ShellRequestArguments> Handler
        {
            get;
            set;
        }
    }
}
