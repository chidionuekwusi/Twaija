using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.Events;

namespace TwaijaComposite.Modules.Common.Interfaces
{
    public interface IColumnResolutionService
    {
        void Initialize();
        IHeaderAndContentObject HandleEvent(CreateColumnEventArgs args);
        T HandleEvent<T>(CreateColumnEventArgs args) where T : class,IHeaderAndContentObject;
    }
}
