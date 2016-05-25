using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.DataInterfaces;

namespace TwaijaComposite.Modules.ColumnsManager.Viewmodels
{
    public class TrendViewmodel:ISetable<ITrend>
    {
        ITrend trend;
        public void Set(ITrend param)
        {
            trend = param;
        }
    }
}
