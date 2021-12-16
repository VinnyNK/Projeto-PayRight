namespace PayRight.Shared.Commands;

public interface ICommandResult
{
    public bool Sucesso { get; }

    public string Mensagem { get; }
    
}