using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;
using Microsoft.Practices.Unity;

namespace TwaijaComposite.Modules.ColumnsManager.Converter
{
    public class AggregateMessageConverter<T, B> : IConverter<AggregateMessage<T>, IList<B>> where T:ISetable<B>,new()
    {
        IModelFactory<T> _factory;
        [InjectionConstructor]
        public AggregateMessageConverter(IModelFactory<T> factory)
        {
            this._factory = factory;
        }
        public AggregateMessageConverter()
        {

        }
        public AggregateMessage<T> Convert(IList<B> paramter)
        {
            if (paramter == null)
            {
                return null;
            }          
            IList<SingleMessage<T>> list = new List<SingleMessage<T>>();
            foreach (B value in paramter)
            {
                T data = default(T);
                if (_factory != null)
                {
                    data = _factory.Create(value);
                }
                else
                {
                    data = new T();
                }
                data.Set(value);
                list.Add(new SingleMessage<T>(data));
            }

            return new AggregateMessage<T>(list);
        }


        public void SetFactory<V>(IModelFactory<V> factory) where V : class,new()
        {
            if (!typeof(V).Equals(typeof(T)))
            {
                throw new ArgumentException("The wrong type of factory was passed ");
            }
            _factory = factory as IModelFactory<T>;
        }
    }
}
