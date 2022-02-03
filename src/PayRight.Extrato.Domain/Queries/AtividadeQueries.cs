using PayRight.Extrato.Domain.Queries.DTOs;
using PayRight.Extrato.Domain.UnitOfWork;

namespace PayRight.Extrato.Domain.Queries;

public class AtividadeQueries : IAtividadeQueries
{

    private readonly IUnitOfWork _unitOfWork;

    public AtividadeQueries(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AtividadeDTO?> BuscaAtividadePorId(Guid atividadeId, Guid usuarioId, Guid contaCorrenteId)
    {
        var atividade = await _unitOfWork.AtividadeLeituraRepository.BuscarAtividadePorId(atividadeId, usuarioId, contaCorrenteId);

        if (atividade == null)
            return null;

        return new AtividadeDTO()
        {
            Id = atividade.Id,
            Pago = atividade.Pago,
            Valor = atividade.Valor,
            DataPagamento = atividade.DataPagamento,
            EstimativaPagamento = atividade.EstimativaPagamento,
            NomeAtividade = atividade.NomeAtividade.Nome,
            DescricaoAtividade = atividade.NomeAtividade.Descricao,
            TipoAtividade = atividade.TipoAtividade.ToString()
        };
    }
}