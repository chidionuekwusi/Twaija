using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using TwaijaComposite.Modules.Common.DialogTypes;
//using Microsoft.Practices.Composite.Presentation.Regions;
//using Microsoft.Practices.Composite.Presentation.Regions.Behaviors;
using System.Collections.Specialized;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.Common.Services;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Regions.Behaviors;

namespace TwaijaComposite.Modules.Common.Behaviors
{
   
    public interface INotificationWindow : IWindow
    {
        void Show(Position popupposition);
    }
    public class NotificationActivationBehavior : RegionBehavior, IHostAwareRegionBehavior
    {
        public DependencyProperty StyleLocator { get; set; }
        [Dependency]
        public Preferencing.Preferences pref { get; set; }
        public const string BehaviorKey = "NotificationBehavior";
        private IClosableContent closable = null;
        protected INotificationWindow CreateWindow()
        {
            return new NotificationsWindowWrapper();
        }

        private INotificationWindow contentDialog;


        /// <summary>
        /// Gets or sets the <see cref="DependencyObject"/> that the <see cref="IRegion"/> is attached to.
        /// </summary>
        /// <value>A <see cref="DependencyObject"/> that the <see cref="IRegion"/> is attached to.
        /// This is usually a <see cref="FrameworkElement"/> that is part of the tree.</value>
        public DependencyObject HostControl { get; set; }

        /// <summary>
        /// Performs the logic after the behavior has been attached.
        /// </summary>
        protected override void OnAttach()
        {
            this.Region.ActiveViews.CollectionChanged += this.ActiveViews_CollectionChanged;
        }

        private void ActiveViews_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                this.CloseContentDialog();
                this.PrepareContentDialog(e.NewItems[0]);
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                this.CloseContentDialog();
            }
        }

        private Style GetStyleForView()
        {
            if(StyleLocator!=null)
            return this.HostControl.GetValue(StyleLocator) as Style;

            return this.HostControl.GetValue(RegionPopupBehaviors.NotificationWindowStyleProperty) as Style;
        }

        private void PrepareContentDialog(object view)
        {

            this.contentDialog = this.CreateWindow();
            var style = this.GetStyleForView();
            if (style != null)
            {
                this.contentDialog.Style = style;
            }
            var frame = view as FrameworkElement;
            if (frame != null)
            {
                closable = frame.DataContext as IClosableContent;
            }
            var rwp = view as IRequestWindowPositionEntity;
            Position current = Position.TopLeft;
            if (rwp != null)
            {
                current = rwp.SuggestedPosition;
            }
            this.contentDialog.Content = view;
            this.contentDialog.Owner = this.HostControl;
            this.contentDialog.Closed += this.ContentDialogClosed;
            this.contentDialog.Show(current);
        }

        private void CloseContentDialog()
        {
            if (this.contentDialog != null)
            {
                this.contentDialog.Closed -= this.ContentDialogClosed;
                this.contentDialog.Close();
                this.contentDialog.Content = null;
                this.contentDialog.Owner = null;
                closable = null;
            }
        }

        protected virtual void ContentDialogClosed(object sender, System.EventArgs e)
        {          
            if (closable != null)
            {
                closable.TriggerContentClosure();
            }
            else
            {
                this.Region.Deactivate(this.contentDialog.Content);
                this.CloseContentDialog();
            }
        }
    }
    public class NotificationsWindowWrapper : INotificationWindow
    {
#if !SILVERLIGHT
        private NotificationsWindow window = null;
#else
        private NotificationzWindow window = null;
#endif
        public NotificationsWindowWrapper()
        {
#if !SILVERLIGHT
            window = new NotificationsWindow();
#else
            window = new NotificationzWindow();
#endif
            window.Height = 200;
            window.Width=350;
        }

#if SILVERLIGHT
  
#endif
#if !SILVERLIGHT
        public event EventHandler Closed
        {
            add { window.Closed += value; }
            remove
            {
                window.Closed -= value;
            }
        }
#else
        public event EventHandler Closed
        {
            add { window.Closed += value; }
            remove
            {
                window.Closed -= value;
            }
        }
      
#endif
#if !SILVERLIGHT
        public object Content
        {
            get
            {
                return window.Content;
            }
            set
            {
                window.Content = value as FrameworkElement;
            }
        }
#else
        public object Content
        {
            get
            {
                return window.MainContent;
            }
            set
            {
                window.MainContent = value;
            }
        }
#endif
        public Style Style
        {
            get
            {
#if !SILVERLIGHT
                return window.Style;
#else
                return null;
#endif
            }
            set
            {
#if !SILVERLIGHT
                window.Style=value;
#endif
            }
        }

        public void Show()
        {
            window.Show();
        }

        public void Close()
        {
            window.Close();
        }

#if !SILVERLIGHT
        public object Owner
        {
            get
            { if(window!=null)
                return window.Owner;

            return null;
            }
            set
            {
                if (window != null)
                {
                    window.Owner = value as Window;
                }
            }
        }
#else
        public object Owner
        {
            get;
            set;
        }
#endif
        public void Show(Position popupposition)
        {
#if !SILVERLIGHT  
            switch (popupposition)
            {
 
                case Position.TopLeft:
                    window.Top = 0.0;
                    window.Left = 0.0;
                    window.Show();
                    break;
                case Position.TopRight:
                    window.Left = System.Windows.SystemParameters.WorkArea.Width - window.Width;
                    window.Top = 0.0;
                    window.Show();
                    break;
                case Position.BottomLeft:
                    window.Top = System.Windows.SystemParameters.WorkArea.Height - window.Height;
                    window.Left = 0.0;
                    window.Show();
                    break;
                case Position.BottomRight:
                    window.Top = System.Windows.SystemParameters.WorkArea.Height - window.Height;
                    window.Left = System.Windows.SystemParameters.WorkArea.Width - window.Width;
                    window.Show();
                    break;
            }
#else
            switch (popupposition)
            {
                case Position.TopLeft:
                    window.Top = 0.0;
                    window.Left = 0.0;
                    window.Show();
                    break;
                case Position.TopRight:
                    window.Left = WindowSize.Width - window.Width-20;
                    window.Top = 0.0;
                    window.Show();
                    break;
                case Position.BottomLeft:
                    window.Top = WindowSize.Height - window.Height-20;
                    window.Left = 0.0;
                    window.Show();
                    break;
                case Position.BottomRight:
                    window.Top = WindowSize.Height - window.Height-20;
                    window.Left = WindowSize.Width - window.Width-20;
                    window.Show();
                    break;
            }
#endif
        }
    }
