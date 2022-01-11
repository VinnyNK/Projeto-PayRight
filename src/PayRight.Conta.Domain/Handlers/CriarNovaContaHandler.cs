using Flunt.Notifications;
using PayRight.Conta.Domain.Commands;
using PayRight.Conta.Domain.Entities;
using PayRight.Conta.Domain.Repositories;
using PayRight.Shared.Commands;
using PayRight.Shared.Handlers;
using PayRight.Shared.Mediator;

namespace PayRight.Conta.Domain.Handlers;

public class CriarNovaContaHandler : Notifiable<Notification>, IHandler<CriarNovaContaCorrenteCommand>
{
    private readonly IContaCorrenteLeituraRepository _contaCorrenteLeituraRepository;
    private readonly IContaCorrenteEscritaRepository _contaCorrenteEscritaRepository;
    private IMediatorHandler _mediatorHandler;

    public CriarNovaContaHandler(IContaCorrenteLeituraRepository contaCorrenteLeituraRepository, IContaCorrenteEscritaRepository contaCorrenteEscritaRepository, IMediatorHandler mediatorHandler)
    {
        _contaCorrenteLeituraRepository = contaCorrenteLeituraRepository;
        _contaCorrenteEscritaRepository = contaCorrenteEscritaRepository;
        _mediatorHandler = mediatorHandler;
    }

    public async Task<ICommandResult> Handle(CriarNovaContaCorrenteCommand command, CancellationToken cancellationToken)
    {
        command.Validar();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, "Problemas nos dados informados para criar a conta corrente", Notifications);
        }

        if (await _contaCorrenteLeituraRepository.NomeContaExisteParaUsuario(command.UsuarioId, command.Nome))
            AddNotification("NomeConta", "Nome da conta ja utilizada");

        var contaCorrente = new ContaCorrente(command.UsuarioId, command.Nome, command.Apelido);
        
        contaCorrente.SomarSaldo(command.SaldoInicial);
        
        AddNotifications(contaCorrente);
        
        if (!IsValid)
            return new CommandResult(false, "Problemas nos dados informados para criar a conta corrente", Notifications);

        await _contaCorrenteEscritaRepository.CriarContaCorrente(contaCorrente);

        var retorno = await _contaCorrenteEscritaRepository.Commit();

        return retorno
            ? new CommandResult(true, "Usuario criado com sucesso", Notifications)
            : new CommandResult(false, "Problemas para criar a conta corrente", Notifications);
    }
}
