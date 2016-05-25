using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.Interfaces;

namespace TwaijaComposite.Modules.Common.Services
{
    public class PostMessageServiceRepositoryImp : IPostMessageServiceRepository
    {
        #region Properties
        private Dictionary<string, IPostMessageService> Services { get; set; }
        public IList<string> AvailiableServices { get; private set; }
        #endregion
        public PostMessageServiceRepositoryImp(IEnumerable<IPostMessageService> availiableServices)
        {
            Services = new Dictionary<string, IPostMessageService>();
            FillServices(availiableServices);
            AvailiableServices = Services.Keys.ToList();
        }
        #region Methods
        void FillServices(IEnumerable<IPostMessageService> services)
        {
            foreach (IPostMessageService service in services)
            {
                Services.Add(service.Name, service);
            }
        }
        #endregion


        public IPostMessageService GetService(string Key)
        {
            return Services[Key];
        }

        public bool ServiceExists(string Key)
        {
            return Services.ContainsKey(Key) ;
        }
    }
}
