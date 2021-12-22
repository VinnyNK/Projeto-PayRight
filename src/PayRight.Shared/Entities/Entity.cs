using Flunt.Notifications;

namespace PayRight.Shared.Entities;

public abstract class Entity : Notifiable<Notification>, IEntity
{
    public Guid Id { get; }
    
    public DateTime CriadoEm { get; }

    protected Entity()
    {
        Id = Guid.NewGuid();
        CriadoEm = DateTime.Now;
    }

    public abstract void Validar();
}