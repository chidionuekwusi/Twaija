using System;
using System.Collections.Generic;
using System.Collections;

namespace TwaijaComposite.Modules.Common.Interfaces
{
    public interface IHeaderAndContentObject : IDisposable
    {
        void Initialize();
        IEnumerable Content { get; }
        IList<System.Windows.Input.ICommand> Commands { get; set; }
        string Header { get; set; }
        Uri Icon { get; set; }
        event EventHandler Close;
        void AddContent(object content);
        void RemoveContent(object key);
        void InsertContent(object content);
        void ClearContent();
    }
}
