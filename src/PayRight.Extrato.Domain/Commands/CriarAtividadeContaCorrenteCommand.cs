using Flunt.Notifications;
using Flunt.Validations;
using PayRight.Extrato.Domain.Enums;
using PayRight.Shared.Commands;
using PayRight.Shared.Utils.Extentions;

namespace PayRight.Extrato.Domain.Commands;

public class CriarAtividadeContaCorrenteCommand : Notifiable<Notification>, ICommand
{
    public Guid ContaCorrenteId { get; }

    public Guid UsuarioId { get; }

    public string NomeAtividade { get; }

    public string? DescricaoAtividade { get; }

    public decimal Valor { get; }

    public TipoAtividade TipoAtividade { get; }

    public DateOnly DataEstimado { get; }

    public CriarAtividadeContaCorrenteCommand(Guid contaCorrenteId, Guid usuarioId, string nomeAtividade, string? descricaoAtividade, decimal valor, TipoAtividade tipoAtividade, DateOnly dataEstimado)
    {
        ContaCorrenteId = contaCorrenteId;
        UsuarioId = usuarioId;
        NomeAtividade = nomeAtividade;
        DescricaoAtividade = descricaoAtividade;
        Valor = valor;
        TipoAtividade = tipoAtividade;
        DataEstimado = dataEstimado;
        
        Validar();
    }
    
    public void Validar()
    {
        AddNotifications(
            new Contract<CriarAtividadeContaCorrenteCommand>()
                .Requires()
                .IsGuidNotEmpty(ContaCorrenteId, "", "")
                .IsGuidNotEmpty(UsuarioId, "", "")
                .IsGreaterOrEqualsThan(Valor, 0, "Valor", "Valor informado deve ser maior que zero")
                .IsNotNullOrEmpty(NomeAtividade, "Nome", "")
            );
    }
}