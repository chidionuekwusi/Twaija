using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Media;

namespace TwaijaComposite.Modules.Controls
{
    [TemplatePart(Name = "PART_TimePlaceHolder", Type = typeof(TextBlock))]
    public class TimerEnabledUserControl : ContentControl
    {

        static TimerEnabledUserControl()
        {
            DateStringFormatProperty =
            DependencyProperty.Register("DateStringFormat", typeof(string), typeof(TimerEnabledUserControl), new PropertyMetadata("{0}"));

            InitialDateProperty =
            DependencyProperty.Register("InitialDate", typeof(DateTime), typeof(TimerEnabledUserControl), new PropertyMetadata(InitialDatePropertyChanged));
#if !SILVERLIGHT
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TimerEnabledUserControl), new FrameworkPropertyMetadata(typeof(TimerEnabledUserControl)));
#endif
        }
        private DispatcherTimer timer;
        public string DateStringFormat
        {
            get { return (string)GetValue(DateStringFormatProperty); }
            set { SetValue(DateStringFormatProperty, value); }
        }




        

        
        private TextBlock TimePlaceHolder { get; set; }
        // Using a DependencyProperty as the backing store for DateStringFormat.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateStringFormatProperty;

        public DateTime InitialDate
        {
            get { return (DateTime)GetValue(InitialDateProperty); }
            set { SetValue(InitialDateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InitialDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InitialDateProperty;
        public static void InitialDatePropertyChanged(DependencyObject owner, DependencyPropertyChangedEventArgs args)
        {

        }
        public TimerEnabledUserControl()
        {
#if SILVERLIGHT
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMinutes(5);
            timer.Tick += Tick;
#else
            timer = new DispatcherTimer(TimeSpan.FromMinutes(1), DispatcherPriority.Background, Tick, Application.Current.Dispatcher);

#endif
            Loaded += new RoutedEventHandler(TimerEnabledUserControl_Loaded);
            Unloaded += new RoutedEventHandler(TimerEnabledUserControl_Unloaded);
        }

        void TimerEnabledUserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        void TimerEnabledUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var stamp = CreateTimeStamp();
            UpdateTimer(stamp);
            timer.Start();
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            TimePlaceHolder = GetTemplateChild("PART_TimePlaceHolder") as TextBlock;
        }
        public void Tick(object sender, EventArgs args)
        {
            var stamp = CreateTimeStamp();
            UpdateTimer(stamp);
        }

        private string CreateTimeStamp()
        {

            if (InitialDate == null || InitialDate == default(DateTime))
            {
                InitialDate = DateTime.Now;
            }
            var Now = DateTime.Now;
            var madeTime = InitialDate;
            long difference;
            DateTime result;

            if (Now.CompareTo(madeTime) == 1)
            {
                difference = Now.Ticks - madeTime.Ticks;
                result = new DateTime(difference);
            }
            else
                if (madeTime.CompareTo(Now) == 1)
                {
                    return string.Format(DateStringFormat, "Now!");

                }
                else
                {
                    return string.Format(DateStringFormat, "seconds ago");
                }
            if (madeTime.Year == Now.Year)
            {
                if (madeTime.Month == Now.Month)
                {
                    if (madeTime.DayOfYear == Now.DayOfYear)
                    {

                        if (result.Hour < 1)
                        {
                            if (result.Minute < 1)
                            {
                                if (result.Second == 0)
                                {
                                    return string.Format(DateStringFormat, "Now!");
                                }
                                return string.Format(DateStringFormat, Convert.ToString(result.Second) + "s");
                            }
                            return string.Format(DateStringFormat, Convert.ToString(result.Minute) + "m");
                        }
                        return string.Format(DateStringFormat, Convert.ToString(result.Hour) + "h");
                    }
                    else
                    {
                        int time = Now.DayOfYear - madeTime.DayOfYear;
                        if (time == 1)
                            return string.Format(DateStringFormat,"Yesterday");

                        return string.Format(DateStringFormat, Convert.ToString(time).Replace("-", string.Empty) + "d");
                    }
                }
                else
                {
                    if (Now.DayOfYear - madeTime.DayOfYear < 31)
                        return string.Format(DateStringFormat, Convert.ToString(Now.DayOfYear - madeTime.DayOfYear) + "d");

                    if (result.Month == 1)
                        return string.Format(DateStringFormat,"Last Month");

                    return string.Format(DateStringFormat,Convert.ToString(result.Month) + " Mnths");
                }
            }
            else
            {
                return string.Format(DateStringFormat, Convert.ToString(Now.Year - madeTime.Year).Replace("-", string.Empty) + "y");

            }

        }

        private void UpdateTimer(string stamp)
        {
            if (TimePlaceHolder != null)
            {
                TimePlaceHolder.Text = stamp;
            }
        }
    }
}
