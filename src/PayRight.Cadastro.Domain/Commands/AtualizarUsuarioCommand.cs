using Flunt.Notifications;
using Flunt.Validations;
using PayRight.Cadastro.Domain.ValueObjects;
using PayRight.Shared.Commands;
using PayRight.Shared.Utils.Extentions;

namespace PayRight.Cadastro.Domain.Commands;

public class AtualizarUsuarioCommand : Notifiable<Notification>, ICommand
{
    public Guid Id { get; }

    public string? PrimeiroNome { get; }

    public string? Sobrenome { get; }

    public string? EnderecoEmail { get; }
    
    public AtualizarUsuarioCommand(Guid id, string primeiroNome, string sobrenome, string enderecoEmail)
    {
        Id = id;
        PrimeiroNome = primeiroNome;
        Sobrenome = sobrenome;
        EnderecoEmail = enderecoEmail;
        
        Validar();
    }

    public void Validar()
    {
        AddNotifications(
            new Contract<AtualizarUsuarioCommand>()
            .Requires()
            .LengthInBetween(PrimeiroNome, NomeCompleto.MIN_CARACTERES, NomeCompleto.MAX_CARACTERES, $"{nameof(NomeCompleto)}.{nameof(PrimeiroNome)}", 
                $"{nameof(PrimeiroNome)} deve estar entre {NomeCompleto.MIN_CARACTERES} e {NomeCompleto.MAX_CARACTERES} caracteres")
            .IsNotNullOrWhiteSpace(PrimeiroNome, $"{nameof(NomeCompleto)}.{nameof(PrimeiroNome)}", 
                $"{nameof(PrimeiroNome)} nao pode estar em branco")
            .IsNotContainSpace(PrimeiroNome, $"{nameof(NomeCompleto)}.{nameof(PrimeiroNome)}",
                $"{nameof(PrimeiroNome)} nao pode conter espaco")
            .LengthInBetween(Sobrenome, NomeCompleto.MIN_CARACTERES, NomeCompleto.MAX_CARACTERES, $"{nameof(NomeCompleto)}.{nameof(Sobrenome)}", 
                $"{nameof(Sobrenome)} deve estar entre {NomeCompleto.MIN_CARACTERES} e {NomeCompleto.MAX_CARACTERES} caracteres")
            .IsNotNullOrWhiteSpace(Sobrenome, $"{nameof(NomeCompleto)}.{nameof(Sobrenome)}", 
                $"{nameof(Sobrenome)} nao pode estar em branco")
                
            .IsNotNullOrEmpty(EnderecoEmail, $"{nameof(Email)}.{nameof(EnderecoEmail)}",
                "Endereco de e-mail deve ser preenchido")
            .IsEmail(EnderecoEmail, $"{nameof(Email)}.{nameof(EnderecoEmail)}",
                "Endereco de e-mail informado esta invalido")
            .LengthInBetween(EnderecoEmail, Email.MIN_CARACTERES, Email.MAX_CARACTERES, $"{nameof(Email)}.{nameof(EnderecoEmail)}", 
                $"E-mail deve estar entre {Email.MIN_CARACTERES} e {Email.MAX_CARACTERES} caracteres")
        );
    }
}