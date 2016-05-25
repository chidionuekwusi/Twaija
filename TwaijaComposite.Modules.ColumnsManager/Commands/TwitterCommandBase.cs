using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.Interfaces;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.Common.DataInterfaces;

namespace TwaijaComposite.Modules.ColumnsManager.Commands
{
    public class TwitterCommandBaseHelper:IDisposable
    {
        public TwitterCommandBaseHelper()
        {
            SuccessMessage = "Successful";
            FailureMessage = "An Error occured";
        }
        [Dependency]
        public IDialogFacade dialogFacade { get; set; }
        protected string SuccessMessage { get; set; }
        protected string FailureMessage { get; set; }
        protected virtual bool Condition(ITwitterUser user)
        {
            return true;
        }
        protected virtual void Template(object user)
        {
            var twitteruser = user as ITwitterUser;
            int attempts = 0;
            bool successful = false;
            while (true)
            {
                if (attempts == 3)
                {
                    break;
                }
                if (successful = Condition(twitteruser))
                {
                    break;
                }
                attempts++;
            }
            if (successful)
            {
                dialogFacade.PushMessageDialog(SuccessMessage);
            }
            else
            {
                dialogFacade.PushMessageDialog(FailureMessage);
            }
        }

        public virtual void Execute(object parameter)
        {
            
        }
        public virtual void Dispose()
        {
            dialogFacade = null;
        }
    }
}
