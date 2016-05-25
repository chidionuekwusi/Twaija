using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.Common.Services;


namespace TwaijaComposite.Modules.Common
{
    public class CommonInfrastructureController:IController
    {
        [Dependency]
        public UserResolutionService service {get;set;}
        public void Initialize()
        {
            service.initialize();
        }
    }
}
