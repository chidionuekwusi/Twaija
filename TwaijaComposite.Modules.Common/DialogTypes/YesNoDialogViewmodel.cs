using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.Practices.Composite.Presentation.Commands;
using System.Threading;
using Microsoft.Practices.Prism.Commands;

namespace TwaijaComposite.Modules.Common.DialogTypes
{
    public class YesNoDialogViewmodel:IYesNoDialogViewmodel
    {
        public string Text { get; set; }
        bool CanExecuteOnUIThread = true;
        private Action<object> _yesaction;
        public YesNoDialogViewmodel(Action<object> _yesaction)
        {
            this._yesaction = _yesaction;
            YesCommand = new DelegateCommand<object>(this._yesaction);
        }
        public YesNoDialogViewmodel(Action<object> _yesaction,string Text,bool UIThread)
        {
            this._yesaction = _yesaction;
            CanExecuteOnUIThread = UIThread;
            YesCommand = new DelegateCommand<object>((p) => {
                if (CanExecuteOnUIThread)
                {
                    try
                    {
                        this._yesaction(p);
                    }
                    catch (Exception)
                    {

                    }
                }
                else
                {
                    ThreadPool.QueueUserWorkItem((d) =>
                    {
                        try
                        {
                            this._yesaction(d);
                        }
                        catch (Exception e)
                        {
                            
                        }
                    }, p);
                }
                OnClose(null); });
            this.Text = Text;
            CanExecuteOnUIThread = UIThread;
        }
        public DelegateCommand<object> YesCommand
        {
            get;
            set;
        }
        private DelegateCommand<object> _no;
        public DelegateCommand<object> NoCommand
        {
            get
            {
                if (_no == null)
                {
                    _no = new DelegateCommand<object>((p) => { OnClose(null); });
                }
                return _no;
            }
        }
        public event EventHandler CloseContent;
        protected void OnClose(EventArgs args)
        {
            if (CloseContent != null)
            {
                try
                {
                    CloseContent(this, args);
                }
                catch (Exception e)
                {
                }
            }
        }


        public void TriggerContentClosure()
        {
            throw new NotImplementedException();
        }
    }
}
