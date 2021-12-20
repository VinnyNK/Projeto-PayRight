using AutoMapper;
using PayRight.Cadastro.API.Controllers;
using PayRight.Cadastro.API.DTOs;
using PayRight.Cadastro.Domain.Commands;

namespace PayRight.Cadastro.API.Configurations;

public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        CreateMap<CriarNovoUsuarioRequestDto, CriarNovoUsuarioCpfCommand>()
            .ConstructUsing(_ => new CriarNovoUsuarioCpfCommand(
                _.PrimeiroNome, 
                _.Sobrenome, 
                _.EnderecoEmail, 
                _.NumeroDocumento, 
                _.Senha, 
                _.ConfirmacaoSenha)
            );
    }
}