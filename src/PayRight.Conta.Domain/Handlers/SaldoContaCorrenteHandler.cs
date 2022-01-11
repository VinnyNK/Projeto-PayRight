using Flunt.Notifications;
using PayRight.Conta.Domain.Commands;
using PayRight.Conta.Domain.Repositories;
using PayRight.Shared.Commands;
using PayRight.Shared.Handlers;

namespace PayRight.Conta.Domain.Handlers;

public class SaldoContaCorrenteHandler : Notifiable<Notification>, IHandler<SomaSaldoContaCorrenteCommand>, IHandler<SubtrairSaldoContaCorrenteCommand>
{
    private readonly IContaCorrenteEscritaRepository _contaCorrenteEscritaRepository;
    private readonly IContaCorrenteLeituraRepository _contaCorrenteLeituraRepository;

    public SaldoContaCorrenteHandler(IContaCorrenteEscritaRepository contaCorrenteEscritaRepository, IContaCorrenteLeituraRepository contaCorrenteLeituraRepository)
    {
        _contaCorrenteEscritaRepository = contaCorrenteEscritaRepository;
        _contaCorrenteLeituraRepository = contaCorrenteLeituraRepository;
    }

    public async Task<ICommandResult> Handle(SomaSaldoContaCorrenteCommand command, CancellationToken cancellationToken)
    {
        command.Validar();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, "Problemas nos dados informados para somar saldo da conta corrente",
                Notifications);
        }

        var conta = await _contaCorrenteLeituraRepository.BuscaContaCorrente(command.UsuarioId, command.ContaCorrenteId);

        if (conta == null)
        {
            AddNotification("ContaCorrenteId", "Conta Corrente informada não existe ou não pertence ao usuário logado");
            return new CommandResult(false, "Id informado não encontrado", Notifications);
        }
        
        conta.SomarSaldo(command.Valor);
        
        AddNotifications(conta);

        if (!IsValid)
            return new CommandResult(false, "Problemas nos dados para somar saldo", Notifications);
        
        await _contaCorrenteEscritaRepository.AtualizarContaCorrente(conta);

        var retorno = await _contaCorrenteEscritaRepository.Commit();

        return retorno
            ? new CommandResult(true, "Saldo alterado com sucesso", Notifications)
            : new CommandResult(false, "Problemas para salvar alterações no banco de dados", Notifications);
    }

    public async Task<ICommandResult> Handle(SubtrairSaldoContaCorrenteCommand command, CancellationToken cancellationToken)
    {
        command.Validar();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, "Problemas nos dados informados para subtrair saldo da conta corrente",
                Notifications);
        }

        var conta = await _contaCorrenteLeituraRepository.BuscaContaCorrente(command.UsuarioId, command.ContaCorrenteId);

        if (conta == null)
        {
            AddNotification("ContaCorrenteId", "Conta Corrente informada não existe ou não pertence ao usuário logado");
            return new CommandResult(false, "Id informado não encontrado", Notifications);
        }
        
        conta.SubtrairSaldo(command.Valor);
        
        AddNotifications(conta);

        if (!IsValid)
            return new CommandResult(false, "Problemas nos dados para subtrair saldo", Notifications);
        
        await _contaCorrenteEscritaRepository.AtualizarContaCorrente(conta);

        var retorno = await _contaCorrenteEscritaRepository.Commit();

        return retorno
            ? new CommandResult(true, "Saldo alterado com sucesso", Notifications)
            : new CommandResult(false, "Problemas para salvar alterações no banco de dados", Notifications);
    }
}