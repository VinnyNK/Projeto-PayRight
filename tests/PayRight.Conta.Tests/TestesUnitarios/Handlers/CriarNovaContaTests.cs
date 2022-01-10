using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using PayRight.Conta.Domain.Handlers;
using PayRight.Conta.Domain.Repositories;
using PayRight.Conta.Tests.TestesUnitarios.Command.Fixtures;
using PayRight.Shared.Mediator;
using Xunit;

namespace PayRight.Conta.Tests.TestesUnitarios.Handlers;

[Collection(nameof(CriarNovaContaCorrenteCommandCollection))]
public class CriarNovaContaTests
{
    private readonly CriarNovaContaCorrenteCommandFixture _criarNovaContaCorrenteCommandFixture;

    public CriarNovaContaTests(CriarNovaContaCorrenteCommandFixture criarNovaContaCorrenteCommandFixture)
    {
        _criarNovaContaCorrenteCommandFixture = criarNovaContaCorrenteCommandFixture;
    }

    [Trait("Handler", "CriarNovaConta")]
    [Fact]
    public async Task DeveRetornarSucessoCriarNovaContaCorrenteValida()
    {
        // Arrange
        var command = _criarNovaContaCorrenteCommandFixture.GerarCommand();
        var contaCorrenteLeituraRepository = new Mock<IContaCorrenteLeituraRepository>();
        var contaCorrenteEscritaRepository = new Mock<IContaCorrenteEscritaRepository>();
        var mediator = new Mock<IMediatorHandler>();
        var handler = new CriarNovaContaHandler(contaCorrenteLeituraRepository.Object, contaCorrenteEscritaRepository.Object, mediator.Object);
        contaCorrenteLeituraRepository.Setup(_ => _.NomeContaExisteParaUsuario(command.UsuarioId, command.Nome))
            .Returns(Task.FromResult(false));
        contaCorrenteEscritaRepository.Setup(_ => _.Commit()).Returns(Task.FromResult(true));

        // Act
        var commandResult = await handler.Handle(command, CancellationToken.None);
        var valid = handler.IsValid;

        // Assert
        Assert.True(commandResult.Sucesso);
        Assert.True(valid);
    }

    [Trait("Handler", "CriarNovaConta")]
    [Fact]
    public async Task DeveRetornarErroCriarNovaContaNomeExistente()
    {
        // Arrange
        var command = _criarNovaContaCorrenteCommandFixture.GerarCommand();
        var contaCorrenteLeituraRepository = new Mock<IContaCorrenteLeituraRepository>();
        var contaCorrenteEscritaRepository = new Mock<IContaCorrenteEscritaRepository>();
        var mediator = new Mock<IMediatorHandler>();
        var handler = new CriarNovaContaHandler(contaCorrenteLeituraRepository.Object, contaCorrenteEscritaRepository.Object, mediator.Object);
        contaCorrenteLeituraRepository.Setup(_ => _.NomeContaExisteParaUsuario(command.UsuarioId, command.Nome))
            .Returns(Task.FromResult(true));

        // Act
        var commandResult = await handler.Handle(command, CancellationToken.None);
        var valid = handler.IsValid;

        // Assert
        Assert.False(commandResult.Sucesso);
        Assert.False(valid);
    }
    
    [Trait("Handler", "CriarNovaConta")]
    [Fact]
    public async Task DeveRetornarErroCriarNovaContaProblemaNoCommit()
    {
        // Arrange
        var command = _criarNovaContaCorrenteCommandFixture.GerarCommand();
        var contaCorrenteLeituraRepository = new Mock<IContaCorrenteLeituraRepository>();
        var contaCorrenteEscritaRepository = new Mock<IContaCorrenteEscritaRepository>();
        var mediator = new Mock<IMediatorHandler>();
        var handler = new CriarNovaContaHandler(contaCorrenteLeituraRepository.Object, contaCorrenteEscritaRepository.Object, mediator.Object);
        contaCorrenteLeituraRepository.Setup(_ => _.NomeContaExisteParaUsuario(command.UsuarioId, command.Nome))
            .Returns(Task.FromResult(false));
        contaCorrenteEscritaRepository.Setup(_ => _.Commit()).Returns(Task.FromResult(false));

        // Act
        var commandResult = await handler.Handle(command, CancellationToken.None);
        var valid = handler.IsValid;

        // Assert
        Assert.False(commandResult.Sucesso);
        Assert.True(valid);
    }

    [Trait("Handler", "CriarNovaConta")]
    [Fact]
    public async Task DeveRetornarErroCriarNovaContaComandoInvalido()
    {
        // Arrange
        var command = _criarNovaContaCorrenteCommandFixture.GerarCommand(Guid.Empty);
        var contaCorrenteLeituraRepository = new Mock<IContaCorrenteLeituraRepository>();
        var contaCorrenteEscritaRepository = new Mock<IContaCorrenteEscritaRepository>();
        var mediator = new Mock<IMediatorHandler>();
        var handler = new CriarNovaContaHandler(contaCorrenteLeituraRepository.Object, contaCorrenteEscritaRepository.Object, mediator.Object);
        
        // Act
        var commandResult = await handler.Handle(command, CancellationToken.None);
        var valid = handler.IsValid;

        // Assert
        Assert.False(commandResult.Sucesso);
        Assert.False(valid);
    }
}