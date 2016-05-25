using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.Common
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public class ThemeViewmodel
    {
        public string DisplayName { get; set; }
        public string Id { get; set; }
        public string ColorValue { get; set; }
    }
}
