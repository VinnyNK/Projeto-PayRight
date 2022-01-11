using System.Threading;
using System.Threading.Tasks;
using Moq;
using PayRight.Conta.Domain.Entities;
using PayRight.Conta.Domain.Handlers;
using PayRight.Conta.Domain.Repositories;
using PayRight.Conta.Tests.TestesUnitarios.Command.Fixtures;
using Xunit;

namespace PayRight.Conta.Tests.TestesUnitarios.Handlers;

[Collection(nameof(SaldoContaCorrenteCommandCollection))]
public class SaldoContaCorrenteHandlerTests
{
    private readonly SomarSaldoContaCorrenteCommandFixture _somarSaldoContaCorrenteCommandFixture;
    
    private readonly SubtrairSaldoContaCorrenteCommandFixture _subtrairSaldoContaCorrenteCommandFixture;

    public SaldoContaCorrenteHandlerTests(SomarSaldoContaCorrenteCommandFixture somarSaldoContaCorrenteCommandFixture, SubtrairSaldoContaCorrenteCommandFixture subtrairSaldoContaCorrenteCommandFixture)
    {
        _somarSaldoContaCorrenteCommandFixture = somarSaldoContaCorrenteCommandFixture;
        _subtrairSaldoContaCorrenteCommandFixture = subtrairSaldoContaCorrenteCommandFixture;
    }

    [Trait("Handler", "SaldoContaCorrente")]
    [Fact]
    public async Task DeveRetornarSucessoSomarSaldoContaCorrenteValido()
    {
        // Arrange
        var command = _somarSaldoContaCorrenteCommandFixture.GerarCommand();
        var escritaRepository = new Mock<IContaCorrenteEscritaRepository>();
        var leituraRepository = new Mock<IContaCorrenteLeituraRepository>();
        var handler = new SaldoContaCorrenteHandler(escritaRepository.Object, leituraRepository.Object);
        leituraRepository.Setup(_ => _.BuscaContaCorrente(command.UsuarioId, command.ContaCorrenteId))
            .Returns(Task.FromResult(new ContaCorrente(command.UsuarioId, "Conta Teste", null))!);
        escritaRepository.Setup(_ => _.Commit()).Returns(Task.FromResult(true));
        
        // Act
        var commandResult = await handler.Handle(command, CancellationToken.None);
        var resultado = handler.IsValid;
        
        // Assert
        Assert.True(commandResult.Sucesso);
        Assert.True(resultado);
    }

    [Trait("Handler", "SaldoContaCorrente")]
    [Fact]
    public async Task DeveRetornarErroSomarContaCorrenteInexistente()
    {
        // Arrange
        var command = _somarSaldoContaCorrenteCommandFixture.GerarCommand();
        var escritaRepository = new Mock<IContaCorrenteEscritaRepository>();
        var leituraRepository = new Mock<IContaCorrenteLeituraRepository>();
        var handler = new SaldoContaCorrenteHandler(escritaRepository.Object, leituraRepository.Object);
        leituraRepository.Setup(_ => _.BuscaContaCorrente(command.UsuarioId, command.ContaCorrenteId))
            .Returns(Task.FromResult<ContaCorrente>(null!)!);

        // Act
        var commandResult = await handler.Handle(command, CancellationToken.None);
        var resultado = handler.IsValid;
        
        // Assert
        Assert.False(commandResult.Sucesso);
        Assert.False(resultado);
    }

    [Trait("Handler", "SaldoContaCorrente")]
    [Fact]
    public async Task DeveRetornarErroCommandoSomarSaldoInvalido()
    {
        // Arrange
        var command = _somarSaldoContaCorrenteCommandFixture.GerarCommand(valor: -20);
        var escritaRepository = new Mock<IContaCorrenteEscritaRepository>();
        var leituraRepository = new Mock<IContaCorrenteLeituraRepository>();
        var handler = new SaldoContaCorrenteHandler(escritaRepository.Object, leituraRepository.Object);

        // Act
        var commandResult = await handler.Handle(command, CancellationToken.None);
        var resultado = handler.IsValid;
        
        // Assert
        Assert.False(commandResult.Sucesso);
        Assert.False(resultado);
    }

    [Trait("Handler", "SaldoContaCorrente")]
    [Fact]
    public async Task DeveRetornarErroSomarSaldoEntityInvalida()
    {
        // Arrange
        var command = _somarSaldoContaCorrenteCommandFixture.GerarCommand(valor: -20);
        var escritaRepository = new Mock<IContaCorrenteEscritaRepository>();
        var leituraRepository = new Mock<IContaCorrenteLeituraRepository>();
        var handler = new SaldoContaCorrenteHandler(escritaRepository.Object, leituraRepository.Object);
        leituraRepository.Setup(_ => _.BuscaContaCorrente(command.UsuarioId, command.ContaCorrenteId))
            .Returns(Task.FromResult(new ContaCorrente(command.UsuarioId, "", null))!);

        // Act
        var commandResult = await handler.Handle(command, CancellationToken.None);
        var resultado = handler.IsValid;
        
        // Assert
        Assert.False(commandResult.Sucesso);
        Assert.False(resultado);
    }

