using Flunt.Notifications;

namespace PayRight.Shared.Services;

public interface IServiceBase
{
    public IReadOnlyCollection<Notification> ServiceNotifications { get; }
}