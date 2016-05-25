using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.ColumnsManager.Column
{
    public interface IDirector
    {
        void Construct(IColumnBuilder builder);
    }
}
