using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.Interfaces;
using Twitterizer;
using TwaijaComposite.Modules.Common.DataInterfaces;
namespace TwaijaComposite.RequestAdapterModule
{
    public class RetrieveTwitterProfile_LargeMethodImp :TwitterMethodBase, IRetrieveTwitterProfile_LargeMethod
    {
        public string TargetScreenName
        {
            get;
            set;
        }       
        public Modules.Common.DataInterfaces.ITwitterProfile_Large Create(Modules.Common.Navigation direction)
        {
            ITwitterProfile_Large profile = null;
            try
            {
                var user=TwitterUser.Show(Token, TargetScreenName).ResponseObject;
                if (user != null)
                {
                    profile = new TwitterProfile_LargeAdapter(user);
                   
                }
            }
            catch (Exception)
            {

            }
            return profile;
        }
    }
}
