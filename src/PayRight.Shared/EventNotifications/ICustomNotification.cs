using MediatR;

namespace PayRight.Shared.EventNotifications;

public interface ICustomNotification : INotification
{
    public Guid AggregateId { get; }
}