using System;
using System.Collections.Generic;
using System.Collections;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common.DataInterfaces;

namespace TwaijaComposite.Modules.ColumnsManager
{
   public interface IColumn:IHeaderAndContentObject
    {
        IUser Owner { get; set; }
        string IdentityString { get; set; }
        IFilter Filter { get; set; }
        IRequest Request { get; set; }
        NewMessageHandler RecieveNewMessages { get; }
    }
}
