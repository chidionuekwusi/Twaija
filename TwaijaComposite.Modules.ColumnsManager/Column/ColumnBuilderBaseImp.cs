using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.ColumnsManager.Converter;

namespace TwaijaComposite.Modules.ColumnsManager.Column
{
    public class ColumnBuilderBaseImp:IColumnBuilder
    {
        readonly IUser user;
        readonly IColumn column;
        IColumnPartsFactory factory;
        Dictionary<string, object> parameters;
        public ColumnBuilderBaseImp(IColumn column,IUser user,IColumnPartsFactory factory,Dictionary<string,object> parameters=null)
        {
            if (column == null)
            {
                throw new ArgumentNullException("IColumn implementation must be passed");
            }
            if (user == null)
            {
                throw new ArgumentNullException("IUser must be passed");
            }
            if (factory == null)
            {
                throw new ArgumentNullException("IFactory implementation must be passed");
            }
            this.user = user;
            this.column = column;
            this.factory = factory;
            this.parameters = parameters;
        }
        public virtual void BuildRequest()
        {
            column.Request = factory.CreateRequest(user,parameters);
        }

        public virtual void BuildFilter()
        {
            column.Filter = factory.CreateFilter(user);           
        }

        public virtual void BuildCommands()
        {
            column.Commands = factory.CreateCommandList(column);
        }

        public virtual void BuildInfrastructure()
        {
            column.Header= factory.InitializeHeader(parameters);
            //Hook directly to Request object 
            if (column.Request != null)
            {
                column.Request.NewMessage += column.RecieveNewMessages;
            }
        }
        public void SwitchFactory(IColumnPartsFactory factory)
        {
            this.factory = factory;
        }

        public string BuilderName
        {
            get { return "ColumnBuilderBaseImp"; }
        }
        public IColumn GetProduct()
        {
            return column;
        }

    }
}
