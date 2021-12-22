using FluentValidation;
using FluentValidation.Validators;

namespace PayRight.Autenticacao.API.DTOs;

public class LoginDto
{
    public string Email { get; set; } = null!;

    public string Senha { get; set; } = null!;
    
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(_ => _.Email)
                .NotEmpty()
                #pragma warning disable CS0618
                .EmailAddress(EmailValidationMode.Net4xRegex);
                #pragma warning restore CS0618

            RuleFor(_ => _.Senha)
                .NotEmpty();
        }
    }
}