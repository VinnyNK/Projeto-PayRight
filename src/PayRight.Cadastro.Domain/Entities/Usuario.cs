using Flunt.Validations;
using PayRight.Cadastro.Domain.ValueObjects;
using PayRight.Shared.Entities;
using PayRight.Shared.Utils.Extentions;

namespace PayRight.Cadastro.Domain.Entities;

public class Usuario : Entity
{
    public const int MIN_CARACTERES = 6;
    public const int MAX_CARACTERES = 128;
    
    public NomeCompleto NomeCompleto { get; private set; }

    public Email NomeUsuario { get; private set; }

    public Documento Documento { get; private set; }

    public string Senha { get; private set; }

    public string? ConfirmacaoSenha { get; private set; }

    public bool Ativo { get; private set; }

    public DateTime UltimaAtualizacaoEm { get; private set; }

    public Usuario(NomeCompleto nomeCompleto, Email nomeUsuario, Documento documento, string senha, string confirmacaoSenha)
    {
        NomeCompleto = nomeCompleto;
        NomeUsuario = nomeUsuario;
        Documento = documento;
        Senha = senha;
        ConfirmacaoSenha = confirmacaoSenha;
        Ativo = true;
        UltimaAtualizacaoEm = DateTime.Now;
        
        AddNotifications(
            new Contract<Usuario>()
                .Requires()
                .Join(ContratoValidacaoSenha())
        );
        
        Validar();
        
        if(IsValid)
            EncriptarSenha();
    }

    public void AlterarEmail(Email email)
    {
        NomeUsuario = email;
        
        Validar();
        if(IsValid)
            UltimaAtualizacaoEm = DateTime.Now;
    }

    public void AlterarNomeCompleto(NomeCompleto nomeCompleto)
    {
        NomeCompleto = nomeCompleto;

        Validar();
        if(IsValid)
            UltimaAtualizacaoEm = DateTime.Now;
    }

    public void DesabilitarUsuario()
    {
        Ativo = false;
        UltimaAtualizacaoEm = DateTime.Now;
    }
    
    public void HabilitarUsuario()
    {
        Ativo = true;
        UltimaAtualizacaoEm = DateTime.Now;
    }

    public bool ValidarSenha(string senha)
    {
        return BCrypt.Net.BCrypt.Verify(senha, Senha);
    }

    public sealed override void Validar()
    {
        AddNotifications(NomeCompleto, NomeUsuario, Documento);
    }
    
    private void EncriptarSenha()
    {
        if (!string.IsNullOrEmpty(Senha) && Senha == ConfirmacaoSenha)
            Senha = BCrypt.Net.BCrypt.HashPassword(Senha);
    }

    private Contract<Usuario> ContratoValidacaoSenha()
    {
        return new Contract<Usuario>()
            .IsNotNullOrEmpty(Senha, $"{nameof(Usuario)}.{nameof(Senha)}",
                $"{nameof(Senha)} deve ser preenchida")
            .IsNotContainSpace(Senha, $"{nameof(Usuario)}.{nameof(Senha)}",
                $"{nameof(Senha)} nao pode conter espaco")
            .LengthInBetween(Senha, MIN_CARACTERES, MAX_CARACTERES, $"{nameof(Usuario)}.{nameof(Senha)}",
                $"{nameof(Senha)} deve conter entre {MIN_CARACTERES} e {MAX_CARACTERES} caracteres")
            .PasswordStrength(Senha, $"{nameof(Usuario)}.{nameof(Senha)}",
                $"{nameof(Senha)} esta fraca. Deve conter no minimo:\n um caracter Maiuculo,\n um caracter Minusculo,\n um caracter Especial,\n um Digito")
            .AreEquals(Senha, ConfirmacaoSenha, $"{nameof(Usuario)}.{nameof(ConfirmacaoSenha)}",
                "Senhas nao sao iguais");
    }
}