using TwaijaComposite.Modules.Common;
namespace TwaijaComposite.Modules.ColumnsManager.Notifications
{
    public interface INotificationsController:IController
    {
        void RecieveNewMessage(Notice args);
    }
    
}
