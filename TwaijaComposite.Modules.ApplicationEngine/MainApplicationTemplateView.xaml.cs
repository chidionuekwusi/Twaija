using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TwaijaComposite.Modules.Common.Interfaces;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.Common.Events;

namespace TwaijaComposite.Modules.ApplicationEngine
{
    /// <summary>
    /// Interaction logic for MainApplicationTemplateView.xaml
    /// </summary>
    public partial class MainApplicationTemplateView : UserControl,INavigationAware
    {
        public MainApplicationTemplateView(ApplicationEngine.ViewModels.ApplicationEngineViewModel context)
        {
            InitializeComponent();
            DataContext = context;
        }

        public Action<NavigatingEventArgs> Navigating
        {
            get
            {
                return new Action<NavigatingEventArgs>((args) =>
                    {
                        NavigatingEvent(this, args);
                    });
            }
        }

        public event Common.Events.NavigatingEventHandler NavigatingEvent =new Common.Events.NavigatingEventHandler((p,o)=> { });
    }
}
