using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using TwaijaComposite.Modules.Common.Interfaces;
using Microsoft.Practices.Unity;
//using Microsoft.Practices.Composite.Events;
using TwaijaComposite.Modules.Common.Events;
//using Microsoft.Practices.Composite.Regions;
using System.Collections.Generic;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Events;

namespace TwaijaComposite.Modules.Common.Services
{
    public class NavigationReaction
    {
       public Action<NavigatingEventArgs> response { get; set; }
       public bool IsOneTimeInvocation { get; set; }
    }
   public class NavigationServiceImp:INavigationService
   {
       #region fields
       readonly IUnityContainer m_Container;
       readonly IRegionManager m_Manager;
       private  ViewRequestedEventArgs PreviousRequest;
       private ViewRequestedEventArgs CurrentRequest;
       private object _prevView;
       private string _homeview;
       private string _homeregion;
       private bool HomeIsSet = false;
       private Dictionary<object, List<NavigationReaction>> _dictionary = new Dictionary<object, List<NavigationReaction>>();
       #endregion
       void InvokeHandlers(object key,NavigatingEventArgs args)
       {
           if (_dictionary.ContainsKey(key))
           {
               List<NavigationReaction> OneTimers = new List<NavigationReaction>();
               foreach (NavigationReaction action in _dictionary[key])
               {
                   action.response(args);
                   if (action.IsOneTimeInvocation)
                   {
                       OneTimers.Add(action);
                   }
               }
               foreach (NavigationReaction rc in OneTimers)
               {
                   _dictionary[key].Remove(rc);
               }
           }
       }
       private void DoNavigateTo(Events.ViewRequestedEventArgs args)
       {
    
           string region = (string.IsNullOrEmpty(args.RegionRequested)) ? _homeregion : args.RegionRequested;
         
           if (!args.IsCanceled)
           {
               var ToView = m_Container.Resolve<object>(args.ViewRequested);
               var ToViewContext = ToView as TwaijaComposite.Modules.Common.Interfaces.INavigationAware;
              
                  //InvokeHandlers
                   if (_prevView != null)
                   {
                       InvokeHandlers(_prevView, new NavigatingEventArgs() { NameOfRequestedRegion = args.ViewRequested, NameOfRequestedView = args.RegionRequested, Direction = NavigationDirection.From });
                   }
                   InvokeHandlers(ToView, new NavigatingEventArgs() { NameOfRequestedRegion = args.ViewRequested, NameOfRequestedView = args.RegionRequested, Direction = NavigationDirection.To });
                 
                  //Alert the View of the Navigation
                  if (ToViewContext != null)
                   {
                       try
                       {
                           ToViewContext.Navigating(new NavigatingEventArgs() { NameOfRequestedRegion = args.ViewRequested, NameOfRequestedView = args.RegionRequested, Direction = NavigationDirection.To });
                       }
                       catch (Exception e)
                       {
                           throw new NavigationException("Error occurred while Calling Navigating on Context", e);
                       }
                   }
                   //Hookup NavigationCallback
                   if (args.NavigationCallback != null)
                   {
                       if (_dictionary.ContainsKey(ToView))
                       {
                           _dictionary[ToView].Add(new NavigationReaction() { response = args.NavigationCallback, IsOneTimeInvocation = args.IsOneTimeCallbackInvocation });
                       }
                       else
                       {
                           _dictionary.Add(ToView, new List<NavigationReaction>());
                           _dictionary[ToView].Add(new NavigationReaction() {IsOneTimeInvocation=args.IsOneTimeCallbackInvocation, response=args.NavigationCallback});
                       }
                   }
               
               if (ToView != null)
               {
                   try
                   { 
                       if (!m_Manager.Regions[region].Views.Contains(ToView))
                       {
                           m_Manager.Regions[region].Add(ToView);
                       }
                           m_Manager.Regions[region].Activate(ToView);
                           if (CurrentRequest != null)
                           {
                               PreviousRequest = CurrentRequest;
                           }
                           CurrentRequest = args;
                           _prevView = ToView;
                   }
                   catch
                   {
                      
                   }
               }
               else
               {
                   throw new NavigationException("The Chosen View has not been created");
               }
           }
       }

       void context_NavigatingEvent(object view, NavigatingEventArgs Name)
       {
           
       }
        public void NavigateTo(Events.ViewRequestedEventArgs args)
        {
            if (args == null)
            {
                if (PreviousRequest != null)
                {
                    PreviousRequest.IsCanceled = false;
                    DoNavigateTo(PreviousRequest);
                }
                else
                {
                    if (!string.IsNullOrEmpty(HomeView))
                    {
                        DoNavigateTo(new ViewRequestedEventArgs() { RegionRequested = HomeRegion, ViewRequested = HomeView });
                    }
                }
            }
            else
            {
                 DoNavigateTo(args);  
            }
        }
        private Events.ViewRequestedEvent _requestview;
        public Events.ViewRequestedEvent RequestView
        {
            get
            {
                if (_requestview == null)
                {
                   var agg= m_Container.Resolve<IEventAggregator>();
                  _requestview= agg.GetEvent<Events.ViewRequestedEvent>();
                }
                return _requestview;
            }
        }

        public void SetHome(string homeview, string homeregion)
        {
            if (!HomeIsSet)
            {
                _homeregion = homeregion;
                _homeview = homeview;
                HomeIsSet = true;
            }
        }


        public NavigationServiceImp(IUnityContainer container, IRegionManager manager)
        {
            m_Container = container;
            m_Manager = manager;
        }


        public string HomeView
        {
            get { return _homeview; }
        }

        public string HomeRegion
        {
            get { return _homeregion; }
        }
   }
   public class NavigationException : Exception
   {
       public string Homeview { get; set; }
       public NavigationException(string message)
           : base(message)
       {
           
       }
       public NavigationException(string message, Exception innner)
           : base(message, innner)
       {
       }
   }
}
