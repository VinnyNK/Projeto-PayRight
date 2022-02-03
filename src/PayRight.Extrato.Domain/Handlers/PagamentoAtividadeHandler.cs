using Flunt.Notifications;
using PayRight.Extrato.Domain.Commands;
using PayRight.Extrato.Domain.Entities;
using PayRight.Extrato.Domain.Enums;
using PayRight.Extrato.Domain.GrpcService;
using PayRight.Extrato.Domain.UnitOfWork;
using PayRight.Shared.Commands;
using PayRight.Shared.Handlers;

namespace PayRight.Extrato.Domain.Handlers;

public class PagamentoAtividadeHandler: Notifiable<Notification>, IHandler<PagarAtividadeContaCorrenteCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IContaCorrenteGrpcService _contaCorrenteGrpcService;

    public PagamentoAtividadeHandler(IUnitOfWork unitOfWork, IContaCorrenteGrpcService contaCorrenteGrpcService)
    {
        _unitOfWork = unitOfWork;
        _contaCorrenteGrpcService = contaCorrenteGrpcService;
    }

    public async Task<ICommandResult> Handle(PagarAtividadeContaCorrenteCommand command, CancellationToken cancellationToken)
    {
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, "Problemas no comando", Notifications);
        }
        
        var atividade = await _unitOfWork.AtividadeLeituraRepository.BuscarAtividadeComExtrato(command.ContaCorrenteId, command.AtividadeId);
        if (atividade == null)
        {
            AddNotification("AtividadeId", "Atividade informada não existe ou Conta Corrente informada não pertence á atividade");
            return new CommandResult(false, "Atividade não existe", Notifications);
        }

        await ValidarSeContaCorrenteEhDoUsuario(command.ContaCorrenteId, command.UsuarioId);
        
        await PagarAtividade(atividade, command.ContaCorrenteId, command.UsuarioId);
        
        if (!IsValid)
            return new CommandResult(false, "Problemas para realizar o pagamento", Notifications);
        
        _unitOfWork.AtividadeEscritaRepository.AtualizarAtividade(atividade);
        var retorno = await _unitOfWork.Commit();

        return retorno
            ? new CommandResult(true, "Problemas para salvar no Banco de Dados", Notifications)
            : new CommandResult(false, "Atividade paga com sucesso", Notifications);
    }

    private async Task PagarAtividade(Atividade atividade, Guid contaCorrenteId, Guid usuarioId)
    {
        if (atividade.Pago)
        {
            AddNotification("Atividade", "Atividade informada ja consta como paga");
            return;
        }
        atividade.PagarAtividade();
        bool resultado;
        if (atividade.TipoAtividade == TipoAtividade.Receita)
            resultado = await _contaCorrenteGrpcService.SomarSaldoContaCorrente(usuarioId, contaCorrenteId, atividade.Valor);
        else 
            resultado = await _contaCorrenteGrpcService.SubtrairSaldoContaCorrente(usuarioId, contaCorrenteId, atividade.Valor);
        if (!resultado)
            AddNotification("ContaCorrente", "Problema para alterar valor na Conta Corrente");
    }

    private async Task ValidarSeContaCorrenteEhDoUsuario(Guid contaCorrenteId, Guid usuarioId)
    {
        var resultado = await
            _unitOfWork.ContaCorrenteExtratoLeituraRepository.VerificaSeContaCorrenteEhDoUsuario(contaCorrenteId,
                usuarioId);
        
        if (!resultado)
            AddNotification("ContaCorrente", "Conta corrente informada não pertence ao usuário");
    }
}