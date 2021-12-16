using Flunt.Notifications;
using Flunt.Validations;
using PayRight.Cadastro.Domain.Entities;
using PayRight.Cadastro.Domain.ValueObjects;
using PayRight.Shared.Commands;
using PayRight.Shared.Utils.Extentions;

namespace PayRight.Cadastro.Domain.Commands;

public class CriarNovoUsuarioCnpjCommand : Notifiable<Notification>, ICommand
{
    public string PrimeiroNome { get; }
    
    public string Sobrenome { get; }

    public string EnderecoEmail { get; }
    
    public string NumeroDocumento { get; }

    public string Senha { get; }

    public string ConfirmacaoSenha { get; }
    
    public CriarNovoUsuarioCnpjCommand(string primeiroNome, string sobrenome, string enderecoEmail, string numeroDocumento, string senha, string confirmacaoSenha)
    {
        PrimeiroNome = primeiroNome;
        Sobrenome = sobrenome;
        EnderecoEmail = enderecoEmail;
        NumeroDocumento = numeroDocumento;
        Senha = senha;
        ConfirmacaoSenha = confirmacaoSenha;
        
        Validar();
    }

    public void Validar()
    {
        AddNotifications(
            new Contract<CriarNovoUsuarioCpfCommand>()
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
                
                .IsNotNullOrEmpty(NumeroDocumento, $"{nameof(Documento)}.{nameof(NumeroDocumento)}",
                        "Numero do Documento deve ser preenchido")

                .IsNotNullOrEmpty(Senha, $"{nameof(Usuario)}.{nameof(Senha)}", 
                    $"{nameof(Senha)} deve ser preenchida")
                .LengthInBetween(Senha, Usuario.MIN_CARACTERES, Usuario.MAX_CARACTERES, $"{nameof(Usuario)}.{nameof(Senha)}",
                    $"{nameof(Senha)} deve conter entre {Usuario.MIN_CARACTERES} e {Usuario.MAX_CARACTERES} caracteres")
            
                .IsNotNullOrEmpty(ConfirmacaoSenha, $"{nameof(Usuario)}.{nameof(ConfirmacaoSenha)}", 
                    $"{nameof(ConfirmacaoSenha)} deve ser preenchida")
                .LengthInBetween(Senha, Usuario.MIN_CARACTERES, Usuario.MAX_CARACTERES, $"{nameof(Usuario)}.{nameof(ConfirmacaoSenha)}",
                    $"{nameof(ConfirmacaoSenha)} deve conter entre {Usuario.MIN_CARACTERES} e {Usuario.MAX_CARACTERES} caracteres")
        );
    }
}