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

    public async Task<ContaCorrenteDTO?> BuscarContaCorrente(Guid usuarioId, Guid contaCorrenteId)
    {
        var conta = await _contaCorrenteLeituraRepository.BuscarContaCorrente(usuarioId, contaCorrenteId);

        return new ContaCorrenteDTO()
        {
            Id = conta!.Id,
            Nome = conta.NomeConta.Nome,
            Apelido = conta.NomeConta.Apelido,
            Ativo = conta.Ativo,
            Saldo = conta.Saldo
        };
    }

    public async Task<bool> ValidaContaCorrenteComUsuario(Guid contaCorrenteId, Guid usuarioId)
    {
        return await _contaCorrenteLeituraRepository.ExisteContaCorrente(contaCorrenteId, usuarioId);
    }
}