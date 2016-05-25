using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.Interfaces;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.Common.Events;
using Microsoft.Practices.Prism.Regions;
using TwaijaComposite.Modules.Common;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using System.ComponentModel;
#if SILVERLIGHT
using GalaSoft.MvvmLight.Command;
#endif
namespace TwaijaComposite.Modules.ProfileViewer.ProfileManager
{
    public class Manager : IProfileController, INotifyPropertyChanged
    {
        #region fields
        private object asynchlock = new object();
        private bool _displayMainRegion = true;
        public bool DisplayMainRegion
        {
            get { return _displayMainRegion; }
            set
            {
                if (_displayMainRegion != value)
                {
                    _displayMainRegion = value;
                    OnPropertyChanged("DisplayMainRegion");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
#if SILVERLIGHT
        RelayCommand<object> _goBackCommand;
        RelayCommand<object> _goFowardCommand;
#else
        private DelegateCommand<object> _goBackCommand;
        private DelegateCommand<object> _goFowardCommand;
#endif
        private  Dictionary<string,IProfileEventHandler> handlers;
        private IEnumerable<IProfileEventHandler> _handlers;
        private bool initialized = false;
        volatile bool intransition = false;
        private readonly IRegionManager m_RegionManager;
        private readonly List<object> views = new  List<object>(3);
        private int currentViewIndex = -1;
        private readonly IEventAggregator aggr;
        private ProfileManagerView _view;
        private ProfileManagerViewmodel _model;
       // private IProfileManagerState State;
        ProfileNavigation currentNav;
        #endregion
        public ICommand GoBackCommand
        {
            get
            {
                if (_goBackCommand == null)
                {
#if !SILVERLIGHT
                    _goBackCommand = new DelegateCommand<object>(NavigateBackwards,CanNavigateBackwards);
#else
                    _goBackCommand=new RelayCommand<object>(NavigateBackwards, CanNavigateBackwards);
#endif
                }
                return _goBackCommand;
            }
        }
        public ICommand GoFowardCommand
        {
            get
            {
                if (_goFowardCommand == null)
                {
#if !SILVERLIGHT
                    _goFowardCommand = new DelegateCommand<object>(NavigateFoward,CanNavigateFoward);
#else
                    _goFowardCommand = new RelayCommand<object>(NavigateFoward, CanNavigateFoward);
#endif
                }
                return _goFowardCommand;
            }
        }
        public Manager(ProfileManagerView view, IEnumerable<IProfileEventHandler> handlers, IRegionManager manager, IEventAggregator aggr)
        {
            _handlers = handlers;
            m_RegionManager = manager;
            this.aggr = aggr;
            _view = view;
            DisplayMainRegion = true;
        }
        private void InitializeHandlers()
        {
            handlers = new Dictionary<string, IProfileEventHandler>();
            foreach (IProfileEventHandler handler in _handlers)
            {
                handlers.Add(handler.Name, handler);
            }
        }
        public void Initialize()
        {
            try
            {
                if (!initialized)
                {
                    InitializeHandlers();
                    var eve=aggr.GetEvent<OpenProfileEvent>();
                    eve.Subscribe(new Action<OpenProfileEventArgs>(HandleOPE), Microsoft.Practices.Prism.Events.ThreadOption.UIThread, true);
                    initialized = true;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Failed to initialize Manager(Profile):reason(s):" + e.Message, e);
            }
        }

        public void AddAndActivate(object view)
        {
            m_RegionManager.Regions[RegionNames.Main_ProfileSpace].Add(view);
            m_RegionManager.Regions[RegionNames.Main_ProfileSpace].Activate(view);
            views.Add(view);
            currentViewIndex=views.Count-1;

        }
        public void ShowProfileViewer()
        {
            if (!m_RegionManager.Regions[RegionNames.ProfileSpace].Views.Contains(_view))
            {
                _view.DataContext = this;
                m_RegionManager.Regions[RegionNames.ProfileSpace].Add(_view);
            }
            m_RegionManager.Regions[RegionNames.ProfileSpace].Activate(_view);
        }
        private void CloseProfileViewer()
        {
            if (m_RegionManager.Regions[RegionNames.ProfileSpace].Views.Contains(_view))
            {
                m_RegionManager.Regions[RegionNames.ProfileSpace].Deactivate(_view);
            }
        }
        public void ReEvaluateSize()
        {
            if (m_RegionManager.Regions[RegionNames.Main_ProfileSpace].Views.Count() == 2)
            {
            
                int index=0;
                switch (currentViewIndex)
                {
                    case 0:
                        index = 1;
                        break;
                    case 1:
                        index=0;
                        break;
                }

                m_RegionManager.Regions[RegionNames.Main_ProfileSpace].Deactivate(views[index]);
                m_RegionManager.Regions[RegionNames.Main_ProfileSpace].Remove(views[index]);
                var view = views[index] as IDisposable;
                views.Remove(views[index]);
                if (view != null)
                {
                    view.Dispose();
                }
                if (currentViewIndex >= views.Count)
                {
                    currentViewIndex = views.Count - 1;
                }
            }
        }
        
        private void HandleOPE(OpenProfileEventArgs args)
        {
            lock (asynchlock)
            {
                if (!intransition)
                {
                    IProfileEventHandler handler = null;
                    handler = ResolveHandler(args);
                    var view = handler.HandleEvent(args);
                    if (view != null)
                    {
                        ShowProfileViewer();
                        if (DisplayMainRegion)
                        {
                            object currentview = null;
                            if (m_RegionManager.Regions[RegionNames.Main_ProfileSpace].ActiveViews.Count() > 0)
                            {
                                currentview = m_RegionManager.Regions[RegionNames.Main_ProfileSpace].ActiveViews.First();
                                if (currentview is DeactivationInteractionTrigger)
                                {
                                    (currentview as DeactivationInteractionTrigger).Deactivated += new EventHandler((f, O) => { });
                                    (currentview as DeactivationInteractionTrigger).Deactivate();
                                }
                                m_RegionManager.Regions[RegionNames.Main_ProfileSpace].Deactivate(currentview);
                                m_RegionManager.Regions[RegionNames.Main_ProfileSpace].Remove(currentview);
                            }
                            if (m_RegionManager.Regions[RegionNames.Secondary_ProfileSpace].Views.Count() > 0)
                            {
                                var secondaryview=m_RegionManager.Regions[RegionNames.Secondary_ProfileSpace].Views.First();
                                m_RegionManager.Regions[RegionNames.Secondary_ProfileSpace].Deactivate(secondaryview);
                                m_RegionManager.Regions[RegionNames.Secondary_ProfileSpace].Remove(secondaryview);
                             //clean up
                            }
                            if (currentview != null)
                            {
                                m_RegionManager.Regions[RegionNames.Secondary_ProfileSpace].Add(currentview);
                                m_RegionManager.Regions[RegionNames.Secondary_ProfileSpace].Activate(currentview);
                            }
                            
                        }

                        m_RegionManager.Regions[RegionNames.Main_ProfileSpace].Add(view);
                        m_RegionManager.Regions[RegionNames.Main_ProfileSpace].Activate(view);
                        DisplayMainRegion = true;
                        //ShowProfileViewer();
                        //ReEvaluateSize();
                        //AddAndActivate(view);
                    }
                }
                _goBackCommand.RaiseCanExecuteChanged();
                _goFowardCommand.RaiseCanExecuteChanged();
            }
        }

        private void PushMainRegionViewToSecondaryRegion()
        {
            throw new NotImplementedException();
        }

        private void NavigateToMainRegion()
        {
            throw new NotImplementedException();
        }
        void GoTo(int index)
        {
            m_RegionManager.Regions[RegionNames.Main_ProfileSpace].Activate(views[index]);
        }
        public void NavigateBackwards(object state)
        {
            lock (asynchlock)
            {
                DisplayMainRegion = false;
                //currentNav = ProfileNavigation.Backwards;
                //intransition = true;
                //DeactivateCurrentView();
            }
        }
        public void NavigateFoward(object state)
        {
            lock (asynchlock)
            {
                DisplayMainRegion = true;
                //currentNav = ProfileNavigation.Foward;
                //intransition = true;
                //DeactivateCurrentView();
            }
        
        }
        void innerFoward()
        {
            lock (asynchlock)
            {
                if (CanNavigateFoward(null))
                {
                    currentViewIndex++;
                    GoTo(currentViewIndex);

                    _goBackCommand.RaiseCanExecuteChanged();
                    _goFowardCommand.RaiseCanExecuteChanged();
                }
            }
        }
        void innerBackward()
        {
            lock (asynchlock)
            {
                if (CanNavigateBackwards(null))
                {
                    currentViewIndex--;
                    GoTo(currentViewIndex);
                    _goBackCommand.RaiseCanExecuteChanged();
                    _goFowardCommand.RaiseCanExecuteChanged();
                }
            }
        }
        void DeactivateCurrentView()
        {
            if (views[currentViewIndex] is DeactivationInteractionTrigger)
            {
                (views[currentViewIndex] as DeactivationInteractionTrigger).Deactivated += new EventHandler(Manager_Deactivated);
                (views[currentViewIndex] as DeactivationInteractionTrigger).Deactivate();
            }
            else
            {
                Manager_Deactivated(null, null);
            }
        }

        void Manager_Deactivated(object sender, EventArgs e)
        {
            
                switch (currentNav)
                {
                    case ProfileNavigation.Foward:
                        innerFoward();
                        break;
                    case ProfileNavigation.Backwards:
                        innerBackward();
                        break;
                }
                if (sender is DeactivationInteractionTrigger)
                {
                    (sender as DeactivationInteractionTrigger).Deactivated -= Manager_Deactivated;
                }
                intransition = false;
            
        }
        public bool CanNavigateBackwards(object state)
        {
            return true;
            //return (DisplayMainRegion) ? m_RegionManager.Regions[RegionNames.Secondary_ProfileSpace].Views.Count() > 0 : false;
            //if (views.Count>1&&currentViewIndex!=0)
            //{
            //    return true;
            //}
            //return false;
        }

        public bool CanNavigateFoward(object state)
        {
            return true;
            //return (DisplayMainRegion) ? false : m_RegionManager.Regions[RegionNames.Main_ProfileSpace].Views.Count() > 0;
            //if ((currentViewIndex<(views.Count-1))&&views.Count>1)
            //{
            //    return true;
            //}
            //return false;
        }
        private IProfileEventHandler ResolveHandler(OpenProfileEventArgs args)
        {
            if (handlers.ContainsKey(args.ProfileType))
            {
                return handlers[args.ProfileType];
            }
            return null;
        }

    }
   
    enum ProfileNavigation
    {
        Foward, Backwards
    }
}
