using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.ViewModels;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common.Services;

namespace TwaijaComposite.Modules.Common.DialogTypes
{
    public class LoadingDialogViewmodel : ViewModelBase, IHandlesProgressReports
    {
        public IDispatcher dispatcher { get; set; }
        public LoadingDialogViewmodel()
        {
            Handler = new ProgressReport(RecieveProgressReport); 
        }
        private int _percentageCompleted;

        public int PercentageCompleted
        {
            get { return _percentageCompleted; }
            set
            {
                dispatcher.Invoke(new System.Threading.SendOrPostCallback((s)=>{
                _percentageCompleted = (int)s ;
                OnPropertyChanged("PercentageCompleted");}),value);
            }
        }
        private string _report;

        public string Report
        {
            get { return _report; }
            set
            {
#if SILVERLIGHT
                dispatcher.Invoke(new System.Threading.SendOrPostCallback((s) =>
                {
                    _report = s as string;
                    OnPropertyChanged("Report");
                }),value);
#else
                if(_report!=value){
                _report=value;
                  OnPropertyChanged("Report");}
#endif
            }
        }
        
        public void RecieveProgressReport(object sender,string report, int percentageCompleted)
        {
            Report = report;
            PercentageCompleted = PercentageCompleted;
            if (sender != null && percentageCompleted == 100)
            {
               var reportSource= sender as IProgressReportable;
               if (reportSource != null)
               {
                   reportSource.Progress -= RecieveProgressReport;
               }
            }
        }
        public void AbortReport(object sender, EventArgs e)
        {

        }
        public ProgressReport Handler
        {
            get;
            set;
        }
    }
}
