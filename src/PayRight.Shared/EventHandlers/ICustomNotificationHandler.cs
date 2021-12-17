using MediatR;
using PayRight.Shared.EventNotifications;

namespace PayRight.Shared.EventHandlers;

public interface ICustomNotificationHandler<in T> : INotificationHandler<T> where T : ICustomNotification
{
}