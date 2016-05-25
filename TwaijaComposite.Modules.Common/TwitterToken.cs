using System;
using System.Runtime.Serialization;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

namespace TwaijaComposite.Modules.Common
{
#if !SILVERLIGHT
    [Serializable]
#else
    [DataContract]
#endif
    public class TwitterToken
    {
#if SILVERLIGHT
        [DataMember]
#endif
        public string ScreenName { get; set; }
#if SILVERLIGHT
        [DataMember]
#endif
        public string ConsumerKey { get; set; }
#if SILVERLIGHT
        [DataMember]
#endif
        public string ConsumerSecret { get; set; }
#if SILVERLIGHT
        [DataMember]
#endif
        public string TokenKey { get; set; }
#if SILVERLIGHT
        [DataMember]
#endif
        public string TokenSecret { get; set; }
    }
}
