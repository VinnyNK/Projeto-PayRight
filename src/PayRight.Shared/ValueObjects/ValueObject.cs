using Flunt.Notifications;

namespace PayRight.Shared.ValueObjects;

public abstract class ValueObject : Notifiable<Notification>
{
    protected ValueObject()
    {
    }

    public abstract void Validar();

}