using System;
using Microsoft.Practices.Prism.Commands;
//using Microsoft.Practices.Composite.Presentation.Commands;

namespace TwaijaComposite.Modules.Common.DialogTypes
{
    public interface IOkDialogViewmodel:IClosableContent
    {
        string Text { get; }
        DelegateCommand<object> CloseCommand { get; }
    }
}
