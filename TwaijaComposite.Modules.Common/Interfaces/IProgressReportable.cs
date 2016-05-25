using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace TwaijaComposite.Modules.Common.Interfaces
{
    public delegate void ProgressReport(object sender,string Report,int PercentageCompleted);
    public interface IProgressReportable
    {
        event ProgressReport Progress;
    }
    public interface IHandlesProgressReports
    {
        ProgressReport Handler { get; set; }
    }
}
