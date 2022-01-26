using Flunt.Validations;
using PayRight.Shared.Utils.Extentions;
using PayRight.Shared.ValueObjects;

namespace PayRight.Cadastro.Domain.ValueObjects;

public class Email : ValueObject
{
    public const int MIN_CARACTERES = 3;
    
    public const int MAX_CARACTERES = 80;
    
    public string Endereco { get; private set; }

    public Email(string endereco)
    {
        Endereco = endereco;

        Validar();
    }

    public sealed override void Validar()
    {
        AddNotifications(
            new Contract<Email>()
                .Requires()
                .IsNotNullOrEmpty(Endereco, $"{nameof(Email)}.{nameof(Endereco)}",
                    "Endereco de e-mail deve ser preenchido")
                .IsEmail(Endereco, $"{nameof(Email)}.{nameof(Endereco)}",
                    "Endereco de e-mail informado esta invalido")
                .LengthInBetween(Endereco, MIN_CARACTERES, MAX_CARACTERES, $"{nameof(Email)}.{nameof(Endereco)}", 
                    $"E-mail deve estar entre {MIN_CARACTERES} e {MAX_CARACTERES} caracteres")
        );
    }
}