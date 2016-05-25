using System;
using TwaijaComposite.Modules.Clipboard;

namespace TwaijaComposite.Modules.Common.Interfaces
{
    public interface IClipboardOperationAware
    {
        IClipboardOperation Operation { get; set; }
    }
}
