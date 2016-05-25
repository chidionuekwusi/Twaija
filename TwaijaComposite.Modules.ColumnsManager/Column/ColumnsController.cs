using System;
using Microsoft.Practices.Unity;
//using Microsoft.Practices.Composite.Regions;
//using Microsoft.Practices.Composite.Events;
using TwaijaComposite.Modules.Common.Events;
using TwaijaComposite.Modules.ColumnsManager.Column;
using TwaijaComposite.Modules.ColumnsManager.Viewmodels;
using TwaijaComposite.Modules.Common.Preferencing;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.ColumnsManager.Views;
using TwaijaComposite.Modules.Common.Commands;
using TwaijaComposite.Modules.Common.DataInterfaces;
using System.Collections.Generic;
using TwaijaComposite.Modules.Common.Services;
using Microsoft.Practices.Prism.Events;
namespace TwaijaComposite.Modules.ColumnsManager
{
    public interface IColumnController : IController
    {
        void HandleCreateColumnEvent(CreateColumnEventArgs args);
    }
    public class ColumnsController:IColumnController
    {
        #region field
        private readonly IColumnsWorkspaceViewmodel ColumnsViewmodel;
        private readonly IEventAggregator m_EventAggr;
        private readonly IColumnResolutionService _cRService;
        private readonly Preferences pref;
        private ColumnWriter writer=new ColumnWriter();
        private ColumnParser parser=new ColumnParser();
        private InitializeUserHandlerRepository initializeUserHandlerRepository;
        #endregion

        [Dependency]
        public IDialogFacade Dialog { get; set; }

        #region Constructor
        
        public ColumnsController(IEventAggregator eventaggr, IColumnsWorkspaceViewmodel mainarea, IColumnResolutionService service,Preferences pref,InitializeUserHandlerRepository initializeUserHandlerRepository)
        {
            m_EventAggr = eventaggr;
            ColumnsViewmodel = mainarea;
            _cRService = service;
            this.pref = pref;
            this.initializeUserHandlerRepository = initializeUserHandlerRepository;
        }

        #endregion
        public void Initialize()
        {
            _cRService.Initialize();
            pref.TransparentUsersFacade.Userrepository.CollectionChanged += new CollectionChangedHandler(Userrepository_CollectionChanged);
            var evt = m_EventAggr.GetEvent<CreateColumnEvent>();
            var deletedUserevent = m_EventAggr.GetEvent<UserDeletedEvent>();
            deletedUserevent.Subscribe(ClearAssociatedColumns);
            evt.Subscribe(new Action<CreateColumnEventArgs>(HandleCreateColumnEvent), Microsoft.Practices.Prism.Events.ThreadOption.BackgroundThread, true);
            PopulateColumnsArea();
        }

        void Userrepository_CollectionChanged(object changeditem, ListChangedEventType change)
        {
            switch (change)
            {
                case ListChangedEventType.Add:
                    var user = changeditem as IUser;
                    initializeUserHandlerRepository.Resolve(user.GetType()).Initialize(user, this);
                    break;
            }
        }
        
        public void HandleCreateColumnEvent(CreateColumnEventArgs args) 
        {
               var product=_cRService.HandleEvent<IColumn>(args);
                if (product != null)
                {
                    if (args.User != null)
                    {
                        args.Parameters.Add("UserType", args.User.GetType().ToString());
                    }
                    var id = writer.CreateColumnString(args.ColumnBuildType, args.ColumnImpType, args.ColumnType, args.User.ScreenName, args.Parameters);
                    if (!pref.GeneralOptions.ColumnsCreated.Contains(id))
                    {
                        product.Owner = args.User;
                        product.Close += product_Close;
                        ColumnsViewmodel.Add(product);
                        pref.GeneralOptions.ColumnsCreated.Add(id);
                        product.IdentityString = id;
                        product.Initialize();
                    }
                }
        }

        void product_Close(object sender, EventArgs e)
        {
            if (sender != null)
            {
                var product = sender as IColumn;
                Dialog.PushYesNoDecisionDialog("Do u want to close this column:" + product.Header + "?", (o) =>
                {
                    var column = o as IColumn;
                    product.Close -= product_Close;
                    ColumnsViewmodel.Remove(product);
                    pref.GeneralOptions.ColumnsCreated.Remove(product.IdentityString);
                    product.Dispose();
                }, false);
            }
        }
         void PopulateColumnsArea()
        {
            foreach (string id in pref.GeneralOptions.ColumnsCreated)
            {

                var args = Parse(id);

                var product = _cRService.HandleEvent<IColumn>(args);
                if (product != null)
                {
                    product.Owner = args.User;
                    product.IdentityString = id;
                    product.Close += this.product_Close;
                    ColumnsViewmodel.Add(product);
                    product.Initialize();
                }
            }
            if (pref.GeneralOptions.ColumnsCreated.Count == 0)
            {
                foreach (IUser user in pref.TransparentUsersFacade.Userrepository.Users)
                {
                   initializeUserHandlerRepository.Resolve(user.GetType()).Initialize(user, this);
                }
            }
        }
        
         CreateColumnEventArgs Parse(string id)
         {
             if (parser.Parse(id))
             {
                 IUser user = null;
                 if (parser.Parameters.ContainsKey("UserType"))
                 {
                     foreach (IUser u in pref.TransparentUsersFacade.Userrepository.Users)
                     {
                         if (u.GetType().ToString().Contains(parser.Parameters["UserType"].ToString())&&u.ScreenName.Contains(parser.User))
                         {
                             user = u;
                         }
                     }
                 }
                // var user = pref.TransparentUsersFacade.Userrepository[parser.User];
                 if (user == null)
                 {
                     throw new ArgumentNullException("Parsed Username does not exist... Error occured while attempting to parse user");
                 }
                 return new CreateColumnEventArgs() { ColumnBuildType = parser.ColumnBuildType, ColumnImpType = parser.ColumnImpType, ColumnType = parser.ColumnType, Parameters = parser.Parameters,User=user };
             }
             return null;
         }

         public void ClearAssociatedColumns(IUser user)
         {
             IList<IColumn> deleted = new List<IColumn>();
             foreach (IColumn column in ColumnsViewmodel.Content)
             {
                 if (column.Owner.Equals(user))
                 {
                     deleted.Add(column);
                    //ColumnsViewmodel.Remove(column);
                     column.Close -= product_Close;
                     pref.GeneralOptions.ColumnsCreated.Remove(column.IdentityString);
                     
                 }
             }
             if (deleted.Count > 0)
             {
                 foreach (IColumn column in deleted)
                 {
                     ColumnsViewmodel.Remove(column);
                     column.Dispose();
                 }
             }
         }
    }
}
