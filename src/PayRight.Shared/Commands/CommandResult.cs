namespace PayRight.Shared.Commands;

public class CommandResult : ICommandResult
{
    public bool Sucesso { get; private set; }
    public string Mensagem { get; private set; }

    public CommandResult(bool sucesso, string mensagem)
    {
        Sucesso = sucesso;
        Mensagem = mensagem;
    }

}
