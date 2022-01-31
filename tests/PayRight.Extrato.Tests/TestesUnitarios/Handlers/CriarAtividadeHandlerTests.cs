using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using PayRight.Extrato.Domain.Entities;
using PayRight.Extrato.Domain.GrpcService;
using PayRight.Extrato.Domain.Handlers;
using PayRight.Extrato.Domain.Repositories;
using PayRight.Extrato.Domain.UnitOfWork;
using PayRight.Extrato.Tests.TestesUnitarios.Commands.Fixtures;
using Xunit;

namespace PayRight.Extrato.Tests.TestesUnitarios.Handlers;

[Collection(nameof(CriarAtividadeContaCorrenteCommandCollection))]
public class CriarAtividadeHandlerTests
{
    private readonly CriarAtividadeContaCorrenteCommandFixture _fixtureCommand;

    public CriarAtividadeHandlerTests(CriarAtividadeContaCorrenteCommandFixture fixtureCommand)
    {
        _fixtureCommand = fixtureCommand;
    }

    [Trait("Handler", "CriarAtividade")]
    [Fact]
    public async Task DeveRetornarSucessoCriarAtividadeValido()
    {
        // Arrange
        var command = _fixtureCommand.GerarCommand();
        var unitOfWork = new Mock<IUnitOfWork>();
        var extratoLeituraRepo = new Mock<IContaCorrenteExtratoLeituraRepository>();
        var extratoEscritaRepo = new Mock<IContaCorrenteExtratoEscritaRepository>();
        var atividadeEscritaRepo = new Mock<IAtividadeEscritaRepository>();
        var grpcService = new Mock<IContaCorrenteGrpcService>();
        var handler = new CriarAtividadeHandler(unitOfWork.Object, grpcService.Object);
        
        // Act
        grpcService.Setup(_ => _.ValidarContaCorrente(command.ContaCorrenteId, command.UsuarioId))
            .Returns(Task.FromResult(true));
        unitOfWork.Setup(_ => _.ContaCorrenteExtratoLeituraRepository)
            .Returns(extratoLeituraRepo.Object);
        unitOfWork.Setup(_ => _.AtividadeEscritaRepository).Returns(atividadeEscritaRepo.Object);
        unitOfWork.Setup(_ => _.ContaCorrenteExtratoEscritaRepository).Returns(extratoEscritaRepo.Object);
        extratoLeituraRepo.Setup(_ => _.BuscarExtratoPorMesEAno(command.ContaCorrenteId, command.UsuarioId,
                command.DataEstimado.Month, command.DataEstimado.Year))
            .Returns(Task.FromResult(new ContaCorrenteExtrato(command.ContaCorrenteId, command.UsuarioId,
                command.DataEstimado.Month, command.DataEstimado.Year))!);
        unitOfWork.Setup(_ => _.Commit()).Returns(Task.FromResult(true));

        var resultado = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(handler.IsValid);
        Assert.True(resultado.Sucesso);
    }
    
    [Trait("Handler", "CriarAtividade")]
    [Fact]
    public async Task DeveRetornarSucessoCriarAtividadeValidoExtratoNaoExistente()
    {
        // Arrange
        var command = _fixtureCommand.GerarCommand();
        var unitOfWork = new Mock<IUnitOfWork>();
        var extratoLeituraRepo = new Mock<IContaCorrenteExtratoLeituraRepository>();
        var extratoEscritaRepo = new Mock<IContaCorrenteExtratoEscritaRepository>();
        var atividadeEscritaRepo = new Mock<IAtividadeEscritaRepository>();
        var grpcService = new Mock<IContaCorrenteGrpcService>();
        var handler = new CriarAtividadeHandler(unitOfWork.Object, grpcService.Object);
        
        // Act
        grpcService.Setup(_ => _.ValidarContaCorrente(command.ContaCorrenteId, command.UsuarioId))
            .Returns(Task.FromResult(true));
        unitOfWork.Setup(_ => _.ContaCorrenteExtratoLeituraRepository)
            .Returns(extratoLeituraRepo.Object);
        unitOfWork.Setup(_ => _.AtividadeEscritaRepository).Returns(atividadeEscritaRepo.Object);
        unitOfWork.Setup(_ => _.ContaCorrenteExtratoEscritaRepository).Returns(extratoEscritaRepo.Object);
        extratoLeituraRepo.Setup(_ => _.BuscarExtratoPorMesEAno(command.ContaCorrenteId, command.UsuarioId,
                command.DataEstimado.Month, command.DataEstimado.Year))
            .Returns(Task.FromResult<ContaCorrenteExtrato>(null!)!);
        unitOfWork.Setup(_ => _.Commit()).Returns(Task.FromResult(true));

        var resultado = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(handler.IsValid);
        Assert.True(resultado.Sucesso);
    }
    
