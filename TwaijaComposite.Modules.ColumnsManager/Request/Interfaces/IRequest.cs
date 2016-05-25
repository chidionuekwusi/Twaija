using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.ColumnsManager
{
    public interface IRequest : IDisposable
        {
            void Start();
            void Restart();
            void Stop();
            event NewMessageHandler NewMessage;
        }
}
