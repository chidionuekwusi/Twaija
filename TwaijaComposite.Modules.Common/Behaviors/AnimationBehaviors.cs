using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Controls.Primitives;

namespace TwaijaComposite.Modules.Common.Behaviors
{
    public static class AnimationBehaviors
    {
        public static string GetStoryboardB(DependencyObject obj)
        {
            return (string)obj.GetValue(StoryboardBProperty);
        }

        public static void SetStoryboardB(DependencyObject obj, string value)
        {
            obj.SetValue(StoryboardBProperty, value);
        }

        // Using a DependencyProperty as the backing store for StoryboardB.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StoryboardBProperty =
            DependencyProperty.RegisterAttached("StoryboardB", typeof(string), typeof(AnimationBehaviors), new PropertyMetadata(StoryboardBPropertyChanged));

        
        public static string GetStoryboardA(DependencyObject obj)
        {
            return (string)obj.GetValue(StoryboardAProperty);
        }

        public static void SetStoryboardA(DependencyObject obj, string value)
        {
            obj.SetValue(StoryboardAProperty, value);
        }

        // Using a DependencyProperty as the backing store for StoryboardA.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StoryboardAProperty =
            DependencyProperty.RegisterAttached("StoryboardA", typeof(string), typeof(AnimationBehaviors), new PropertyMetadata(StoryboardAPropertyChanged));

        
        public static FlipFlopAnimationBehavior GetFlipFlopAnimationBehavior(DependencyObject obj)
        {
            return (FlipFlopAnimationBehavior)obj.GetValue(FlipFlopAnimationBehaviorProperty);
        }

        public static void SetFlipFlopAnimationBehavior(DependencyObject obj, FlipFlopAnimationBehavior value)
        {
            obj.SetValue(FlipFlopAnimationBehaviorProperty, value);
        }

        // Using a DependencyProperty as the backing store for FlipFlopAnimationBehavior.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty FlipFlopAnimationBehaviorProperty =
            DependencyProperty.RegisterAttached("FlipFlopAnimationBehavior", typeof(FlipFlopAnimationBehavior), typeof(AnimationBehaviors), new PropertyMetadata(null));

        
        public static AnimationTrigger GetActionTrigger(DependencyObject obj)
        {
            return (AnimationTrigger)obj.GetValue(ActionTriggerProperty);
        }

        public static void SetActionTrigger(DependencyObject obj, AnimationTrigger value)
        {
            obj.SetValue(ActionTriggerProperty, value);
        }

        // Using a DependencyProperty as the backing store for ActionTrigger.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActionTriggerProperty =
            DependencyProperty.RegisterAttached("ActionTrigger", typeof(AnimationTrigger), typeof(AnimationBehaviors), new PropertyMetadata(null,AnimationTriggerPropertyChanged));

        
        public static TriggerAnimationBehavior GetTriggerAnimationBehavior(DependencyObject obj)
        {
            return (TriggerAnimationBehavior)obj.GetValue(TriggerAnimationBehaviorProperty);
        }

        public static void SetTriggerAnimationBehavior(DependencyObject obj, TriggerAnimationBehavior value)
        {
            obj.SetValue(TriggerAnimationBehaviorProperty, value);
        }

        // Using a DependencyProperty as the backing store for TriggerAnimationBehavior.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty TriggerAnimationBehaviorProperty =
            DependencyProperty.RegisterAttached("TriggerAnimationBehavior", typeof(TriggerAnimationBehavior), typeof(AnimationBehaviors), new PropertyMetadata(null));
        
        
        public static string GetAnimationTriggerStoryboardName(DependencyObject obj)
        {
            return (string)obj.GetValue(AnimationTriggerStoryboardNameProperty);
        }

        public static void SetAnimationTriggerStoryboardName(DependencyObject obj, string value)
        {
            obj.SetValue(AnimationTriggerStoryboardNameProperty, value);
        }

        // Using a DependencyProperty as the backing store for AnimationTriggerStoryboardName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimationTriggerStoryboardNameProperty =
            DependencyProperty.RegisterAttached("AnimationTriggerStoryboardName", typeof(string), typeof(AnimationBehaviors), new PropertyMetadata(AnimationStoryboardNamePropertyChanged));

