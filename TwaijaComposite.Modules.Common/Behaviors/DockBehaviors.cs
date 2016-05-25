using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace TwaijaComposite.Modules.Common.Behaviors
{
    public class DockBehaviors
    {
           
        public static object GetDockManager(DependencyObject obj)
        {
            return (object)obj.GetValue(DockManagerProperty);
        }

        public static void SetDockManager(DependencyObject obj, object value)
        {
            obj.SetValue(DockManagerProperty, value);
        }

        // Using a DependencyProperty as the backing store for DockManager.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DockManagerProperty =
            DependencyProperty.RegisterAttached("DockManager", typeof(object), typeof(DockBehaviors), new PropertyMetadata(null));

        
    }
}
