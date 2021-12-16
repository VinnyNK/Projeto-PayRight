using Flunt.Validations;
using PayRight.Shared.Utils.Extentions;
using PayRight.Shared.ValueObjects;

namespace PayRight.Cadastro.Domain.ValueObjects;

public class NomeCompleto : ValueObject
{
    public const int MIN_CARACTERES = 3;
    public const int MAX_CARACTERES = 64;

    public string PrimeiroNome { get; private set; }
    public string Sobrenome { get; private set; }

    public NomeCompleto(string primeiroNome, string sobrenome)
    {
        PrimeiroNome = primeiroNome;
        Sobrenome = sobrenome;

        AddNotifications(
            new Contract<NomeCompleto>()
                .Requires()
                .LengthInBetween(PrimeiroNome, MIN_CARACTERES, MAX_CARACTERES, $"{nameof(NomeCompleto)}.{nameof(PrimeiroNome)}", 
                    $"{nameof(PrimeiroNome)} deve estar entre {MIN_CARACTERES} e {MAX_CARACTERES} caracteres")
                .IsNotNullOrWhiteSpace(PrimeiroNome, $"{nameof(NomeCompleto)}.{nameof(PrimeiroNome)}", 
                    $"{nameof(PrimeiroNome)} nao pode estar em branco")
                .LengthInBetween(Sobrenome, MIN_CARACTERES, MAX_CARACTERES, $"{nameof(NomeCompleto)}.{nameof(Sobrenome)}", 
                    $"{nameof(Sobrenome)} deve estar entre {MIN_CARACTERES} e {MAX_CARACTERES} caracteres")
                .IsNotNullOrWhiteSpace(Sobrenome, $"{nameof(NomeCompleto)}.{nameof(Sobrenome)}", 
                    $"{nameof(Sobrenome)} nao pode estar em branco")
                .IsNotContainSpace(PrimeiroNome, $"{nameof(NomeCompleto)}.{nameof(PrimeiroNome)}",
                    $"{nameof(PrimeiroNome)} nao pode conter espaco")
        );
    }
}