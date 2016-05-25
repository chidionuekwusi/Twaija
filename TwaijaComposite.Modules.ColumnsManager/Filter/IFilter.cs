using System;
using System.Collections.Generic;
using TwaijaComposite.Modules.ColumnsManager.Filter;
using Microsoft.Practices.Prism.Commands;
using TwaijaComposite.Modules.Common;
//using Microsoft.Practices.Composite.Presentation.Commands;

namespace TwaijaComposite.Modules.ColumnsManager
{
    public interface IFilter : IDisposable
    {
        IList<IFilterCriteria> Criterion { get; }
        int MaxNumberOfItemsAllowed { get; }
        IEnumerable<object> Filter(IEnumerable<object> content, out IEnumerable<object> filteredoutcontent);     
    }
}
