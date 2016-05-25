using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.Interfaces;

namespace TwaijaComposite.Modules.ColumnsManager.Column
{
    public class ProxyColumnImp : IHeaderAndContentObject
    {
        public object MainContent { get; set; }
        public void Initialize()
        {
        }

        public System.Collections.IEnumerable Content
        {
            get { return null; }
        }

        public IList<System.Windows.Input.ICommand> Commands
        {
            get;
            set;
        }

        public string Header
        {
            get { return "Profile"; }
            set { }
        }

        public Uri Icon
        {
            get;
            set;
        }

        public event EventHandler Close;

        public void AddContent(object content)
        {
            throw new NotImplementedException();
        }

        public void RemoveContent(object key)
        {
            throw new NotImplementedException();
        }

        public void InsertContent(object content)
        {
            throw new NotImplementedException();
        }

        public void ClearContent()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
           
        }
    }
}
