using System;
using System.Collections.Generic;
using TwaijaComposite.Modules.Common;
//using Microsoft.Practices.Composite.Events;
using TwaijaComposite.Modules.Common.Preferencing;
using TwaijaComposite.Modules.Common.Events;
using TwaijaComposite.Modules.Clipboard.Viewmodels;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.Common.Interfaces;

namespace TwaijaComposite.Modules.Clipboard
{
    public class ClipboardController:IController
    {
        private readonly IEnumerable<IPictureService> _serviceList;
        private readonly IClipboardViewmodel _clipboardViewmodel;
       // private readonly Preferences _pref;
        public ClipboardController(IEnumerable<IPictureService> services)
        {
            _serviceList = services;
            //_pref = pref;
        }
        public void Initialize()
        {
            /*Check the available picture services and populate the list in 
            Userfacade if it is empty */
            //if (_pref.TransparentUsersFacade.PictureServices.PictureServices.Count == 0)
            //{
            //    _pref.TransparentUsersFacade.PictureServices.FillPictureServices(_serviceList);
            //}

        }
    }
}