        private static TriggerAnimationBehavior GetOrCreateBehavior(DependencyObject element)
        {
            TriggerAnimationBehavior behavior = element.GetValue(TriggerAnimationBehaviorProperty) as TriggerAnimationBehavior;
            if (behavior == null)
            {
                behavior = new TriggerAnimationBehavior(element as FrameworkElement);
                element.SetValue(TriggerAnimationBehaviorProperty, behavior);
            }
            return behavior;
        }
        public static void AnimationStoryboardNamePropertyChanged(DependencyObject owner, DependencyPropertyChangedEventArgs e)
        {
            var name = e.NewValue as string;
            if (!string.IsNullOrEmpty(name))
            {
                var behavior=GetOrCreateBehavior(owner);
                behavior.StoryboardName = name;
            }
        }
        public static void AnimationTriggerPropertyChanged(DependencyObject owner, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                (e.NewValue as AnimationTrigger).Trigger = GetOrCreateBehavior(owner).Trigger;
            }
        }
        public static void StoryboardAPropertyChanged(DependencyObject owner, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                var name = e.NewValue as string;
                GetOrCreateFlipFlopAnimation(owner).Storyboard1 = name;
            }
        }
        public static void StoryboardBPropertyChanged(DependencyObject owner, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                var name = e.NewValue as string;
                GetOrCreateFlipFlopAnimation(owner).Storyboard2 = name;
            }
        }
        private static FlipFlopAnimationBehavior GetOrCreateFlipFlopAnimation(DependencyObject element)
        {
            FlipFlopAnimationBehavior behavior = element.GetValue(FlipFlopAnimationBehaviorProperty) as FlipFlopAnimationBehavior;
            if (behavior == null)
            {
                behavior = new FlipFlopAnimationBehavior(element as ButtonBase);
                element.SetValue(FlipFlopAnimationBehaviorProperty, behavior);
            }
            return behavior;
        }
    }
    public class AnimationTrigger
    {
        public AnimationTrigger()
        {
            //So that it doesnt throw an exception if no Trigger has been attached.
            Trigger = () => { };
        }
       public Action Trigger { get; set; }
    }
    public class FlipFlopAnimationBehavior
    {
        private ButtonBase Target { get; set; }
        public string Storyboard1 { get; set; }
        public string Storyboard2 { get; set; }
        Storyboard _st1;
        Storyboard _st2;
        bool flip;
        public FlipFlopAnimationBehavior(ButtonBase button)
        {
            button.Click += new RoutedEventHandler(button_Click);
            Target = button;
        }

        void button_Click(object sender, RoutedEventArgs e)
        {
            if (flip)
            {
                flip = false;
                Trigger(_st1, Storyboard1);
                return;
            }
            Trigger(_st2, Storyboard2);
            flip = true;
        }
        void Trigger(Storyboard st, string name)
        {
            if (st == null)
            {
#if !SILVERLIGHT
                st = Target.FindResource(name) as Storyboard;
#else
                if (Target.Resources.Contains(name))
                {
                    st = Target.Resources[name] as Storyboard;
                }
#endif
            }
            if (st!= null)
            {
                st.Begin();
            }
        }
        
    }
    public class TriggerAnimationBehavior
    {
        private FrameworkElement Target { get; set; }
        public Action Trigger { get; set; }
        public string StoryboardName { get; set; }
        Storyboard _st;
        public TriggerAnimationBehavior(FrameworkElement element)
        {
            Trigger = TriggerAnimation;
            Target = element;
        }
        public void TriggerAnimation()
        {
            if (_st == null)
            {
#if !SILVERLIGHT
                _st = Target.FindResource(StoryboardName) as Storyboard;
#else
                 if (Target.Resources.Contains(StoryboardName))
                {
                    _st = Target.Resources[StoryboardName] as Storyboard;
                }
#endif
            }
            if (_st != null)
            {
                _st.Begin();
            }
        }
    }
}
