using Flunt.Validations;
using PayRight.Shared.Utils.Extentions;
using PayRight.Shared.ValueObjects;

namespace PayRight.Extrato.Domain.ValueObjects;

public class NomeAtividade : ValueObject
{
    public const int MIN_CARACTERES_NOME = 3;
    public const int MAX_CARACTERES_NOME = 32;
    public const int MIN_CARACTERES_DESCRICAO = 3;
    public const int MAX_CARACTERES_DESCRICAO= 128;
    
    public string Nome { get; private set; }
    
    public string? Descricao { get; private set; }

    public NomeAtividade(string nome, string? descricao)
    {
        Nome = nome;
        Descricao = descricao;
        
        Validar();
    }

    public sealed override void Validar()
    {
        AddNotifications(
            new Contract<NomeAtividade>()
                .Requires()
                .IsNotNullOrEmpty(Nome, "Nome", "")
                .LengthInBetween(Nome, MIN_CARACTERES_NOME, MAX_CARACTERES_NOME, "Nome", $"Nome da atividade deve ter entre {MIN_CARACTERES_NOME} e {MAX_CARACTERES_NOME} caracteres")
        );
        
        if (!string.IsNullOrEmpty(Descricao))
            AddNotifications(
                new Contract<NomeAtividade>()
                    .Requires()
                    .LengthInBetween(Descricao,  MIN_CARACTERES_DESCRICAO, MAX_CARACTERES_DESCRICAO, "Descricao", $"Descrição atividade deve ter entre {MIN_CARACTERES_DESCRICAO} e {MAX_CARACTERES_DESCRICAO} caracteres")
            );
    }
}