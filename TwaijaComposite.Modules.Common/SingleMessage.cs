using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;


namespace TwaijaComposite.Modules.Common
{
    public class SingleMessage<T> : IMessage 
    {
        T data;
        public SingleMessage(T data)
        {
            this.data = data;
        }
        public void AddToCollection(IList collection)
        {
            collection.Add(data);
        }


        public void InsertIntoCollection(IList collection)
        {
            collection.Insert(0,data);
        }


        public void RemoveFromSelf(IList collection)
        {
            //No Implementation for  a single object.
        }


        public bool Contains(object message)
        {
            return data.Equals(message);
        }

        public int Size
        {
            get
            {
                return (data != null) ? 1 : 0;
            }
        }
    }
}
