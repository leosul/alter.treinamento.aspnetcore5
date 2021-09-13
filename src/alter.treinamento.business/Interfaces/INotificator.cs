using alter.treinamento.business.Notifications;
using System.Collections.Generic;

namespace alter.treinamento.business.Interfaces
{
    public interface INotificator
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
