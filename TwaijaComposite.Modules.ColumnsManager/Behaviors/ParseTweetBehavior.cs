using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Windows.Documents;
using TwaijaComposite.Modules.Common.Commands.Factories;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Commands;
using System.Diagnostics;

namespace TwaijaComposite.Modules.ColumnsManager.Behaviors
{
   public class ParseTweetBehavior
    {
       [Dependency]
       public ICommandFactory factory { get; set; }
       string _screenName;
       public string ScreenName { get { return _screenName; }
           set
           {
               if (_screenName != value)
               {
                   _screenName = value;
                   Parse();
               }
               
           }
       }
       public bool MustSupplyScreenName { get; set; }
       private static readonly Regex UriMatchingRegex = new Regex(@"(?<url>[a-zA-Z]+:\/\/[a-zA-Z0-9]+([\-\.]{1}[a-zA-Z0-9]+)*\.[a-zA-Z]{2,5}(:[0-9]{1,5})?([a-zA-Z0-9_\-\.\~\%\+\?\=\&\;\|/]*)?)|(?<emailAddress>[^\s]+@[a-zA-Z0-9]+([\-\.]{1}[a-zA-Z0-9]+)*\.[a-zA-Z]{2,5})|(?<toTwitterScreenName>\@[a-zA-Z0-9\-_]+)|(?<HashTag>\#[a-zA-Z0-9\-_]+)", RegexOptions.CultureInvariant);
       
#if SILVERLIGHT
       public RichTextBox Owner
       {
           get;
           set;
       }
#else
         public TextBlock Owner
       {
           get;
           set;
       }
#endif
       private Dictionary<string, string> _linkDictionary;
       public Dictionary<string, string> LinkDictionary
       {
           get { return _linkDictionary; }
           set
           {
               if (_linkDictionary != value)
               {
                   _linkDictionary = value;
                   Parse();
               }
           }
       }
       

