using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.ColumnsManager
{
    public interface IColumnBuilder
    {
        string BuilderName { get; }
        void BuildRequest();
        void BuildFilter();
        void BuildCommands();
        void BuildInfrastructure();
        IColumn GetProduct();
    }
}
