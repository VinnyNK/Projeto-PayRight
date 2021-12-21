using FluentValidation;

namespace PayRight.Cadastro.API.DTOs;

public class AtualizarUsuarioRequestDto
{
    public string? PrimeiroNome { get; set; }

    public string? Sobrenome { get; set; }

    public string? EnderecoEmail { get; set; }
    
    public class AtualizarUsuarioRequestValidator : AbstractValidator<AtualizarUsuarioRequestDto>
    {
        public AtualizarUsuarioRequestValidator()
        {
            When(_ => string.IsNullOrEmpty(_.PrimeiroNome), () =>
            {
                RuleFor(_ => _.PrimeiroNome).NotEmpty();
            });
            
            When(_ => string.IsNullOrEmpty(_.PrimeiroNome), () =>
            {
                RuleFor(_ => _.Sobrenome).NotEmpty();
            });
            
            When(_ => string.IsNullOrEmpty(_.PrimeiroNome), () =>
            {
                RuleFor(_ => _.EnderecoEmail).NotEmpty().EmailAddress();
            });
            
        }
    }
}