using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Commands;
//using Microsoft.Practices.Composite.Presentation.Commands;

namespace TwaijaComposite.Modules.Common.DialogTypes
{
   public class OkDialogViewmodel:IOkDialogViewmodel
    {
       public OkDialogViewmodel() { }
       public OkDialogViewmodel(string Text)
       {
           this.Text = Text;
       }
       public string Text { get; set; }
       private DelegateCommand<object> _closecommand;
       public DelegateCommand<object> CloseCommand
       {
           get
           {
               if (_closecommand == null)
                   _closecommand = new DelegateCommand<object>((p) => { OnClose(null); });
                   return _closecommand;
           }
       }
           
       public event EventHandler CloseContent;
       protected void OnClose(EventArgs args)
       {
           if (CloseContent != null)
           {
               CloseContent(this, args);
           }
       }


       public void TriggerContentClosure()
       {
           throw new NotImplementedException();
       }
    }
}
