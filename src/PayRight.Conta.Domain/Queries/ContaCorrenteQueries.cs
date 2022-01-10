﻿using PayRight.Conta.Domain.Queries.DTOs;
using PayRight.Conta.Domain.Repositories;

namespace PayRight.Conta.Domain.Queries;

public class ContaCorrenteQueries : IContaCorrenteQueries
{
    private readonly IContaCorrenteLeituraRepository _contaCorrenteLeituraRepository;

    public ContaCorrenteQueries(IContaCorrenteLeituraRepository contaCorrenteLeituraRepository)
    {
        _contaCorrenteLeituraRepository = contaCorrenteLeituraRepository;
    }

    public async Task<IEnumerable<ContaCorrenteDTO>> BuscarContasCorrentes(Guid usuarioId)
    {
        var contas = await _contaCorrenteLeituraRepository.BuscarContasCorrente(usuarioId);

        return contas.Select(_ => new ContaCorrenteDTO()
        {
            Id = _.Id,
            Ativo = _.Ativo,
            Nome = _.NomeConta.Nome,
            Apelido = _.NomeConta.Apelido,
            Saldo = _.Saldo
        });
    }
}