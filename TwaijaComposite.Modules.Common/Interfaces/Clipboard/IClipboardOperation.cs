using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.Interfaces;

namespace TwaijaComposite.Modules.Clipboard
{
    public interface IClipboardOperation
    {
        bool Processed { get; set; }
        string OperationKey { get; }
        string OperationDescription { get;  }
        string PostMessageServiceKey { get;  }
        object Parameter { get; set; }
    }
}
