using Flunt.Notifications;
using Flunt.Validations;
using PayRight.Conta.Domain.ValueObjects;
using PayRight.Shared.Commands;
using PayRight.Shared.Utils.Extentions;

namespace PayRight.Conta.Domain.Commands;

public class CriarNovaContaCorrenteCommand : Notifiable<Notification>, ICommand
{
    public Guid UsuarioId { get; }

    public string Nome { get; }

    public string? Apelido { get; }

    public decimal SaldoInicial { get; }


    public CriarNovaContaCorrenteCommand(Guid usuarioId, string nome, string? apelido, decimal saldoInicial = 0)
    {
        UsuarioId = usuarioId;
        Nome = nome;
        Apelido = apelido;
        SaldoInicial = saldoInicial;
        
        Validar();
    }

    public void Validar()
    {
        AddNotifications(
            new Contract<CriarNovaContaCorrenteCommand>()
                .Requires()
                .IsGuidNotEmpty(UsuarioId, "UsuarioId", "Id do usuario não foi informado ou esta invalido")
                .IsNotNullOrEmpty(Nome, "Nome", "Nome da conta deve ser informado")
                .LengthInBetween(Nome, NomeConta.MIN_CARACTERES_NOME, NomeConta.MAX_CARACTERES_NOME, "Nome", $"Nome deve estar entre {NomeConta.MIN_CARACTERES_NOME} e {NomeConta.MAX_CARACTERES_NOME} caracteres")
        );
        
        if (!string.IsNullOrEmpty(Apelido))
        {
            AddNotifications(
                new Contract<NomeConta>()
                    .Requires()
                    .LengthInBetween(Apelido, NomeConta.MIN_CARACTERES_APELIDO, NomeConta.MAX_CARACTERES_APELIDO, "Apelido", $"Apelido deve estar entre {NomeConta.MIN_CARACTERES_APELIDO} e {NomeConta.MAX_CARACTERES_APELIDO} caracteres")
            );
        }
    }
}