using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.ColumnsManager.Column;

namespace TwaijaComposite.Modules.ColumnsManager
{
    public class ColumnDirector:IDirector
    {
        public void Construct(IColumnBuilder builder)
        {
            builder.BuildFilter();
            builder.BuildRequest();
            builder.BuildCommands();
            builder.BuildInfrastructure();
        }

        
    }
}
