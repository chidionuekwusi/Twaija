using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.Interfaces;
//using Microsoft.Practices.Composite.Regions;
using TwaijaComposite.Modules.Common.DialogTypes;
using Microsoft.Practices.Prism.Regions;


namespace TwaijaComposite.Modules.Common.Services
{
    public class DialogFacadeImp:IDialogFacade
    {
        #region fields
        IRegionManager m_RegionManager;
        IDispatcher dispatcher;
        object _currentView;
        LoadingDialogViewmodel currentmodel;
        #endregion
        public DialogFacadeImp(IRegionManager manager,IDispatcher dispatcher)
        {
            m_RegionManager = manager;
            this.dispatcher = dispatcher;
        }
        private void Activate(object view)
        {
            m_RegionManager.Regions[RegionNames.PopupRegion].Add(view);
            m_RegionManager.Regions[RegionNames.PopupRegion].Activate(view);
            _currentView = view;
        }
        public void PushMessageDialog(string message)
        {
            dispatcher.Invoke(new System.Threading.SendOrPostCallback((u) =>
            {
                var model = new OkDialogViewmodel(u.ToString());
                var view = new OkDialog();
                model.CloseContent += new EventHandler(model_Close);
                view.DataContext = model;
                Activate(view);
            }), message);
        }

        void model_Close(object sender, EventArgs e)
        {
            dispatcher.Invoke(new System.Threading.SendOrPostCallback((s) =>
            {
                if (_currentView != null)
                {
                    if (m_RegionManager.Regions[RegionNames.PopupRegion].Views.Contains(_currentView))
                    {
                        m_RegionManager.Regions[RegionNames.PopupRegion].Deactivate(_currentView);
                        m_RegionManager.Regions[RegionNames.PopupRegion].Remove(_currentView);
                    }
                }
            }));
            var closer=sender as IClosableContent;
            if (closer != null)
            {
                closer.CloseContent -= model_Close;
            }
        }

        public void PushYesNoDecisionDialog(string message, Action<object> yesaction,bool IsExecutedOnUIThread)
        {
            dispatcher.Invoke(new System.Threading.SendOrPostCallback((u) =>
            {
                var parameters = u as object[];
                var model = new YesNoDialogViewmodel(parameters[1] as Action<object>, parameters[0].ToString(),(bool)parameters[2]);
                var view = new YesNoDialogView();
                model.CloseContent += model_Close;
                view.DataContext = model;
                Activate(view);
            }), new object[] { message, yesaction ,IsExecutedOnUIThread});
        }

        public void PushDecisionWithOptionsDialog(string message, IList<object> options, Action<object> selectedaction,bool IsExecutedOnUIThread)
        {
            dispatcher.Invoke(new System.Threading.SendOrPostCallback((u) =>
            {
                var parameters = u as object[];
                var model = new DecisionDialogViewmodel(parameters[1] as IList<object>, parameters[0].ToString(), parameters[2] as Action<object>,(bool)parameters[3]);
                var view = new DecisionDialogView();
                model.CloseContent += model_Close;
                view.DataContext = model;
                Activate(view);
            }), new object[] { message, options, selectedaction ,IsExecutedOnUIThread});
        }

        public void PushLoadingDialog(IClosableContent Content)
        {
            dispatcher.Invoke(new System.Threading.SendOrPostCallback((u) =>
            {
                var parameter = u as IClosableContent;
                var model = new LoadingDialogViewmodel();
                model.dispatcher = dispatcher;
                var view = new LoadingDialogView();
                if (parameter is IProgressReportable)
                {
                  var  reporter = parameter as IProgressReportable;
                  reporter.Progress += model.Handler;
                   // reporter.Abort+=model.
                }
                parameter.CloseContent += model_Close;
                view.DataContext = model;
                Activate(view);
            }),Content);  
        }
    }
}
