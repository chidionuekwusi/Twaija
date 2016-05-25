using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.Practices.Composite.Events;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common.Events;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.Common.Preferencing;
using System.Threading;
using Microsoft.Practices.Prism.Events;

namespace TwaijaComposite.Modules.Common.Services
{
    public class UserResolutionService
    {
        IEventAggregator aggr;
        UserRepository repos;
        GeneralPreferences generalPref;
        IDialogFacade dialog;
        public UserResolutionService(IEventAggregator aggr,Preferences pref,IDialogFacade facade)
        {
            this.generalPref = pref.GeneralOptions;
            repos = pref.TransparentUsersFacade.Userrepository;
            this.aggr = aggr;
            this.dialog = facade;
                    }
        
        public  void initialize()
        {
            this.aggr.GetEvent<UserResolutionDialogEvent>().Subscribe(RecieveMessage, Microsoft.Practices.Prism.Events.ThreadOption.UIThread);
        }
        
        private  void RecieveMessage(UserRDEventPayLoad payload)
        {
            string message = "";
            if (repos.Users.Count() > 1)
            {
                if (generalPref.PromptUserSelectionDialog)
                {
                    message = (string.IsNullOrEmpty(payload.Text)) ? "Select a user for the selected action" : payload.Text;
                    IList<object> users = null;

#if SILVERLIGHT
                    users=GetUsers();
#else
                    users = repos.Users.ToList<object>();
#endif
                    dialog.PushDecisionWithOptionsDialog(message, users, payload.action, payload.IsExecutedOnUIThread);
                }
                else
                {
                    if (payload.IsExecutedOnUIThread)
                    {
                        payload.action(repos.SelectedUser);
                    }
                    else
                    {
                        ThreadPool.QueueUserWorkItem((s) => payload.action(s), repos.SelectedUser);
                    }
                }
            }
            else
            {
                //message = (string.IsNullOrEmpty(payload.Text)) ? "Are u sure? " : payload.Text;
                message = "Are u sure? ";
                dialog.PushYesNoDecisionDialog(message, (g)=>payload.action(repos.SelectedUser), payload.IsExecutedOnUIThread);
            }
        }
#if SILVERLIGHT
        IList<object> GetUsers()
        {
        IList<object> users=new List<object>();
          foreach(object user in repos.Users)
            {
                users.Add(user);
            }
        return users;
        }
#endif
    }
}
