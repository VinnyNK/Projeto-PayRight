using Flunt.Notifications;
using PayRight.Extrato.Domain.Commands;
using PayRight.Extrato.Domain.Entities;
using PayRight.Extrato.Domain.GrpcService;
using PayRight.Extrato.Domain.UnitOfWork;
using PayRight.Shared.Commands;
using PayRight.Shared.Handlers;

namespace PayRight.Extrato.Domain.Handlers;

public class CriarAtividadeHandler : Notifiable<Notification>, IHandler<CriarAtividadeContaCorrenteCommand>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IContaCorrenteGrpcService _contaCorrenteGrpcService;

    public CriarAtividadeHandler(IUnitOfWork unitOfWork, IContaCorrenteGrpcService contaCorrenteGrpcService)
    {
        _unitOfWork = unitOfWork;
        _contaCorrenteGrpcService = contaCorrenteGrpcService;
    }

    public async Task<ICommandResult> Handle(CriarAtividadeContaCorrenteCommand command, CancellationToken cancellationToken)
    {
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, "Problema no comando", Notifications);
        }

        if (!await ValidarContaCorrenteEUsuario(command.ContaCorrenteId, command.UsuarioId))
        {
            AddNotification("ContaCorrente", "Conta Corrente informada não existe ou não pertence ao usuario");
            return new CommandResult(false, "Problema na validação", Notifications);
        }

        var atividade = new Atividade(command.NomeAtividade, command.DescricaoAtividade, command.Valor,
            command.TipoAtividade);
        AddNotifications(atividade);

        var extrato = await BuscarExtratoContaCorrente(command.ContaCorrenteId, command.UsuarioId, 
            command.DataEstimado.Month, command.DataEstimado.Year);

        if (!IsValid)
            return new CommandResult(false, "problemas", Notifications); 
        
        extrato.AdicionarAtividade(atividade);
        _unitOfWork.ContaCorrenteExtratoEscritaRepository.AtualizarExtrato(extrato);
        await _unitOfWork.AtividadeEscritaRepository.AdicionaAtividade(atividade);

        var retorno = await _unitOfWork.Commit();

        return retorno
            ? new CommandResult(true, "", Notifications)
            : new CommandResult(false, "", Notifications);
    }

    private async Task<ContaCorrenteExtrato> BuscarExtratoContaCorrente(Guid contaCorrenteId, Guid usuarioId, int mes, int ano)
    {
        var extrato = await _unitOfWork.ContaCorrenteExtratoLeituraRepository.BuscarExtratoPorMesEAno(contaCorrenteId,
            usuarioId, mes, ano);

        if (extrato != null) return extrato;
        
        extrato = new ContaCorrenteExtrato(contaCorrenteId, usuarioId, mes, ano);
        AddNotifications(extrato);
        if (!IsValid) return extrato;
        await _unitOfWork.ContaCorrenteExtratoEscritaRepository.AdicionaExtrato(extrato);
        await _unitOfWork.Commit();

        return extrato;
    }

    private async Task<bool> ValidarContaCorrenteEUsuario(Guid contaCorrenteId, Guid usuarioId)
    {
        var resultado = await _contaCorrenteGrpcService.ValidarContaCorrente(contaCorrenteId, usuarioId);
        return resultado.EhValido;
    }
}