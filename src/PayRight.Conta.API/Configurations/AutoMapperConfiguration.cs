using AutoMapper;
using PayRight.Conta.API.DTOs;
using PayRight.Conta.Domain.Commands;
using PayRight.Conta.Domain.Queries.DTOs;

namespace PayRight.Conta.API.Configurations;

public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        CreateMap<ContaCorrenteDTO, ListarContasCorrentesResponse>();

        CreateMap<ContaCorrenteDTO, ContaCorrenteResponse>();

        CreateMap<ContaCorrenteRequest, CriarNovaContaCorrenteCommand>()
            .ConstructUsing(_ => new CriarNovaContaCorrenteCommand(
                _.UsuarioId,
                _.Nome!,
                _.Apelido,
                _.SaldoInicial
                ));
    }
}