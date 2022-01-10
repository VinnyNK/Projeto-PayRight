﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using PayRight.Conta.Domain.Entities;
using PayRight.Conta.Domain.Queries;
using PayRight.Conta.Domain.Repositories;
using PayRight.Conta.Tests.TestesUnitarios.Entities.Fixtures;
using Xunit;

namespace PayRight.Conta.Tests.TestesUnitarios.Queries;

[Collection(nameof(ContaCorrenteCollection))]
public class ContaCorrenteQueriesTests
{
    private readonly ContaCorrenteFixture _contaCorrenteFixture;

    public ContaCorrenteQueriesTests(ContaCorrenteFixture contaCorrenteFixture)
    {
        _contaCorrenteFixture = contaCorrenteFixture;
    }

    [Trait("Queries", "ContaCorrente")]
    [Fact]
    public async Task DeveRetornarListaDasContasCorrentesDeUmUsuario()
    {
        // Arrange
        var leituraRepository = new Mock<IContaCorrenteLeituraRepository>();
        var idUsuario = Guid.NewGuid();
        var listaRetorno = new List<ContaCorrente> {_contaCorrenteFixture.GerarNovoContaCorrente()};
        var query = new ContaCorrenteQueries(leituraRepository.Object);
        leituraRepository.Setup(_ => _.BuscarContasCorrente(idUsuario)).Returns(Task.FromResult(listaRetorno.AsEnumerable()));
        
        // Act
        var resultado = await query.BuscarContasCorrentes(idUsuario);

        // Assert
        Assert.Equal(1, resultado.Count());
    }

    [Trait("Queries", "ContaCorrente")]
    [Fact]
    public async Task DeveRetornarListaVaziaCasoNaoEncontreNenhumaContaCorrente()
    {
        // Arrange
        var leituraRepository = new Mock<IContaCorrenteLeituraRepository>();
        var listaRetorno = new List<ContaCorrente> {_contaCorrenteFixture.GerarNovoContaCorrente()};
        var query = new ContaCorrenteQueries(leituraRepository.Object);
        leituraRepository.Setup(_ => _.BuscarContasCorrente(Guid.NewGuid())).Returns(Task.FromResult(listaRetorno.AsEnumerable()));
        
        // Act
        var resultado = await query.BuscarContasCorrentes(Guid.NewGuid());

        // Assert
        Assert.Equal(0, resultado.Count());
    }
}