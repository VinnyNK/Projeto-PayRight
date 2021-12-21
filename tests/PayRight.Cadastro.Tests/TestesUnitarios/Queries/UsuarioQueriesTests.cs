using System;
using System.Threading.Tasks;
using Moq;
using PayRight.Cadastro.Domain.Entities;
using PayRight.Cadastro.Domain.Queries;
using PayRight.Cadastro.Domain.Queries.DTOs;
using PayRight.Cadastro.Domain.Repositories;
using PayRight.Cadastro.Tests.TestesUnitarios.Entities.Fixtures;
using Xunit;

namespace PayRight.Cadastro.Tests.TestesUnitarios.Queries;

[Collection(nameof(UsuarioCollection))]
public class UsuarioQueriesTests
{
    private readonly UsuarioTestsFixture _usuarioFixture;

    public UsuarioQueriesTests(UsuarioTestsFixture usuarioFixture)
    {
        _usuarioFixture = usuarioFixture;
    }

    [Trait("Queries", "Usuario")]
    [Fact]
    public async Task DeveRetornarUsuarioInfoDtoChamarUsuarioExistente()
    {
        // Arrange
        var usuarioRepository = new Mock<IUsuarioLeituraRepository>();
        var usuario = _usuarioFixture.GerarNovoUsuario();
        usuarioRepository.Setup(_ => _.BuscaUsuarioQuery(usuario.Id)).Returns(Task.FromResult(new UsuarioInfoDTO())!);
        var queries = new UsuarioQueries(usuarioRepository.Object);

        // Act
        var resultado = await queries.BuscaInfoUsuario(usuario.Id);

        // Assert
        Assert.IsType<UsuarioInfoDTO>(resultado);
    }
    
    [Trait("Queries", "Usuario")]
    [Fact]
    public async Task DeveRetornarNullChamarUsuarioNaoExistente()
    {
        // Arrange
        var usuarioRepository = new Mock<IUsuarioLeituraRepository>();
        usuarioRepository.Setup(_ => _.BuscaUsuarioQuery(Guid.NewGuid())).Returns(Task.FromResult<UsuarioInfoDTO>(null!)!);
        var queries = new UsuarioQueries(usuarioRepository.Object);

        // Act
        var resultado = await queries.BuscaInfoUsuario(Guid.NewGuid());

        // Assert
        Assert.Null(resultado);
    }
    
    [Trait("Queries", "Usuario")]
    [Fact]
    public async Task DeveRetornarUsuarioInfoCompletoDtoChamarUsuarioExistente()
    {
        // Arrange
        var usuarioRepository = new Mock<IUsuarioLeituraRepository>();
        var usuario = _usuarioFixture.GerarNovoUsuario();
        usuarioRepository.Setup(_ => _.BuscaUsuarioCompletoQuery(usuario.Id)).Returns(Task.FromResult(new UsuarioInfoCompletoDTO())!);
        var queries = new UsuarioQueries(usuarioRepository.Object);

        // Act
        var resultado = await queries.BuscaUsuarioCompleto(usuario.Id);

        // Assert
        Assert.IsType<UsuarioInfoCompletoDTO>(resultado);
    }
    
    [Trait("Queries", "Usuario")]
    [Fact]
    public async Task DeveRetornarNullQueryUsuarioCompletoUsuarioNaoExistente()
    {
        // Arrange
        var usuarioRepository = new Mock<IUsuarioLeituraRepository>();
        usuarioRepository.Setup(_ => _.BuscaUsuarioCompletoQuery(Guid.NewGuid())).Returns(Task.FromResult<UsuarioInfoCompletoDTO>(null!)!);
        var queries = new UsuarioQueries(usuarioRepository.Object);

        // Act
        var resultado = await queries.BuscaUsuarioCompleto(Guid.NewGuid());

        // Assert
        Assert.Null(resultado);
    }
}