using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.Practices.Composite.Presentation.Commands;
using System.Threading;
using TwaijaComposite.Modules.Common.Exceptions;
using Microsoft.Practices.Prism.Commands;

namespace TwaijaComposite.Modules.Common.DialogTypes
{
   public class DecisionDialogViewmodel:IDecisionDialogViewmodel
    {
       bool CanExecuteOnUIThread = true;
       public DecisionDialogViewmodel(IList<object> options, string Text, Action<object> action,bool UIthread)
       {
           Options = options;
           this.Text = Text;
           _action = action;
           CanExecuteOnUIThread = UIthread;
       }
       private void PassSelectedObject(object u)
       {
           bool CanClose = false;
           if (CanExecuteOnUIThread)
           {
               try
               {
                   _action(u);
               }
               catch (WrongUserTypeException exc)
               {

               }
               catch (Exception e)
               {
                  
               }
               if (CanClose)
               {
                   OnClose(null);
               }
           }
           else
           {
               ThreadPool.QueueUserWorkItem((d) => {
                   try
                   {
                       _action(d);
                   }
                   catch (Exception e)
                   {
                       throw new Exception("Thrown while Passing the selected object to the method:reason:" + e.Message, e);
                   }
                   if (CanClose)
                   {
                       OnClose(null);
                   }
               }, u);
           }
      
           
       }
        private Action<object> _action;
        public IList<object> Options { get; set; }
        public object SelectedOption { get; set; }
        public event EventHandler CloseContent;
        public string Text { get; set; }
        private DelegateCommand<object> _closecommand;
        public DelegateCommand<object> CloseCommand
        {
            get
            {
                if (_closecommand == null)
                    _closecommand = new DelegateCommand<object>((p) => PassSelectedObject(SelectedOption)); 
                    
                return _closecommand;
            }
        }
        public DelegateCommand AbortCommand
        {
            get { return new DelegateCommand(() => { OnClose(null); }); }
        }
        private void CleanUp()
        {
            _action = null;
            Options.Clear();
        }
        protected void OnClose(EventArgs args)
        {
            if (CloseContent != null)
            {
                CleanUp();
                try
                {
                    CloseContent(this, args);
                }
                catch (Exception c)
                {

                }
                
            }
        }



        public string Status
        {
            get;
            set;
        }


        public void TriggerContentClosure()
        {
            throw new NotImplementedException();
        }
    }
}
