using Flunt.Notifications;

namespace PayRight.Shared.Services;

public abstract class ServiceBase : Notifiable<Notification>, IServiceBase
{
    public IReadOnlyCollection<Notification> ServiceNotifications  => Notifications;
}