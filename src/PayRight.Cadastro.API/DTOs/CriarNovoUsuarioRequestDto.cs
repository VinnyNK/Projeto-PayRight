using FluentValidation;

namespace PayRight.Cadastro.API.DTOs;

public class CriarNovoUsuarioRequestDto
{
    public string PrimeiroNome { get; set; }
    
    public string Sobrenome { get; set; }

    public string EnderecoEmail { get; set; }
    
    public string NumeroDocumento { get; set; }

    public string Senha { get; set; }

    public string ConfirmacaoSenha { get; set; }
    
    public class CriarNovoUsuarioRequestValidation : AbstractValidator<CriarNovoUsuarioRequestDto>
    {
        public CriarNovoUsuarioRequestValidation()
        {
            RuleFor(_ => _.PrimeiroNome)
                .NotEmpty();
            
            RuleFor(_ => _.Sobrenome)
                .NotEmpty();

            RuleFor(_ => _.EnderecoEmail)
                .NotEmpty();

            RuleFor(_ => _.Senha)
                .NotEmpty();

            RuleFor(_ => _.ConfirmacaoSenha)
                .NotEmpty();
        }
    }
}


