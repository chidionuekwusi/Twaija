using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;
using Microsoft.Practices.Unity;

namespace TwaijaComposite.Modules.ColumnsManager.Converter
{
    public class SingleMessageConverter<T, B> : IConverter<SingleMessage<T>, B> where T : ISetable<B>, new()
    {
        IModelFactory<T> factory;
        [InjectionConstructor]
        public SingleMessageConverter(IModelFactory<T> factory)
        {
            this.factory = factory;
        }
        public SingleMessageConverter()
        {

        }
        public SingleMessage<T> Convert(B parameter)
        {
            T data = default(T);
            if (factory == null)
            {
                data = new T();
            }
            else
            {
                data = factory.Create(parameter);
            }
            data.Set(parameter);
            return new SingleMessage<T>(data);
        }
        public void SetFactory<V>(IModelFactory<V> factory) where V : class,new()
        {
            if (!typeof(V).Equals(typeof(T)))
            {
                throw new ArgumentException("The wrong type of factory was passed ");
            }
            this.factory = factory as IModelFactory<T>;
        }
    }
}
