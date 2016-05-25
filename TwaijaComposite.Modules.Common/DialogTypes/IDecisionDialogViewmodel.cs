using System;
using System.Collections.Generic;
using Microsoft.Practices.Prism.Commands;
//using Microsoft.Practices.Composite.Presentation.Commands;

namespace TwaijaComposite.Modules.Common.DialogTypes
{
    public interface IDecisionDialogViewmodel:IClosableContent
    {
        IList<object> Options { get; }
        string Text { get; }
        string Status { get; }
        DelegateCommand<object> CloseCommand { get; }
    }
}
