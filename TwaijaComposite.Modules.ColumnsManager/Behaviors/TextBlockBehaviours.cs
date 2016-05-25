using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.ServiceLocation;

namespace TwaijaComposite.Modules.ColumnsManager.Behaviors
{
#if SILVERLIGHT
    public class TextBlockBehaviours
    {

        public static string GetScreenName(DependencyObject obj)
        {
            return (string)obj.GetValue(ScreenNameProperty);
        }

        public static void SetScreenName(DependencyObject obj, string value)
        {
            obj.SetValue(ScreenNameProperty, value);
        }

        // Using a DependencyProperty as the backing store for ScreeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScreenNameProperty =
            DependencyProperty.RegisterAttached("ScreenName", typeof(string), typeof(TextBlockBehaviours), new PropertyMetadata(ScreenNamePropertyChanged));

        


        public static ParseTweetBehavior GetParseTweetBehavior(DependencyObject obj)
        {
            return (ParseTweetBehavior)obj.GetValue(ParseTweetBehaviorProperty);
        }

        public static void SetParseTweetBehavior(DependencyObject obj, ParseTweetBehavior value)
        {
            obj.SetValue(ParseTweetBehaviorProperty, value);
        }

        // Using a DependencyProperty as the backing store for ParseTweetBehavior.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty ParseTweetBehaviorProperty =
            DependencyProperty.RegisterAttached("ParseTweetBehavior", typeof(ParseTweetBehavior), typeof(TextBlockBehaviours), new PropertyMetadata(null));

        

        public static string GetTweetText(DependencyObject obj)
        {
            return (string)obj.GetValue(TweetTextProperty);
        }

        public static void SetTweetText(DependencyObject obj, string value)
        {
            obj.SetValue(TweetTextProperty, value);
        }

        // Using a DependencyProperty as the backing store for TweetText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TweetTextProperty =
            DependencyProperty.RegisterAttached("TweetText", typeof(string), typeof(TextBlockBehaviours), new PropertyMetadata(TweetTextPropertyChanged));

        

        public static Dictionary<string,string> GetLinkDictionary(DependencyObject obj)
        {
            return (Dictionary<string,string>)obj.GetValue(LinkDictionaryProperty);
        }

        public static void SetLinkDictionary(DependencyObject obj, Dictionary<string,string> value)
        {
            obj.SetValue(LinkDictionaryProperty, value);
        }

        // Using a DependencyProperty as the backing store for LinkDictionary.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinkDictionaryProperty =
            DependencyProperty.RegisterAttached("LinkDictionary", typeof(Dictionary<string, string>), typeof(TextBlockBehaviours), new PropertyMetadata(LinkDictionaryPropertyChanged));


        public static void LinkDictionaryPropertyChanged(DependencyObject owner, DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue != null)
            {
                var textblock = owner as RichTextBox;
                var behavior = GetOrCreateParseTweetBehaviour(textblock);
                behavior.LinkDictionary = args.NewValue as Dictionary<string, string>;
            }
            
        }
        public static void TweetTextPropertyChanged(DependencyObject owner, DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue != null)
            {
                var textblock = owner as RichTextBox;
                var behavior = GetOrCreateParseTweetBehaviour(textblock);
                behavior.Text = args.NewValue.ToString();
            }
        }
        public static void ScreenNamePropertyChanged(DependencyObject owner, DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue != null)
            {
                var screenName=args.NewValue.ToString();
                var textblock = owner as RichTextBox;
                var behavior = GetOrCreateParseTweetBehaviour(textblock);
                behavior.MustSupplyScreenName = true;
                behavior.ScreenName = screenName;
                
            }
        }
        static ParseTweetBehavior GetOrCreateParseTweetBehaviour(RichTextBox owner)
        {
            var behavior = owner.GetValue(ParseTweetBehaviorProperty) as ParseTweetBehavior;
            if (behavior == null)
            {
                behavior = ServiceLocator.Current.GetInstance<ParseTweetBehavior>();
                behavior.Owner = owner;               
                owner.SetValue(ParseTweetBehaviorProperty, behavior);
            }
            return behavior;
        }
    }
