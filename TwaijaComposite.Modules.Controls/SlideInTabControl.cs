using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows;
using System.Windows.Media;

namespace TwaijaComposite.Modules.Controls
{
    public class SlideInTabControl:TabControl
    {
        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            base.ClearContainerForItemOverride(element, item);
        }
        protected override DependencyObject GetContainerForItemOverride()
        {
            return base.GetContainerForItemOverride();
        }
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return base.IsItemItsOwnContainerOverride(item);
        }
        protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {          
            base.OnItemsChanged(e);
        }
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {

            base.PrepareContainerForItemOverride(element, item);
        }

        public List<Storyboard> TransitionAnimations { get; set; }
        public Storyboard TransitionAnimation
        {
            get { return (Storyboard)GetValue(TransitionAnimationProperty); }
            set { SetValue(TransitionAnimationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TransitionAnimation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TransitionAnimationProperty =
            DependencyProperty.Register("TransitionAnimation", typeof(Storyboard), typeof(SlideInTabControl), new PropertyMetadata(null));

        
        public SlideInTabControl()
        {
            TransitionAnimations = new List<Storyboard>();
            base.SelectionChanged += new SelectionChangedEventHandler(SlideInTabControl_SelectionChanged);
         
        }
        Random gen = new Random();
        void SlideInTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
#if !SILVERLIGHT
            if (this.Equals(e.OriginalSource))
            {
                if (TransitionAnimation != null)
                {
                    TransitionAnimation.Begin();
                }
            }
#else
               if (TransitionAnimations.Count>0)
                {
                    TransitionAnimations[gen.Next(TransitionAnimations.Count)].Begin();
                }
#endif
        }

        public override void OnApplyTemplate()
        {
            try
            {
                var lb = GetTemplateChild("AnimationContentPresenter") as Grid;
                var scaleTransform = new ScaleTransform();
                TransformGroup group = new TransformGroup();
                var translateTransform = new TranslateTransform();
                group.Children.Add(scaleTransform);
                group.Children.Add(translateTransform);
                 lb.RenderTransform = group;
                lb.RenderTransformOrigin = new Point(0.5, 0.5);

                DoubleAnimationUsingKeyFrames opacityTimeline1 = new DoubleAnimationUsingKeyFrames();
                opacityTimeline1.KeyFrames.Add(new EasingDoubleKeyFrame() { Value = 0, KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)) });
                opacityTimeline1.KeyFrames.Add(new EasingDoubleKeyFrame() { Value = 1, KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.15)) });
                Storyboard.SetTarget(opacityTimeline1, lb);
                Storyboard.SetTargetProperty(opacityTimeline1, new PropertyPath("Opacity"));

                DoubleAnimationUsingKeyFrames opacityTimeline = new DoubleAnimationUsingKeyFrames();
                opacityTimeline.KeyFrames.Add(new EasingDoubleKeyFrame() { Value=0, KeyTime=KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))});
                opacityTimeline.KeyFrames.Add(new EasingDoubleKeyFrame() { Value=1,KeyTime=KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.15))});
                Storyboard.SetTarget(opacityTimeline, lb);
                Storyboard.SetTargetProperty(opacityTimeline, new PropertyPath("Opacity"));
   #if SILVERLIGHT
                 var silverlightboard = new Storyboard();
                 var bounce = new Storyboard();
                 lb.Projection = new PlaneProjection() { RotationX = 0 };
                 DoubleAnimationUsingKeyFrames bd3 = new DoubleAnimationUsingKeyFrames();
                 DoubleAnimationUsingKeyFrames bounceTimeline = new DoubleAnimationUsingKeyFrames();
                 Storyboard.SetTarget(bd3, lb);
                 Storyboard.SetTarget(bounceTimeline, lb);
                 Storyboard.SetTargetProperty(bounceTimeline, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(TranslateTransform.Y)"));
                 Storyboard.SetTargetProperty(bd3, new PropertyPath("(UIElement.Projection).(PlaneProjection.RotationX)"));
                 bd3.KeyFrames.Add(new EasingDoubleKeyFrame() { Value = -65, KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)) });
                 bd3.KeyFrames.Add(new SplineDoubleKeyFrame() { Value = 0, KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.4)), KeySpline = new KeySpline() { ControlPoint1 = new Point(0, 1.1), ControlPoint2 = new Point(0.5, 1) } });
                 bounceTimeline.KeyFrames.Add(new EasingDoubleKeyFrame() {Value=0,KeyTime=KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)) });
                 bounceTimeline.KeyFrames.Add(new SplineDoubleKeyFrame() { Value = -5, KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.2)), KeySpline = new KeySpline() {  ControlPoint1=new Point(0.49,0),ControlPoint2=new Point(0.49,1)}  });
                 bounceTimeline.KeyFrames.Add(new EasingDoubleKeyFrame() {Value = 0, KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.467)), EasingFunction = new ElasticEase() { EasingMode= EasingMode.EaseOut }});
                 silverlightboard.Children.Add(bd3);
                 silverlightboard.Children.Add(opacityTimeline);
                 bounce.Children.Add(bounceTimeline);
                 bounce.Children.Add(opacityTimeline1);
                 TransitionAnimations.Add(silverlightboard);
                 //TransitionAnimations.Add(bounce);
#endif
                Storyboard board = new Storyboard();
                DoubleAnimationUsingKeyFrames bd1 = new DoubleAnimationUsingKeyFrames();
                DoubleAnimationUsingKeyFrames bd2 = new DoubleAnimationUsingKeyFrames();
                Storyboard.SetTarget(bd1, lb);
                Storyboard.SetTarget(bd2, lb);
                Storyboard.SetTargetProperty(bd1, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"));
                Storyboard.SetTargetProperty(bd2, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
                bd1.KeyFrames.Add(new EasingDoubleKeyFrame() { Value = 0, KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)) });
                bd1.KeyFrames.Add(new SplineDoubleKeyFrame() { Value = 1, KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.4 )), KeySpline = new KeySpline() { ControlPoint1 = new Point(0.5, 0), ControlPoint2 = new Point(0.5, 1) } });
                bd2.KeyFrames.Add(new EasingDoubleKeyFrame() { Value = 0, KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)) });
                bd2.KeyFrames.Add(new SplineDoubleKeyFrame() { Value = 1, KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.4 )), KeySpline = new KeySpline() { ControlPoint1 = new Point(0.5, 0), ControlPoint2 = new Point(0.5, 1) } });             
                board.Children.Add(bd1);
                board.Children.Add(bd2);
                //board.Children.Add(opacityTimeline);
                //TransitionAnimations.Add(board);
                TransitionAnimation = board;
            }
            catch
            {
            }
            base.OnApplyTemplate();

        }
    }
}
