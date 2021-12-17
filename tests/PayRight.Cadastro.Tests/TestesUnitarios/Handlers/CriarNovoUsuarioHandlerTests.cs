using System.Threading;
using System.Threading.Tasks;
using Moq;
using PayRight.Cadastro.Domain.Handlers;
using PayRight.Cadastro.Domain.Repositories;
using PayRight.Cadastro.Tests.TestesUnitarios.Commands.Fixtures;
using PayRight.Shared.EventNotifications;
using PayRight.Shared.Mediator;
using Xunit;

namespace PayRight.Cadastro.Tests.TestesUnitarios.Handlers;

[Collection(nameof(CriarNovoUsuarioCommandCollection))]
public class CriarNovoUsuarioHandlerTests
{
    private readonly CriarNovoUsuarioCommandFixture _criarNovoUsuarioCommandFixture;

    public CriarNovoUsuarioHandlerTests(CriarNovoUsuarioCommandFixture criarNovoUsuarioCommandFixture)
    {
        _criarNovoUsuarioCommandFixture = criarNovoUsuarioCommandFixture;
    }

    [Trait("Handler", "CriarNovoUsuario")]
    [Fact]
    public async Task DeveRetornarSucessoCriarNovoUsuarioHandlerTipoCpfValido()
    {
        // Arrange
        var command = _criarNovoUsuarioCommandFixture.GerarNovoUsuarioCpfCommand();
        var usuarioRepository = new Mock<IUsuarioRepository>();
        var mediator = new Mock<IMediatorHandler>();
        var handler = new CriarNovoUsuarioHandler(usuarioRepository.Object, mediator.Object);
        usuarioRepository.Setup(_ => _.EmailExiste(command.EnderecoEmail)).Returns(Task.FromResult(false));
        usuarioRepository.Setup(_ => _.DocumentoExiste(command.NumeroDocumento)).Returns(Task.FromResult(false));
        usuarioRepository.Setup(_ => _.Commit()).Returns(Task.FromResult(true));

        // Act
        var commandResult = await handler.Handle(command, CancellationToken.None);
        var resultado = handler.IsValid;

        // Assert
        Assert.True(resultado);
        Assert.True(commandResult.Sucesso);
    }
    
    [Trait("Handler", "CriarNovoUsuario")]
    [Fact]
    public async Task DeveRetornarSucessoCriarNovoUsuarioHandlerTipoCnpjValido()
    {
        // Arrange
        var command = _criarNovoUsuarioCommandFixture.GerarNovoUsuarioCnpjCommand();
        var usuarioRepository = new Mock<IUsuarioRepository>();
        var mediator = new Mock<IMediatorHandler>();
        var handler = new CriarNovoUsuarioHandler(usuarioRepository.Object, mediator.Object);
        usuarioRepository.Setup(_ => _.EmailExiste(command.EnderecoEmail)).Returns(Task.FromResult(false));
        usuarioRepository.Setup(_ => _.DocumentoExiste(command.NumeroDocumento)).Returns(Task.FromResult(false));
        usuarioRepository.Setup(_ => _.Commit()).Returns(Task.FromResult(true));
        
        // Act
        var commandResult = await handler.Handle(command, CancellationToken.None);
        var resultado = handler.IsValid;

        // Assert
        Assert.True(resultado);
        Assert.True(commandResult.Sucesso);
    }
    
    [Trait("Handler", "CriarNovoUsuario")]
    [Fact]
    public async Task DeveRetornarErroCriarNovoUsuarioHandlerTipoCpfEmailExistente()
    {
        // Arrange
        var command = _criarNovoUsuarioCommandFixture.GerarNovoUsuarioCpfCommand();
        var usuarioRepository = new Mock<IUsuarioRepository>();
        var mediator = new Mock<IMediatorHandler>();
        var handler = new CriarNovoUsuarioHandler(usuarioRepository.Object, mediator.Object);
        usuarioRepository.Setup(_ => _.EmailExiste(command.EnderecoEmail)).Returns(Task.FromResult(true));
        usuarioRepository.Setup(_ => _.DocumentoExiste(command.NumeroDocumento)).Returns(Task.FromResult(false));

        // Act
        var commandResult = await handler.Handle(command, CancellationToken.None);
        var resultado = handler.IsValid;

        // Assert
        Assert.False(resultado);
        Assert.False(commandResult.Sucesso);
    }
    
