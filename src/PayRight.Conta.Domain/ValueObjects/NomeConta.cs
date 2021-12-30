using Flunt.Validations;
using PayRight.Shared.Utils.Extentions;
using PayRight.Shared.ValueObjects;

namespace PayRight.Conta.Domain.ValueObjects;

public class NomeConta : ValueObject
{
    public const int MIN_CARACTERES_NOME = 3;
    public const int MAX_CARACTERES_NOME = 24;
    public const int MIN_CARACTERES_APELIDO = 1;
    public const int MAX_CARACTERES_APELIDO = 15;
    
    public string Nome { get; }

    public string? Apelido { get; }

    public NomeConta(string nome, string? apelido)
    {
        Nome = nome;
        Apelido = apelido;
        
        AddNotifications(
            new Contract<NomeConta>()
                .Requires()
                .IsNotNullOrEmpty(Nome, "NomeConta.Nome", "Nome da conta deve ser informado")
                .LengthInBetween(Nome, MIN_CARACTERES_NOME, MAX_CARACTERES_NOME, "NomeConta.Nome", $"Nome deve estar entre {MIN_CARACTERES_NOME} e {MAX_CARACTERES_NOME} caracteres")
        );

        if (!string.IsNullOrEmpty(Apelido))
        {
            AddNotifications(
                new Contract<NomeConta>()
                    .Requires()
                    .LengthInBetween(Apelido, MIN_CARACTERES_APELIDO, MAX_CARACTERES_APELIDO, "NomeConta.Apelido", $"Apelido deve estar entre {MIN_CARACTERES_APELIDO} e {MAX_CARACTERES_APELIDO} caracteres")
            );
        }
    }
}