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
using System.Windows.Data;
using System.Collections;

namespace TwaijaComposite.Modules.ProfileViewer.Views
{
    public class TabItemSourceConverter : IValueConverter
    {
        public DataTemplate TabItemContentTemplate { get; set; }
        public DataTemplate TabItemHeaderTemplate { get; set; }
        public Style TabItemStyle { get; set; }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                List<TabItem> items = new List<TabItem>();
                var list = value as IEnumerable;
                    foreach (object model in list)
                    {
                        var tab=new TabItem();
                        if(TabItemContentTemplate!=null)
                        {
                            tab.ContentTemplate=this.TabItemContentTemplate;
                        }
                        if(TabItemHeaderTemplate!=null)
                        {
                            tab.HeaderTemplate=this.TabItemHeaderTemplate;
                        }
                        if (TabItemStyle != null)
                        {
                            tab.Style = TabItemStyle;
                        }
                        tab.Content=model;
                        tab.Header=model;
                        items.Add(tab);
                    }
                
                return items;

            }
            return null;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class TabItemSelectedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var item = value as TabItem;
            if (item != null)
            {
                return item.Content;
            }
            return null;
        }
    }

    public partial class ProfileView : UserControl
    {
        public ProfileView()
        {
            InitializeComponent();
        }
    }
}
