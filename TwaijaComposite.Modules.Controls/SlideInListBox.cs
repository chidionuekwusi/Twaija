using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows;
using System.Windows.Media;
using System.Threading;

namespace TwaijaComposite.Modules.Controls
{
    [TemplateVisualState(Name="MouseOver",GroupName="CommonStates")]
    [TemplateVisualState(Name = "Normal",GroupName = "CommonStates")]
    [TemplatePart(Name="PART_LayoutRoot",Type=typeof(FrameworkElement))]
   public class SlideInListBox:ListBox
    {
       public SlideInListBox()
        {
          
        }

        List<FrameworkElement> buffer = new List<FrameworkElement>();
        List<Storyboard> anims = new List<Storyboard>();


        public int NewMessageSize
        {
            get { return (int)GetValue(NewMessageSizeProperty); }
            set { SetValue(NewMessageSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NewMessageSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NewMessageSizeProperty =
            DependencyProperty.Register("NewMessageSize", typeof(int), typeof(SlideInListBox), new PropertyMetadata(0));

        
        private FrameworkElement _root;
        public override void OnApplyTemplate()
        {
           var root= this.GetTemplateChild("PART_LayoutRoot") as FrameworkElement;
           if (root != null)
           {
               root.MouseEnter += new System.Windows.Input.MouseEventHandler(root_MouseEnter);
               root.MouseLeave += new System.Windows.Input.MouseEventHandler(root_MouseLeave);
               _root = root;
           }
            base.OnApplyTemplate();
            VisualStateManager.GoToState(this, "Normal", false);
        }

        void root_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            VisualStateManager.GoToState(this, "Normal", false);
        }

        void root_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            VisualStateManager.GoToState(this, "MouseOver", false);
        }
       double delay=-0.05;
        int counter=0;
       protected override void PrepareContainerForItemOverride(System.Windows.DependencyObject element, object item)
        {

//            if (counter == NewMessageSize-1)
//            {
//                delay = -0.05;
//                counter = 0;
//            }
//            if (counter < NewMessageSize)
//            {
//                delay = delay + 0.05;
//            }
//            counter++;
//            var lb = (ListBoxItem)element;
//#if SILVERLIGHT
//            lb.Projection = new PlaneProjection() { RotationX = 0 };
          
//#endif
//            lb.RenderTransformOrigin = new Point(0.5, 0.5);
//            Storyboard board = new Storyboard();
//            DoubleAnimationUsingKeyFrames bd = new DoubleAnimationUsingKeyFrames();
//            DoubleAnimationUsingKeyFrames bd0 = new DoubleAnimationUsingKeyFrames();
//            DoubleAnimationUsingKeyFrames bd1 = new DoubleAnimationUsingKeyFrames();
//            DoubleAnimationUsingKeyFrames bd2 = new DoubleAnimationUsingKeyFrames();
//            DoubleAnimationUsingKeyFrames bd3 = new DoubleAnimationUsingKeyFrames();
          
//            var RenderTransform = new TranslateTransform();
//            var scaleTransform = new ScaleTransform();
        
//            TransformGroup group = new TransformGroup();
//          //group.Children.Add(RenderTransform);
//            group.Children.Add(scaleTransform);
//            lb.RenderTransform = group;
//            Storyboard.SetTarget(bd0, lb);
//            Storyboard.SetTarget(bd1, lb);
//            Storyboard.SetTarget(bd2, lb);
//            Storyboard.SetTarget(bd3, lb);
//            Storyboard.SetTargetProperty(bd1, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"));
//            Storyboard.SetTargetProperty(bd2, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
//#if SILVERLIGHT
//            Storyboard.SetTargetProperty(bd3, new PropertyPath("(UIElement.Projection).(PlaneProjection.RotationX)"));
//            bd3.KeyFrames.Add(new EasingDoubleKeyFrame() {Value=-65, KeyTime=KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)) });
//            bd3.KeyFrames.Add(new SplineDoubleKeyFrame() { Value = 0, KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.4 + delay)), KeySpline = new KeySpline() { ControlPoint1 = new Point(1, 0), ControlPoint2 = new Point(0,1) } });
//#endif
//            bd1.KeyFrames.Add(new EasingDoubleKeyFrame() { Value = 0, KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)) });
//            bd1.KeyFrames.Add(new SplineDoubleKeyFrame() { Value = 1, KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.4+delay)), KeySpline = new KeySpline() {ControlPoint1=new Point(0.5,0),ControlPoint2=new Point(0.5,1) } });
//            bd2.KeyFrames.Add(new EasingDoubleKeyFrame() { Value = 0, KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)) });
//            bd2.KeyFrames.Add(new SplineDoubleKeyFrame() { Value = 1, KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.4+delay)), KeySpline = new KeySpline() { ControlPoint1 = new Point(0.5, 0), ControlPoint2 = new Point(0.5,1) } });
//            bd.KeyFrames.Add(new EasingDoubleKeyFrame() { Value = 0, KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)) });
//         //   bd.KeyFrames.Add(new EasingDoubleKeyFrame() { Value = 1, KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.2+delay)) });
//            bd.KeyFrames.Add(new EasingDoubleKeyFrame() { Value = 1, KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.4+delay)) });
//         //   bd.KeyFrames.Add(new EasingDoubleKeyFrame() { Value = 1, KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.9+delay)) });
//           Storyboard.SetTarget(bd, lb);
//           Storyboard.SetTargetProperty(bd, new PropertyPath(OpacityProperty));
//           //bd0.KeyFrames.Add(new EasingDoubleKeyFrame(){Value=-25,KeyTime= KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))});
//           //bd0.KeyFrames.Add(new EasingDoubleKeyFrame(){Value=-25,KeyTime= KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))});
//           //bd0.KeyFrames.Add(new EasingDoubleKeyFrame() { Value = -25, KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.5)) });
//           //bd0.KeyFrames.Add(new SplineDoubleKeyFrame() { Value = 0, KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.9)), KeySpline = new KeySpline() { ControlPoint1 = new Point(0.5, 0), ControlPoint2 = new Point(0.5, 1) } });
//            board.Children.Add(bd);
//            board.Children.Add(bd1);
//            board.Children.Add(bd2);
//#if SILVERLIGHT
//           // board.Children.Add(bd3);
//#endif
//          // board.Begin();
           base.PrepareContainerForItemOverride(element, item);
       }

    }
}
