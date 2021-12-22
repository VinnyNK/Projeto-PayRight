using Flunt.Validations;
using PayRight.Shared.Entities;
using PayRight.Shared.Utils.Extentions;

namespace PayRight.Autenticacao.API.Models;

public class Usuario : Entity
{
    public string Email { get; }

    public string Senha { get; private set; }
    
    protected Usuario() {}

    public Usuario(string email, string senha)
    {
        Email = email;
        Senha = senha;
        
        Validar();
    }
    
    public virtual bool ValidarSenha(string senha)
    {
        return BCrypt.Net.BCrypt.Verify(senha, Senha);
    }

    public sealed override void Validar()
    {
        AddNotifications(new Contract<Usuario>()
            .Requires()
            .IsNotNullOrEmpty(Email, $"{nameof(Email)}.{nameof(Email)}",
                "Endereco de e-mail deve ser preenchido")
            .IsEmail(Email, $"{nameof(Email)}.{nameof(Email)}",
                "Endereco de e-mail informado esta invalido")
            .IsNotNullOrEmpty(Senha, $"{nameof(Usuario)}.{nameof(Senha)}",
                $"{nameof(Senha)} deve ser preenchida")
            .IsNotContainSpace(Senha, $"{nameof(Usuario)}.{nameof(Senha)}",
                $"{nameof(Senha)} nao pode conter espaco"));
    }
}