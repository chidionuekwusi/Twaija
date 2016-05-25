using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.Common.DataInterfaces
{
   public interface ITrend
    {
       string Address { get; set; }
       string Event { get; set; }
       string Name { get; set; }
       string PromotedContent { get; set; }
       string SearchQuery { get; set; }
    }
}