    [Trait("Handler", "SaldoContaCorrente")]
    [Fact]
    public async Task DeveRetornarErroSomarSaldoErroCommit()
    {
        // Arrange
        var command = _somarSaldoContaCorrenteCommandFixture.GerarCommand();
        var escritaRepository = new Mock<IContaCorrenteEscritaRepository>();
        var leituraRepository = new Mock<IContaCorrenteLeituraRepository>();
        var handler = new SaldoContaCorrenteHandler(escritaRepository.Object, leituraRepository.Object);
        leituraRepository.Setup(_ => _.BuscaContaCorrente(command.UsuarioId, command.ContaCorrenteId))
            .Returns(Task.FromResult(new ContaCorrente(command.UsuarioId, "Conta Teste", null))!);
        escritaRepository.Setup(_ => _.Commit()).Returns(Task.FromResult(false));
        
        // Act
        var commandResult = await handler.Handle(command, CancellationToken.None);
        var resultado = handler.IsValid;
        
        // Assert
        Assert.False(commandResult.Sucesso);
        Assert.True(resultado);
    }
    
    [Trait("Handler", "SaldoContaCorrente")]
    [Fact]
    public async Task DeveRetornarSucessoSubtrairSaldoContaCorrenteValido()
    {
        // Arrange
        var command = _subtrairSaldoContaCorrenteCommandFixture.GerarCommand();
        var escritaRepository = new Mock<IContaCorrenteEscritaRepository>();
        var leituraRepository = new Mock<IContaCorrenteLeituraRepository>();
        var handler = new SaldoContaCorrenteHandler(escritaRepository.Object, leituraRepository.Object);
        leituraRepository.Setup(_ => _.BuscaContaCorrente(command.UsuarioId, command.ContaCorrenteId))
            .Returns(Task.FromResult(new ContaCorrente(command.UsuarioId, "Conta Teste", null))!);
        escritaRepository.Setup(_ => _.Commit()).Returns(Task.FromResult(true));
        
        // Act
        var commandResult = await handler.Handle(command, CancellationToken.None);
        var resultado = handler.IsValid;
        
        // Assert
        Assert.True(commandResult.Sucesso);
        Assert.True(resultado);
    }

    [Trait("Handler", "SaldoContaCorrente")]
    [Fact]
    public async Task DeveRetornarErroSubtrairContaCorrenteInexistente()
    {
        // Arrange
        var command = _subtrairSaldoContaCorrenteCommandFixture.GerarCommand();
        var escritaRepository = new Mock<IContaCorrenteEscritaRepository>();
        var leituraRepository = new Mock<IContaCorrenteLeituraRepository>();
        var handler = new SaldoContaCorrenteHandler(escritaRepository.Object, leituraRepository.Object);
        leituraRepository.Setup(_ => _.BuscaContaCorrente(command.UsuarioId, command.ContaCorrenteId))
            .Returns(Task.FromResult<ContaCorrente>(null!)!);

        // Act
        var commandResult = await handler.Handle(command, CancellationToken.None);
        var resultado = handler.IsValid;
        
        // Assert
        Assert.False(commandResult.Sucesso);
        Assert.False(resultado);
    }

    [Trait("Handler", "SaldoContaCorrente")]
    [Fact]
    public async Task DeveRetornarErroCommandoSubtrairSaldoInvalido()
    {
        // Arrange
        var command = _subtrairSaldoContaCorrenteCommandFixture.GerarCommand(valor: -20);
        var escritaRepository = new Mock<IContaCorrenteEscritaRepository>();
        var leituraRepository = new Mock<IContaCorrenteLeituraRepository>();
        var handler = new SaldoContaCorrenteHandler(escritaRepository.Object, leituraRepository.Object);

        // Act
        var commandResult = await handler.Handle(command, CancellationToken.None);
        var resultado = handler.IsValid;
        
        // Assert
        Assert.False(commandResult.Sucesso);
        Assert.False(resultado);
    }

    [Trait("Handler", "SaldoContaCorrente")]
    [Fact]
    public async Task DeveRetornarErroSubtrairSaldoEntityInvalida()
    {
        // Arrange
        var command = _subtrairSaldoContaCorrenteCommandFixture.GerarCommand(valor: -20);
        var escritaRepository = new Mock<IContaCorrenteEscritaRepository>();
        var leituraRepository = new Mock<IContaCorrenteLeituraRepository>();
        var handler = new SaldoContaCorrenteHandler(escritaRepository.Object, leituraRepository.Object);
        leituraRepository.Setup(_ => _.BuscaContaCorrente(command.UsuarioId, command.ContaCorrenteId))
            .Returns(Task.FromResult(new ContaCorrente(command.UsuarioId, "", null))!);

        // Act
        var commandResult = await handler.Handle(command, CancellationToken.None);
        var resultado = handler.IsValid;
        
        // Assert
        Assert.False(commandResult.Sucesso);
        Assert.False(resultado);
    }

    [Trait("Handler", "SaldoContaCorrente")]
    [Fact]
    public async Task DeveRetornarErroSubtrairSaldoErroCommit()
    {
        // Arrange
        var command = _subtrairSaldoContaCorrenteCommandFixture.GerarCommand();
        var escritaRepository = new Mock<IContaCorrenteEscritaRepository>();
        var leituraRepository = new Mock<IContaCorrenteLeituraRepository>();
        var handler = new SaldoContaCorrenteHandler(escritaRepository.Object, leituraRepository.Object);
        leituraRepository.Setup(_ => _.BuscaContaCorrente(command.UsuarioId, command.ContaCorrenteId))
            .Returns(Task.FromResult(new ContaCorrente(command.UsuarioId, "Conta Teste", null))!);
        escritaRepository.Setup(_ => _.Commit()).Returns(Task.FromResult(false));
        
        // Act
        var commandResult = await handler.Handle(command, CancellationToken.None);
        var resultado = handler.IsValid;
        
        // Assert
        Assert.False(commandResult.Sucesso);
        Assert.True(resultado);
    }
}