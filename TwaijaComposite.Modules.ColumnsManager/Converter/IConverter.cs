using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.ColumnsManager.Converter
{
    public interface IConverter<T,B>
    {
        T Convert(B paramter);
        void SetFactory<V>(IModelFactory<V> factory) where V : class,new();
    }
}