       private void Parse()
       {
           if (!string.IsNullOrEmpty(Text) &&  Owner!=null)
           {
               if (MustSupplyScreenName)
               {
                   if (string.IsNullOrEmpty(ScreenName))
                   {
                       return;
                   }
               }
               ParseText(Owner, Text);
           }
       }
       private string _text;
       public string Text
       {
           get { return _text; }
           set
           {
               if (_text != value)
               {
                   _text = value;
                   Parse();
               }
           }
       }
       public ParseTweetBehavior()
       {

       }
#if !SILVERLIGHT
        IEnumerable<Inline> GenerateInlinesFromRawEntryText(string entryText)
       {
           int startIndex = 0;
           Match match = UriMatchingRegex.Match(entryText);
           var foreground =  new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Orange);
           while (match.Success)
           {
               if (startIndex != match.Index)
               {
                   yield return new Run() { Text = DecodeStatusEntryText(entryText.Substring(startIndex, match.Index - startIndex)) };
               }
               Inline i = new Hyperlink();
               Hyperlink hyperLink = new Hyperlink(){  
                   TextDecorations = null, Foreground = foreground
               };
               hyperLink.Inlines.Add(new Run() {Text= match.Value });
               string uri = match.Value;

               if (match.Groups["emailAddress"].Success)
               {
                   uri = "mailto:" + uri;
               }

               if (match.Groups["toTwitterScreenName"].Success)
               {
                   hyperLink.Command = factory.CreateOpenTwitterProfileCommand(match.Value);
               }
               if (match.Groups["url"].Success)
               {
                   string link = string.Empty;
                   if (LinkDictionary != null)
                   {
                       if (LinkDictionary.ContainsKey(match.Value))
                       {
#if !SILVERLIGHT
                           link = LinkDictionary[match.Value];
#endif
                           /* Create the Parse Command
                       * hyperLink.Command=
                       * */
                       }
                   }
                   else
                   {
                       link = match.Value;
                   }
                   hyperLink.ToolTip = link;
                   hyperLink.CommandParameter =link;
                   hyperLink.Command = new DelegateCommand<object>((s) =>
                   {
                       Process.Start(s.ToString());

                   }, (a) => { return true; });
               }
               if (match.Groups["HashTag"].Success)
               {
                   hyperLink.Command = factory.CreateTweetSearchCommand(match.Value);
               }
               yield return hyperLink;

               startIndex = match.Index + match.Length;

               match = match.NextMatch();
           }
           if (MustSupplyScreenName)
           {
               
               if (Owner.Inlines.Count != 0)
               {
                   var run=new Run() { Text = ScreenName + " : ", Foreground = foreground};
#if SILVERLIGHT
                   Owner.Inlines.Insert(0, run);
#else
                   Owner.Inlines.InsertBefore(Owner.Inlines.FirstInline, run);
#endif
               }
               if (entryText.Length != 0 && Owner.Inlines.Count == 0)
               {
                   var txt = new Run() { Text = ScreenName + " : ", Foreground =foreground };
                   txt.TextDecorations = null;
                   Owner.Inlines.Add(txt);
               }
           }
           if (startIndex != entryText.Length)
           {
               var run = new Run();
               run.Text = DecodeStatusEntryText(entryText.Substring(startIndex));
               yield return run;
           }

        }
#else
        IEnumerable<Inline> GenerateInlinesFromRawEntryText(string entryText)
        {
            int startIndex = 0;
            Match match = UriMatchingRegex.Match(entryText);
            var foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255,50,180,100));
            var hiforeground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255,90,220,140));
            while (match.Success)
            {
                if (startIndex != match.Index)
                {
                    yield return new Run() { Text = DecodeStatusEntryText(entryText.Substring(startIndex, match.Index - startIndex)) };
                }
                Inline i = new Hyperlink();
                Hyperlink hyperLink = new Hyperlink()
                {
                    TextDecorations = null,
                    Foreground = foreground,
                    MouseOverForeground = hiforeground,MouseOverTextDecorations=null
                };
                hyperLink.Inlines.Add(new Run() { Text = match.Value });
                string uri = match.Value;

                if (match.Groups["emailAddress"].Success)
                {
                    uri = "mailto:" + uri;
                }

                if (match.Groups["toTwitterScreenName"].Success)
                {
                    hyperLink.Command = factory.CreateOpenTwitterProfileCommand(match.Value);
                }
                if (match.Groups["url"].Success)
                {
                    if (LinkDictionary != null)
                    {
                        if (LinkDictionary.ContainsKey(match.Value))
                        {
#if !SILVERLIGHT
                           hyperLink.ToolTip = LinkDictionary[match.Value];
#endif
                            /* Create the Parse Command
                       * hyperLink.Command=
                       * */
                        }
                    }
                }
                if (match.Groups["HashTag"].Success)
                {
                    hyperLink.Command = factory.CreateTweetSearchCommand(match.Value);
                }
                yield return hyperLink;

                startIndex = match.Index + match.Length;

                match = match.NextMatch();
            }
            if (MustSupplyScreenName)
            {

//                if (Owner.Inlines.Count != 0)
//                {
//                    var run = new Run() { Text = ScreenName + " : ", Foreground = foreground };
//#if SILVERLIGHT
//                    Owner.Inlines.Insert(0, run);
//#else
//                   Owner.Inlines.InsertBefore(Owner.Inlines.FirstInline, run);
//#endif
//                }
//                if (entryText.Length != 0 && Owner.Inlines.Count == 0)
//                {
//                    var txt = new Run() { Text = ScreenName + " : ", Foreground = foreground };
//                    txt.TextDecorations = null;
//                    Owner.Inlines.Add(txt);
//                }
            }
            if (startIndex != entryText.Length)
            {
                var run = new Run();
                run.Text = DecodeStatusEntryText(entryText.Substring(startIndex));
                yield return run;
            }

        }
#endif
        string DecodeStatusEntryText(string text)
       {
           return text.Replace("&gt;", ">").Replace("&lt;", "<");
       }
#if !SILVERLIGHT
       void ParseText(TextBlock textblock,string text)
#else
        void ParseText(RichTextBox textblock, string text)
#endif
       {
          
#if !SILVERLIGHT
           textblock.Inlines.Clear();
           textblock.Inlines.AddRange(GenerateInlinesFromRawEntryText(text)); 

#else
           textblock.Blocks.Clear();
           var para = new Paragraph() {TextAlignment = System.Windows.TextAlignment.Left };
           //para.SetValue(System.Windows.Media.TextOptions.TextRenderingModeProperty, System.Windows.Media.TextRenderingMode.Aliased);
           textblock.Blocks.Add(para);
           foreach (Inline inline in GenerateInlinesFromRawEntryText(text))
           {
              (textblock.Blocks[0] as Paragraph).Inlines.Add(inline);
           }
#endif
       }
    }
}
