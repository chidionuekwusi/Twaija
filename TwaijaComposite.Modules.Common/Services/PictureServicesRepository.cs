using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.Interfaces;

namespace TwaijaComposite.Modules.Common.Services
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public class PictureServicesRepository:IPictureServicesRepository
    {
        #region Properties
        private Dictionary<string,IPictureService> PictureServices { get; set; }
        public IList<string> AvailiableServices { get; private set; }
        #endregion
        public PictureServicesRepository(IEnumerable<IPictureService> availiableServices)
        {
            PictureServices = new Dictionary<string, IPictureService>();
            FillPictureServices(availiableServices);
            AvailiableServices = PictureServices.Keys.ToList();
        }
        #region Methods
        void FillPictureServices(IEnumerable<IPictureService> services)
        {
            foreach (IPictureService service in services)
            {
                PictureServices.Add(service.Name,service);
            }
        }
        #endregion


        public IPictureService GetService(string Key)
        {
            return PictureServices[Key];
        }

        public bool ServiceExists(string Key)
        {
            return PictureServices.ContainsKey(Key);
        }
    }
  
}
