using PayRight.Extrato.Domain.Entities;

namespace PayRight.Extrato.Domain.Repositories;

public interface IAtividadeEscritaRepository
{
    Task AdicionaAtividade(Atividade atividade);
}