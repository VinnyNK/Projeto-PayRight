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
        var usuarioRepository = new Mock<IUsuarioRepository>();
        var usuario = _usuarioFixture.GerarNovoUsuario();
        usuarioRepository.Setup(_ => _.BuscaUsuario(usuario.Id)).Returns(Task.FromResult(usuario)!);
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
        var usuarioRepository = new Mock<IUsuarioRepository>();
        usuarioRepository.Setup(_ => _.BuscaUsuario(Guid.NewGuid())).Returns(Task.FromResult<Usuario>(null!)!);
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
        var usuarioRepository = new Mock<IUsuarioRepository>();
        var usuario = _usuarioFixture.GerarNovoUsuario();
        usuarioRepository.Setup(_ => _.BuscaUsuarioCompleto(usuario.Id)).Returns(Task.FromResult(usuario)!);
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
        var usuarioRepository = new Mock<IUsuarioRepository>();
        usuarioRepository.Setup(_ => _.BuscaUsuario(Guid.NewGuid())).Returns(Task.FromResult<Usuario>(null!)!);
        var queries = new UsuarioQueries(usuarioRepository.Object);

        // Act
        var resultado = await queries.BuscaUsuarioCompleto(Guid.NewGuid());

        // Assert
        Assert.Null(resultado);
    }
}