using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace TwaijaComposite.Modules.Common
{
    public interface IMessage
    {
        int Size { get; }
        void AddToCollection(IList collection);
        void InsertIntoCollection(IList collection);
        void RemoveFromSelf(IList collection);
        bool Contains(object message);
    }
}
