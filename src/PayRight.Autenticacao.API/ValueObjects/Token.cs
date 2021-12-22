using Flunt.Validations;
using PayRight.Shared.ValueObjects;

namespace PayRight.Autenticacao.API.ValueObjects;

public class Token : ValueObject
{
    public string Valor { get; }

    public DateTime CriadoEm { get; }

    public DateTime ValidoAte { get; }

    public Token(string valor, DateTime validoAte)
    {
        Valor = valor;
        ValidoAte = validoAte;
        CriadoEm = DateTime.Now;
        
        AddNotifications(
            new Contract<Token>()
                .Requires()
                .IsNotNullOrEmpty(Valor, "Token.Value", "Valor do token deve ser informado")
                .Matches(Valor, $@"^[A-Za-z0-9-_=]+\.[A-Za-z0-9-_=]+\.?[A-Za-z0-9-_.+/=]*$", "Token.Value", "Valor informado nao eh um JWT valido")
                
                .IsNotNull(ValidoAte, "Token.ValidoAte", "Deve ser informado data de validate do token")
                .IsGreaterThan(ValidoAte, CriadoEm, "Token.ValidoAte", "Validade do token deve ser maior do que a sua criacao")
            );
    }

}