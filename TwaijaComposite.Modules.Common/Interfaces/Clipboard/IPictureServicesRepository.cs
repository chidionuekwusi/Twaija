using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.Common.Interfaces
{
        public interface IPictureServicesRepository
        {
            IList<string> AvailiableServices { get;  }
            IPictureService GetService(string Key);
            bool ServiceExists(string Key);
        }    
}
