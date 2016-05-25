using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.DialogTypes;

namespace TwaijaComposite.Modules.Common.Interfaces
{
    public interface IDialogFacade
    {
        void PushLoadingDialog(IClosableContent Content);
        void PushMessageDialog(string message);
        void PushYesNoDecisionDialog(string message, Action<object> yesaction,bool IsExecutedOnUIThread);
        void PushDecisionWithOptionsDialog(string message, IList<object> options, Action<object> selectedaction,bool IsExecutedOnUIThread);
    }
}
