using System;
using System.Collections.Generic;
using Microsoft.Practices.Prism.Commands;
//using Microsoft.Practices.Composite.Presentation.Commands;

namespace TwaijaComposite.Modules.Common.DialogTypes
{
    public interface IYesNoDialogViewmodel:IClosableContent
    {
        string Text { get; }
        DelegateCommand<object> YesCommand { get; }
        DelegateCommand<object> NoCommand { get; }
    }
}
