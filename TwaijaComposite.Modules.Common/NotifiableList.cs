using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.Common
{
    public enum ListChangedEventType
    {
        Add,Insert,Remove
    }
    public delegate void CollectionChangedHandler(object changeditem,ListChangedEventType change);
#if !SILVERLIGHT
    [Serializable]
#endif
    public class NotifiableList<T>:List<T>
    {
#if !SILVERLIGHT
    [field:NonSerialized]
#endif
        public event CollectionChangedHandler CollectionChanged = (p,a) => { };
        protected void OnCollectionChanged(object item, ListChangedEventType type)
        {
            try
            {
                if (CollectionChanged != null)
                {
                    CollectionChanged(item, type);
                }
            }
            catch
            {

            }
        }
        public new void Add(T o)
        {        
            base.Add(o);
            OnCollectionChanged(o, ListChangedEventType.Add); 
        }
        public new bool Remove(T o)
        {
            OnCollectionChanged(o, ListChangedEventType.Remove);
           return base.Remove(o);
        }
        public new void Insert(int index,T o)
        {
            base.Insert(index,o);
            OnCollectionChanged(o, ListChangedEventType.Insert);
        }
    }
}
