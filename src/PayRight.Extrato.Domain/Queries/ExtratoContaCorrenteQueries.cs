using PayRight.Extrato.Domain.Queries.DTOs;
using PayRight.Extrato.Domain.UnitOfWork;

namespace PayRight.Extrato.Domain.Queries;

public class ExtratoContaCorrenteQueries : IExtratoContaCorrenteQueries
{
    private readonly IUnitOfWork _unitOfWork;

    public ExtratoContaCorrenteQueries(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ExtratoContaCorrenteDTO?> BuscarExtrato(Guid extratoId, Guid usuarioId)
    {
        var extrato = await _unitOfWork.ContaCorrenteExtratoLeituraRepository.BuscaExtrato(extratoId, usuarioId);

        if (extrato == null)
            return null;
        
        return new ExtratoContaCorrenteDTO()
        {
            Id = extrato.Id,
            ContaCorrenteId = extrato.ContaCorrenteId,
            Mes = extrato.PeriodoExtrato.Mes,
            Ano = extrato.PeriodoExtrato.Ano,
            Total = extrato.Total,
            TotalEstimado = extrato.TotalEstimado,
            Atividades = extrato.Atividades.Select(_ => new AtividadeDTO()
            {
                Id = _.Id,
                Pago = _.Pago,
                NomeAtividade = _.NomeAtividade.Nome,
                DescricaoAtividade = _.NomeAtividade.Descricao,
                Valor = _.Valor,
                TipoAtividade = _.TipoAtividade.ToString(),
                EstimativaPagamento = _.EstimativaPagamento,
                DataPagamento = _.DataPagamento
            })
        };
    }

    public async Task<ExtratoContaCorrenteDTO?> BuscaExtratoPorData(Guid contaCorrenteId, int mes, int ano)
    {
        var extrato =
            await _unitOfWork.ContaCorrenteExtratoLeituraRepository.BuscaExtratoPorData(contaCorrenteId, mes, ano);

        if (extrato == null)
            return null;
        
        return new ExtratoContaCorrenteDTO()
        {
            Id = extrato.Id,
            ContaCorrenteId = extrato.ContaCorrenteId,
            Mes = extrato.PeriodoExtrato.Mes,
            Ano = extrato.PeriodoExtrato.Ano,
            Total = extrato.Total,
            TotalEstimado = extrato.TotalEstimado,
            Atividades = extrato.Atividades.Select(_ => new AtividadeDTO()
            {
                Id = _.Id,
                Pago = _.Pago,
                NomeAtividade = _.NomeAtividade.Nome,
                DescricaoAtividade = _.NomeAtividade.Descricao,
                Valor = _.Valor,
                TipoAtividade = _.TipoAtividade.ToString(),
                EstimativaPagamento = _.EstimativaPagamento,
                DataPagamento = _.DataPagamento
            })
        };
    }
}