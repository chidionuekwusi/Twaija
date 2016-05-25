using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.Common.Interfaces
{
    public class ObjectRepository<T, B>
        where T : IContainsObjectRepositoryKey<B>
        where B : class
    {
        private Dictionary<B, T> dictionary;
        public ObjectRepository(IEnumerable<T> source)
        {
            dictionary = new Dictionary<B, T>();
            foreach (T model in source)
            {
                dictionary.Add(model.Key, model);
            }

        }
        public T Resolve(B key)
        {
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }
            return default(T);
        }
    }
    public interface IContainsObjectRepositoryKey<B>
    {
        B Key { get; }
    }
}
