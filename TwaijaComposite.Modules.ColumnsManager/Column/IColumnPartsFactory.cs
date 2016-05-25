using System.Collections.Generic;
using TwaijaComposite.Modules.Common.DataInterfaces;
using System.Windows.Input;

namespace TwaijaComposite.Modules.ColumnsManager.Column
{
    public interface IColumnPartsFactory
    {
        string FactoryName { get; }
        IRequest CreateRequest(IUser user,Dictionary<string,object> parameters);
        IFilter CreateFilter(IUser user);
        IList<ICommand> CreateCommandList(IColumn column);
        string InitializeHeader(Dictionary<string,object> parameters);
    }
}