#if SILVERLIGHT
    public class NotificationzWindow:Window
    {
        public bool LegitExit { get; set; }
        private ContentControl holder = new ContentControl() { HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch, HorizontalContentAlignment = HorizontalAlignment.Stretch, VerticalContentAlignment = VerticalAlignment.Stretch };
        public object MainContent
        {
            get
            {
                return holder.Content;
            }
            set
            {
                holder.Content = value;
            }
        }
        public new void Show()
        {
            timer.Start();
            base.Show();
        }
        public new void Close()
        {
            LegitExit = true;
            if (Closed != null)
            {
                Closed(this, new EventArgs());
            }
            holder.MouseEnter -= this.NotificationsWindow_MouseEnter;
            holder.MouseLeave -= this.NotificationsWindow_MouseLeave;
            base.Close();
        }
        public event EventHandler Closed;
        DispatcherTimer timer;
        bool CanExit = true;
        public NotificationzWindow()
        {
            this.TopMost = true;
            base.Content = holder;
            this.WindowStyle = WindowStyle.None;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(40);
            timer.Tick += Tick;
            this.Closing += this.window_Closing;
            holder.MouseEnter += new System.Windows.Input.MouseEventHandler(NotificationsWindow_MouseEnter);
            holder.MouseLeave += new System.Windows.Input.MouseEventHandler(NotificationsWindow_MouseLeave);
        }
        void NotificationsWindow_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            CanExit = true;
        }

        void NotificationsWindow_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            CanExit = false;
        }
        public void Tick(object sender, EventArgs args)
        {
            if (CanExit)
            {
                timer.Tick -= Tick;
                timer.Stop();
                this.Close();
            }
        }
        void window_Closing(object sender, System.ComponentModel.ClosingEventArgs e)
        {
            if (!LegitExit)
            {
                e.Cancel = true;
            }
        }
    }
#endif
}
