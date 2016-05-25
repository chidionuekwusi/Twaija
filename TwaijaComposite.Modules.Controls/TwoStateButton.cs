using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace TwaijaComposite.Modules.Controls
{
    
    [TemplateVisualState(Name="StateA",GroupName="Looks")]
    [TemplateVisualState(Name = "StateB", GroupName = "Looks")]
    public class TwoStateButton:Button
    {
        public const string StateBasedMode = "StateBased";


        public bool IsStateBased
        {
            get { return (bool)GetValue(IsStateBasedProperty); }
            set { SetValue(IsStateBasedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsStateBased.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsStateBasedProperty =
            DependencyProperty.Register("IsStateBased", typeof(bool?), typeof(TwoStateButton), new PropertyMetadata(false));

        
        public bool FlipFlop
        {
            get { return (bool)GetValue(FlipFlopProperty); }
            set { SetValue(FlipFlopProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FlipFlop.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FlipFlopProperty =
            DependencyProperty.Register("FlipFlop", typeof(bool), typeof(TwoStateButton), new PropertyMetadata(true,FlipFLopPropertyChangedHandler));

        protected static void FlipFLopPropertyChangedHandler(DependencyObject owner, DependencyPropertyChangedEventArgs args)
        {
            var button = owner as TwoStateButton;
            if (button.IsStateBased)
            {
                button.Flip((bool)args.NewValue);
            }
        }
        bool FlipFlag = true;
        public TwoStateButton()
        {
            this.Click += new RoutedEventHandler(TwoStateButton_Click);
        }

        void TwoStateButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsStateBased)
            {
                this.Click -= TwoStateButton_Click;
                return;
            }
            if (FlipFlag)
            {
                BeginTransition("StateA");
                FlipFlag = false;
                return;
            }
            BeginTransition("StateB");
            FlipFlag = true;
            return;

        }
        public void Flip(bool flipstate)
        {
            if (flipstate)
            {
                BeginTransition("StateB");
                return;
            }
            BeginTransition("StateA");
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var test=VisualStateManager.GoToState(this, "StateB", false);
        }
        void BeginTransition(string name)
        {
            VisualStateManager.GoToState(this, name, true);
        }
    }
}