    [Trait("Handler", "CriarAtividade")]
    [Fact]
    public async Task DeveRetornarErroCriarAtividadeContaCorrenteInvalidaOunNaoPertenceAoUsuario()
    {
        // Arrange
        var command = _fixtureCommand.GerarCommand();
        var unitOfWork = new Mock<IUnitOfWork>();
        var grpcService = new Mock<IContaCorrenteGrpcService>();
        var handler = new CriarAtividadeHandler(unitOfWork.Object, grpcService.Object);
        
        // Act
        grpcService.Setup(_ => _.ValidarContaCorrente(command.ContaCorrenteId, command.UsuarioId))
            .Returns(Task.FromResult(false));

        var resultado = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(handler.IsValid);
        Assert.False(resultado.Sucesso);
    }
    
    [Trait("Handler", "CriarAtividade")]
    [Fact]
    public async Task DeveRetornarErroCriarAtividadeErroCommit()
    {
        // Arrange
        var command = _fixtureCommand.GerarCommand();
        var unitOfWork = new Mock<IUnitOfWork>();
        var extratoLeituraRepo = new Mock<IContaCorrenteExtratoLeituraRepository>();
        var extratoEscritaRepo = new Mock<IContaCorrenteExtratoEscritaRepository>();
        var atividadeEscritaRepo = new Mock<IAtividadeEscritaRepository>();
        var grpcService = new Mock<IContaCorrenteGrpcService>();
        var handler = new CriarAtividadeHandler(unitOfWork.Object, grpcService.Object);
        
        // Act
        grpcService.Setup(_ => _.ValidarContaCorrente(command.ContaCorrenteId, command.UsuarioId))
            .Returns(Task.FromResult(true));
        unitOfWork.Setup(_ => _.ContaCorrenteExtratoLeituraRepository)
            .Returns(extratoLeituraRepo.Object);
        unitOfWork.Setup(_ => _.AtividadeEscritaRepository).Returns(atividadeEscritaRepo.Object);
        unitOfWork.Setup(_ => _.ContaCorrenteExtratoEscritaRepository).Returns(extratoEscritaRepo.Object);
        extratoLeituraRepo.Setup(_ => _.BuscarExtratoPorMesEAno(command.ContaCorrenteId, command.UsuarioId,
                command.DataEstimado.Month, command.DataEstimado.Year))
            .Returns(Task.FromResult(new ContaCorrenteExtrato(command.ContaCorrenteId, command.UsuarioId,
                command.DataEstimado.Month, command.DataEstimado.Year))!);
        unitOfWork.Setup(_ => _.Commit()).Returns(Task.FromResult(false));

        var resultado = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(handler.IsValid);
        Assert.False(resultado.Sucesso);
    }
    
    [Trait("Handler", "CriarAtividade")]
    [Fact]
    public async Task DeveRetornarErroCriarAtividadeCommandoInvalido()
    {
        // Arrange
        var command = _fixtureCommand.GerarCommand(Guid.Empty);
        var unitOfWork = new Mock<IUnitOfWork>();
        var grpcService = new Mock<IContaCorrenteGrpcService>();
        var handler = new CriarAtividadeHandler(unitOfWork.Object, grpcService.Object);
        
        // Act
        var resultado = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(handler.IsValid);
        Assert.False(resultado.Sucesso);
    }
    
    [Trait("Handler", "CriarAtividade")]
    [Fact]
    public async Task DeveRetornarErroCriarAtividadeExtratoCriadoInvalido()
    {
        // Arrange
        var command = _fixtureCommand.GerarCommand();
        var unitOfWork = new Mock<IUnitOfWork>();
        var extratoLeituraRepo = new Mock<IContaCorrenteExtratoLeituraRepository>();
        var extratoEscritaRepo = new Mock<IContaCorrenteExtratoEscritaRepository>();
        var atividadeEscritaRepo = new Mock<IAtividadeEscritaRepository>();
        var grpcService = new Mock<IContaCorrenteGrpcService>();
        var handler = new CriarAtividadeHandler(unitOfWork.Object, grpcService.Object);
        
        // Act
        grpcService.Setup(_ => _.ValidarContaCorrente(command.ContaCorrenteId, command.UsuarioId))
            .Returns(Task.FromResult(true));
        unitOfWork.Setup(_ => _.ContaCorrenteExtratoLeituraRepository)
            .Returns(extratoLeituraRepo.Object);
        unitOfWork.Setup(_ => _.AtividadeEscritaRepository).Returns(atividadeEscritaRepo.Object);
        unitOfWork.Setup(_ => _.ContaCorrenteExtratoEscritaRepository).Returns(extratoEscritaRepo.Object);
        extratoLeituraRepo.Setup(_ => _.BuscarExtratoPorMesEAno(command.ContaCorrenteId, command.UsuarioId,
                command.DataEstimado.Month, command.DataEstimado.Year))
            .Returns(Task.FromResult(new ContaCorrenteExtrato(command.ContaCorrenteId, Guid.Empty,
                command.DataEstimado.Month, command.DataEstimado.Year))!);
        unitOfWork.Setup(_ => _.Commit()).Returns(Task.FromResult(true));

        var resultado = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(handler.IsValid);
        Assert.False(resultado.Sucesso);
    }
}