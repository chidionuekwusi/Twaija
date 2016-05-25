using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace TwaijaComposite.Modules.ColumnsManager.Views
{
    [TemplateVisualState(GroupName="CommonStates",Name="Normal")]
    [TemplateVisualState(GroupName="CommonStates",Name="MouseOver")]
    public partial class TweetViewmodelTemplate : UserControl
    {
        public TweetViewmodelTemplate()
        {
            InitializeComponent();
            this.MouseEnter += new MouseEventHandler(TweetViewmodelTemplate_MouseEnter);
            this.MouseLeave += new MouseEventHandler(TweetViewmodelTemplate_MouseLeave);
            this.Loaded += new RoutedEventHandler(TweetViewmodelTemplate_Loaded);
            VisualStateManager.GoToState(this, "Normal", false);
        }

        void TweetViewmodelTemplate_Loaded(object sender, RoutedEventArgs e)
        {
           
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            
        }
        void TweetViewmodelTemplate_MouseLeave(object sender, MouseEventArgs e)
        {
            VisualStateManager.GoToState(this, "Normal", true);
        }

        void TweetViewmodelTemplate_MouseEnter(object sender, MouseEventArgs e)
        {
            VisualStateManager.GoToState(this, "MouseOver", true);
        }
    }
}
