using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using TwaijaComposite.Modules.Common;

namespace TwaijaComposite.Modules.ColumnsManager
{
    public class AggregateMessage<T>:IMessage
    {
    
        private IList<SingleMessage<T>> components;
        public AggregateMessage(IList<SingleMessage<T>> list)
        {
            components =list;
        }
        public void AddToCollection(IList collection)
        {
            lock (collection)
            {
                foreach (IMessage comp in components)
                {
                    comp.AddToCollection(collection);
                }
            }
        }


        public void InsertIntoCollection(IList collection)
        {
            lock (collection)
            {
                foreach (IMessage comp in components)
                {
                    comp.InsertIntoCollection(collection);
                }
            }
        }


        public void RemoveFromSelf(IList collection)
        {
            lock (collection)
            {
                foreach (object state in collection)
                {
                    foreach (SingleMessage<T> comp in components.ToList())
                    {
                        if (comp.Contains(state))
                        {
                            this.components.Remove(comp);
                        }
                    }
                }
            }
        }


        public bool Contains(object message)
        {
            //No implementation for this type of Message
            return false;
        }

        public int Size
        {
            get { if(components!=null)
                return components.Count;

            return 0;
            }
        }
    }
}