#else
     public class TextBlockBehaviours
    {

        public static string GetScreenName(DependencyObject obj)
        {
            return (string)obj.GetValue(ScreenNameProperty);
        }

        public static void SetScreenName(DependencyObject obj, string value)
        {
            obj.SetValue(ScreenNameProperty, value);
        }

        // Using a DependencyProperty as the backing store for ScreeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScreenNameProperty =
            DependencyProperty.RegisterAttached("ScreenName", typeof(string), typeof(TextBlockBehaviours), new PropertyMetadata(ScreenNamePropertyChanged));

        


        public static ParseTweetBehavior GetParseTweetBehavior(DependencyObject obj)
        {
            return (ParseTweetBehavior)obj.GetValue(ParseTweetBehaviorProperty);
        }

        public static void SetParseTweetBehavior(DependencyObject obj, ParseTweetBehavior value)
        {
            obj.SetValue(ParseTweetBehaviorProperty, value);
        }

        // Using a DependencyProperty as the backing store for ParseTweetBehavior.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty ParseTweetBehaviorProperty =
            DependencyProperty.RegisterAttached("ParseTweetBehavior", typeof(ParseTweetBehavior), typeof(TextBlockBehaviours), new PropertyMetadata(null));

        

        public static string GetTweetText(DependencyObject obj)
        {
            return (string)obj.GetValue(TweetTextProperty);
        }

        public static void SetTweetText(DependencyObject obj, string value)
        {
            obj.SetValue(TweetTextProperty, value);
        }

        // Using a DependencyProperty as the backing store for TweetText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TweetTextProperty =
            DependencyProperty.RegisterAttached("TweetText", typeof(string), typeof(TextBlockBehaviours), new PropertyMetadata(TweetTextPropertyChanged));

        

        public static Dictionary<string,string> GetLinkDictionary(DependencyObject obj)
        {
            return (Dictionary<string,string>)obj.GetValue(LinkDictionaryProperty);
        }

        public static void SetLinkDictionary(DependencyObject obj, Dictionary<string,string> value)
        {
            obj.SetValue(LinkDictionaryProperty, value);
        }

        // Using a DependencyProperty as the backing store for LinkDictionary.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinkDictionaryProperty =
            DependencyProperty.RegisterAttached("LinkDictionary", typeof(Dictionary<string, string>), typeof(TextBlockBehaviours), new PropertyMetadata(LinkDictionaryPropertyChanged));


        public static void LinkDictionaryPropertyChanged(DependencyObject owner, DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue != null)
            {
                var textblock = owner as TextBlock;
                var behavior = GetOrCreateParseTweetBehaviour(textblock);
                behavior.LinkDictionary = args.NewValue as Dictionary<string, string>;
            }
            
        }
        public static void TweetTextPropertyChanged(DependencyObject owner, DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue != null)
            {
                var textblock = owner as TextBlock;
                var behavior = GetOrCreateParseTweetBehaviour(textblock);
                behavior.Text = args.NewValue.ToString();
            }
        }
        public static void ScreenNamePropertyChanged(DependencyObject owner, DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue != null)
            {
                var screenName=args.NewValue.ToString();
                var textblock = owner as TextBlock;
                var behavior = GetOrCreateParseTweetBehaviour(textblock);
                behavior.MustSupplyScreenName = true;
                behavior.ScreenName = screenName;
                
            }
        }
        static ParseTweetBehavior GetOrCreateParseTweetBehaviour(TextBlock owner)
        {
            var behavior = owner.GetValue(ParseTweetBehaviorProperty) as ParseTweetBehavior;
            if (behavior == null)
            {
                behavior = ServiceLocator.Current.GetInstance<ParseTweetBehavior>();
                behavior.Owner = owner;               
                owner.SetValue(ParseTweetBehaviorProperty, behavior);
            }
            return behavior;
        }
    }
#endif
}
