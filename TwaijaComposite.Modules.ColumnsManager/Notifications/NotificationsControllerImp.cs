using System;
using System.Collections.Generic;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.Preferencing;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Events;

namespace TwaijaComposite.Modules.ColumnsManager.Notifications
{
    public class NotificationsControllerImp : INotificationsController
    {
        #region fields
object asynchlock=new object();
        IRegionManager r_Manager;
        INotificationViewmodel model;
        INotificationsView _View;
        //GeneralPreferences generalPreferences;
        #endregion
        [Dependency]
        public Preferences pref { get; set; }

        public NotificationsControllerImp(IEventAggregator aggr, IRegionManager r_Manager, INotificationViewmodel model, INotificationsView view)
        {
            //generalPreferences = pref.GeneralOptions;
            this.model = model;
            aggr.GetEvent<NewNotificationsEvent>().Subscribe(RecieveNewMessage, Microsoft.Practices.Prism.Events.ThreadOption.UIThread);
            this.r_Manager = r_Manager;
            this._View = view;
            model.CloseContent += new EventHandler(model_CloseContent);
        }

        void model_CloseContent(object sender, EventArgs e)
        {
            lock (asynchlock)
            {
                r_Manager.Regions[RegionNames.NotificationsSpace].Remove(_View);
                _View.IsAlive = false;
            }
        }

        public void RecieveNewMessage(Notice args)
        {
            if (pref.GeneralOptions.IsNotificationEnabled)
            {
                lock (asynchlock)
                {

                    if (_View.IsAlive)
                    {
                        model.Add(args);
                    }
                    else
                    {
                        _View.IsAlive = true;
                        _View.TriggerTimerReset();
                        _View.SuggestedPosition = pref.GeneralOptions.SelectedNotificationWindowPosition;
                        model.Clear();
                        model.Add(args);
                        r_Manager.Regions[RegionNames.NotificationsSpace].Add(_View);
                        r_Manager.Regions[RegionNames.NotificationsSpace].Activate(_View);
                    }
                }
            }
        }

        public void Initialize()
        {
            
        }
    }
}
