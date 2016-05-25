using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace TwaijaComposite.Modules.Common.Behaviors
{
    public class ExpansionBehavior
    {
        public FrameworkElement Target { get; set; }
        public ExpansionBehavior(FrameworkElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("Unable to create ExpansionBehavior object , null owner passed to the constructor");
            }
            element.SizeChanged += new SizeChangedEventHandler(element_SizeChanged);
        }
        void element_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!e.NewSize.IsEmpty)
            {			
                Target.Height =((e.NewSize.Height-1)>=0)? e.NewSize.Height:0;
                Target.Width = ((e.NewSize.Width-1)>=0)?e.NewSize.Width:0;
            }
        }
    }
    public static class GripBehaviors
    {
        public static FrameworkElement GetExpansionTarget(DependencyObject obj)
        {
            return (FrameworkElement)obj.GetValue(ExpansionTargetProperty);
        }

        public static void SetExpansionTarget(DependencyObject obj, FrameworkElement value)
        {
            obj.SetValue(ExpansionTargetProperty, value);
        }

        // Using a DependencyProperty as the backing store for ExpansionTarget.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExpansionTargetProperty =
            DependencyProperty.RegisterAttached("ExpansionTarget", typeof(FrameworkElement), typeof(GripBehaviors), new PropertyMetadata(ExpansionTargetPropertyChanged));

        private static void ExpansionTargetPropertyChanged(DependencyObject owner, DependencyPropertyChangedEventArgs args)
        {
            var element = owner as FrameworkElement;
            var behavior = new ExpansionBehavior(element);
            behavior.Target = args.NewValue as FrameworkElement;
        }
    }
}
