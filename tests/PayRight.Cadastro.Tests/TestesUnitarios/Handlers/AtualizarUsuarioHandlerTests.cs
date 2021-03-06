using System.Threading.Tasks;
using Moq;
using PayRight.Cadastro.Domain.Entities;
using PayRight.Cadastro.Domain.Handlers;
using PayRight.Cadastro.Domain.Repositories;
using PayRight.Cadastro.Domain.ValueObjects;
using PayRight.Cadastro.Tests.TestesUnitarios.Commands.Fixtures;
using PayRight.Cadastro.Tests.TestesUnitarios.Entities.Fixtures;
using Xunit;

namespace PayRight.Cadastro.Tests.TestesUnitarios.Handlers;

[Collection(nameof(AtualizarUsuarioCommandCollection))]
public class AtualizarUsuarioHandlerTests
{
    private readonly AtualizarUsuarioCommandFixture _atualizarUsuarioCommandFixture;
    private readonly UsuarioTestsFixture _usuarioTestsFixture;

    public AtualizarUsuarioHandlerTests(AtualizarUsuarioCommandFixture atualizarUsuarioCommandFixture, UsuarioTestsFixture usuarioTestsFixture)
    {
        _atualizarUsuarioCommandFixture = atualizarUsuarioCommandFixture;
        _usuarioTestsFixture = usuarioTestsFixture;
    }

    [Trait("Handler", "AtualizarUsuario")]
    [Fact]
    public async Task DeveRetornarSucessoAtualizarEmailENomeCompletoValidosUsuarioExistente()
    {
        // Arrange
        var usuario = _usuarioTestsFixture.GerarNovoUsuario();
        var command = _atualizarUsuarioCommandFixture.GerarAtualizarUsuarioCommand(usuario);
        var usuarioRepository = new Mock<IUsuarioRepository>();
        usuarioRepository.Setup(_ => _.BuscaUsuario(command.Id)).Returns(Task.FromResult(usuario)!);
        usuarioRepository.Setup(_ => _.EmailExiste(command.EnderecoEmail!)).Returns(Task.FromResult(false));
        usuarioRepository.Setup(_ => _.Commit()).Returns(Task.FromResult(true));
        var handler = new AtualizarUsuarioHandler(usuarioRepository.Object);

        // Act
        var commandResult = await handler.Handle(command);
        var resultado = handler.IsValid;

        // Assert
        Assert.True(commandResult.Sucesso);
        Assert.True(resultado);
    }
    
    [Trait("Handler", "AtualizarUsuario")]
    [Fact]
    public async Task DeveRetornarErroAtualizarUsuarioComandoInvalido()
    {
        // Arrange
        var usuario = _usuarioTestsFixture.GerarNovoUsuario();
        var command = _atualizarUsuarioCommandFixture.GerarAtualizarUsuarioCommand(usuario, primeiroNome: "Fula no");
        var usuarioRepository = new Mock<IUsuarioRepository>();
        var handler = new AtualizarUsuarioHandler(usuarioRepository.Object);

        // Act
        var commandResult = await handler.Handle(command);
        var resultado = handler.IsValid;

        // Assert
        Assert.False(commandResult.Sucesso);
        Assert.False(resultado);
    }
    
    [Trait("Handler", "AtualizarUsuario")]
    [Fact]
    public async Task DeveRetornarErroAtualizarEmailENomeCompletoValidosUsuarioInexistente()
    {
        // Arrange
        var usuario = _usuarioTestsFixture.GerarNovoUsuario();
        var command = _atualizarUsuarioCommandFixture.GerarAtualizarUsuarioCommand(usuario);
        var usuarioRepository = new Mock<IUsuarioRepository>();
        usuarioRepository.Setup(_ => _.BuscaUsuario(command.Id)).Returns(Task.FromResult<Usuario>(null!)!);
        var handler = new AtualizarUsuarioHandler(usuarioRepository.Object);

        // Act
        var commandResult = await handler.Handle(command);
        var resultado = handler.IsValid;

        // Assert
        Assert.False(commandResult.Sucesso);
        Assert.False(resultado);
    }
    
    [Trait("Handler", "AtualizarUsuario")]
    [Fact]
    public async Task DeveRetornarErroAtualizarEmailENomeCompletoValidosUsuarioExistenteErroNoBanco()
    {
        // Arrange
        var usuario = _usuarioTestsFixture.GerarNovoUsuario();
        var command = _atualizarUsuarioCommandFixture.GerarAtualizarUsuarioCommand(usuario);
        var usuarioRepository = new Mock<IUsuarioRepository>();
        usuarioRepository.Setup(_ => _.BuscaUsuario(command.Id)).Returns(Task.FromResult(usuario)!);
        usuarioRepository.Setup(_ => _.EmailExiste(command.EnderecoEmail!)).Returns(Task.FromResult(false));
        usuarioRepository.Setup(_ => _.Commit()).Returns(Task.FromResult(false));
        var handler = new AtualizarUsuarioHandler(usuarioRepository.Object);

        // Act
        var commandResult = await handler.Handle(command);
        var resultado = handler.IsValid;

        // Assert
        Assert.False(commandResult.Sucesso);
        Assert.True(resultado);
    }
    
    [Trait("Handler", "AtualizarUsuario")]
    [Fact]
    public async Task DeveRetornarErroAtualizarEmailExistenteValidosUsuarioExistente()
    {
        // Arrange
        var usuario = _usuarioTestsFixture.GerarNovoUsuario();
        var command = _atualizarUsuarioCommandFixture.GerarAtualizarUsuarioCommand(usuario);
        var usuarioRepository = new Mock<IUsuarioRepository>();
        usuarioRepository.Setup(_ => _.BuscaUsuario(command.Id)).Returns(Task.FromResult(usuario)!);
        usuarioRepository.Setup(_ => _.EmailExiste(command.EnderecoEmail!)).Returns(Task.FromResult(true));
        var handler = new AtualizarUsuarioHandler(usuarioRepository.Object);

        // Act
        var commandResult = await handler.Handle(command);
        var resultado = handler.IsValid;

        // Assert
        Assert.False(commandResult.Sucesso);
        Assert.False(resultado);
    }
}