    [Trait("Handler", "CriarNovoUsuario")]
    [Fact]
    public async Task DeveRetornarErroCriarNovoUsuarioHandlerTipoCpfDocumentoExistente()
    {
        // Arrange
        var command = _criarNovoUsuarioCommandFixture.GerarNovoUsuarioCpfCommand();
        var usuarioRepository = new Mock<IUsuarioRepository>();
        var mediator = new Mock<IMediatorHandler>();
        var handler = new CriarNovoUsuarioHandler(usuarioRepository.Object, mediator.Object);
        usuarioRepository.Setup(_ => _.EmailExiste(command.EnderecoEmail)).Returns(Task.FromResult(false));
        usuarioRepository.Setup(_ => _.DocumentoExiste(command.NumeroDocumento)).Returns(Task.FromResult(true));
        
        // Act
        var commandResult = await handler.Handle(command, CancellationToken.None);
        var resultado = handler.IsValid;

        // Assert
        Assert.False(resultado);
        Assert.False(commandResult.Sucesso);
    }
    
    [Trait("Handler", "CriarNovoUsuario")]
    [Fact]
    public async Task DeveRetornarErroCriarNovoUsuarioHandlerTipoCnpjEmailExistente()
    {
        // Arrange
        var command = _criarNovoUsuarioCommandFixture.GerarNovoUsuarioCnpjCommand();
        var usuarioRepository = new Mock<IUsuarioRepository>();
        var mediator = new Mock<IMediatorHandler>();
        var handler = new CriarNovoUsuarioHandler(usuarioRepository.Object, mediator.Object);
        usuarioRepository.Setup(_ => _.EmailExiste(command.EnderecoEmail)).Returns(Task.FromResult(false));
        usuarioRepository.Setup(_ => _.DocumentoExiste(command.NumeroDocumento)).Returns(Task.FromResult(true));
        
        // Act
        var commandResult = await handler.Handle(command, CancellationToken.None);
        var resultado = handler.IsValid;

        // Assert
        Assert.False(resultado);
        Assert.False(commandResult.Sucesso);
    }


    [Trait("Handler", "CriarNovoUsuario")]
    [Fact]
    public async Task DeveRetornarErroCriarNovoUsuarioHandlerTipoCnpjDocumentoExistente()
    {
        // Arrange
        var command = _criarNovoUsuarioCommandFixture.GerarNovoUsuarioCnpjCommand();
        var usuarioRepository = new Mock<IUsuarioRepository>();
        var mediator = new Mock<IMediatorHandler>();
        var handler = new CriarNovoUsuarioHandler(usuarioRepository.Object, mediator.Object);
        usuarioRepository.Setup(_ => _.EmailExiste(command.EnderecoEmail)).Returns(Task.FromResult(true));
        usuarioRepository.Setup(_ => _.DocumentoExiste(command.NumeroDocumento)).Returns(Task.FromResult(false));
        
        // Act
        var commandResult = await handler.Handle(command, CancellationToken.None);
        var resultado = handler.IsValid;

        // Assert
        Assert.False(resultado);
        Assert.False(commandResult.Sucesso);
    }
    
    [Trait("Handler", "Criarusuario")]
    [Fact]
    public async Task DeveExecutarHandleAoPublicarEvento()
    {
        // Arrange
        var command = _criarNovoUsuarioCommandFixture.GerarNovoUsuarioCpfCommand();
        var usuarioRepository = new Mock<IUsuarioRepository>();
        var mediator = new Mock<IMediatorHandler>();
        var handler = new CriarNovoUsuarioHandler(usuarioRepository.Object, mediator.Object);
        usuarioRepository.Setup(_ => _.EmailExiste(command.EnderecoEmail)).Returns(Task.FromResult(false));
        usuarioRepository.Setup(_ => _.DocumentoExiste(command.NumeroDocumento)).Returns(Task.FromResult(false));
        usuarioRepository.Setup(_ => _.Commit()).Returns(Task.FromResult(true));

        // Act
        var commandResult = await handler.Handle(command, CancellationToken.None);
        var resultado = handler.IsValid;

        // Assert
        Assert.True(resultado);
        Assert.True(commandResult.Sucesso);
        mediator.Verify(_ => _.PublicarEvento(It.IsAny<ICustomNotification>()), Times.Once);
    }
    
}