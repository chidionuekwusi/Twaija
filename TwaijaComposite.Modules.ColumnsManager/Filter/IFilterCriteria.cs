using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.ColumnsManager.Filter
{
    public interface IFilterCriteria
    {
        bool Included { get; set; }
        bool Filter(object state);
        IFilterCriteria nextCriteria { get; set; }
    }
}
