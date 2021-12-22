using AutoMapper;
using PayRight.Autenticacao.API.DTOs;
using PayRight.Autenticacao.API.ValueObjects;

namespace PayRight.Autenticacao.API.Configurations;

public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        CreateMap<Token, TokenResponseDto>()
            .ForMember(_ => _.Token, opts => 
                opts.MapFrom(_ => _.Valor));
    }
}