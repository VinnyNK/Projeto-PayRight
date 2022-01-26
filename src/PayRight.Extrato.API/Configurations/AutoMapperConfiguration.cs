using AutoMapper;
using PayRight.Extrato.API.DTOs;
using PayRight.Extrato.Domain.Commands;

namespace PayRight.Extrato.API.Configurations;

public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        CreateMap<AtividadeContaCorrenteRequestDTO, CriarAtividadeContaCorrenteCommand>()
            .ConstructUsing(_ => new CriarAtividadeContaCorrenteCommand(
                _.ContaCorrenteId,
                _.UsuarioId,
                _.NomeAtividade,
                _.DescricaoAtividade,
                _.Valor,
                _.TipoAtividade,
                new DateOnly(_.DataEstimada.Year, _.DataEstimada.Month, _.DataEstimada.Day)
            ));
    }
}