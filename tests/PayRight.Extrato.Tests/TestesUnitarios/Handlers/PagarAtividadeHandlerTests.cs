using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using PayRight.Extrato.Domain.Entities;
using PayRight.Extrato.Domain.Enums;
using PayRight.Extrato.Domain.GrpcService;
using PayRight.Extrato.Domain.Handlers;
using PayRight.Extrato.Domain.Repositories;
using PayRight.Extrato.Domain.UnitOfWork;
using PayRight.Extrato.Tests.TestesUnitarios.Commands.Fixtures;
using Xunit;

namespace PayRight.Extrato.Tests.TestesUnitarios.Handlers;

[Collection(nameof(PagarAtividadeContaCorrenteCommandCollection))]
public class PagarAtividadeHandlerTests
{
    private readonly PagarAtividadeContaCorrenteCommandFixture _commandFixture;

    public PagarAtividadeHandlerTests(PagarAtividadeContaCorrenteCommandFixture commandFixture)
    {
        _commandFixture = commandFixture;
    }

    [Trait("Handler", "PagarAtividade")]
    [Fact]
    public async Task DeveRetornarSucessoPagarAtividadeReceitaValido()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var extratoLeituraRepo = new Mock<IContaCorrenteExtratoLeituraRepository>();
        var atividadeLeituraRepo = new Mock<IAtividadeLeituraRepository>();
        var atividadeEscritaRepo = new Mock<IAtividadeEscritaRepository>();
        var grpcService = new Mock<IContaCorrenteGrpcService>();
        var command = _commandFixture.GerarCommand();
        var handler = new PagamentoAtividadeHandler(unitOfWork.Object, grpcService.Object);
        var atividade = new Atividade("teste", null, 30, TipoAtividade.Despesa, DateOnly.FromDateTime(DateTime.Today));
        var extrato = new ContaCorrenteExtrato(command.ContaCorrenteId, command.UsuarioId, DateTime.Today.Month,
            DateTime.Today.Year);
        extrato.AdicionarAtividade(atividade);
        
        // Act
        unitOfWork.Setup(_ => _.ContaCorrenteExtratoLeituraRepository).Returns(extratoLeituraRepo.Object);
        unitOfWork.Setup(_ => _.AtividadeLeituraRepository).Returns(atividadeLeituraRepo.Object);
        unitOfWork.Setup(_ => _.AtividadeEscritaRepository).Returns(atividadeEscritaRepo.Object);
        unitOfWork.Setup(_ => _.Commit()).Returns(Task.FromResult(true));
        extratoLeituraRepo.Setup(_ => _.VerificaSeContaCorrenteEhDoUsuario(command.ContaCorrenteId, command.UsuarioId))
            .Returns(Task.FromResult(true));
        atividadeLeituraRepo.Setup(_ => _.BuscarAtividadeComExtrato(command.ContaCorrenteId, command.AtividadeId))
            .Returns(Task.FromResult<Atividade?>(atividade));
        grpcService.Setup(_ => _.SubtrairSaldoContaCorrente(command.UsuarioId, command.ContaCorrenteId, atividade.Valor))
            .Returns(Task.FromResult(true));

        var resultado = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(resultado.Sucesso);
        Assert.True(handler.IsValid);
    }
    
    [Trait("Handler", "PagarAtividade")]
    [Fact]
    public async Task DeveRetornarSucessoPagarAtividadeDespesaValido()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var extratoLeituraRepo = new Mock<IContaCorrenteExtratoLeituraRepository>();
        var atividadeLeituraRepo = new Mock<IAtividadeLeituraRepository>();
        var atividadeEscritaRepo = new Mock<IAtividadeEscritaRepository>();
        var grpcService = new Mock<IContaCorrenteGrpcService>();
        var command = _commandFixture.GerarCommand();
        var handler = new PagamentoAtividadeHandler(unitOfWork.Object, grpcService.Object);
        var atividade = new Atividade("teste", null, 30, TipoAtividade.Receita, DateOnly.FromDateTime(DateTime.Today));
        var extrato = new ContaCorrenteExtrato(command.ContaCorrenteId, command.UsuarioId, DateTime.Today.Month,
            DateTime.Today.Year);
        extrato.AdicionarAtividade(atividade);
        
        // Act
        unitOfWork.Setup(_ => _.ContaCorrenteExtratoLeituraRepository).Returns(extratoLeituraRepo.Object);
        unitOfWork.Setup(_ => _.AtividadeLeituraRepository).Returns(atividadeLeituraRepo.Object);
        unitOfWork.Setup(_ => _.AtividadeEscritaRepository).Returns(atividadeEscritaRepo.Object);
        unitOfWork.Setup(_ => _.Commit()).Returns(Task.FromResult(true));
        extratoLeituraRepo.Setup(_ => _.VerificaSeContaCorrenteEhDoUsuario(command.ContaCorrenteId, command.UsuarioId))
            .Returns(Task.FromResult(true));
        atividadeLeituraRepo.Setup(_ => _.BuscarAtividadeComExtrato(command.ContaCorrenteId, command.AtividadeId))
            .Returns(Task.FromResult<Atividade?>(atividade));
        grpcService.Setup(_ => _.SomarSaldoContaCorrente(command.UsuarioId, command.ContaCorrenteId, atividade.Valor))
            .Returns(Task.FromResult(true));

        var resultado = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(resultado.Sucesso);
        Assert.True(handler.IsValid);
    }
    
    [Trait("Handler", "PagarAtividade")]
    [Fact]
    public async Task DeveRetornarErroPagarAtividadeContaCorrenteNaoEhDoUsuario()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var extratoLeituraRepo = new Mock<IContaCorrenteExtratoLeituraRepository>();
        var atividadeLeituraRepo = new Mock<IAtividadeLeituraRepository>();
        var atividadeEscritaRepo = new Mock<IAtividadeEscritaRepository>();
        var grpcService = new Mock<IContaCorrenteGrpcService>();
        var command = _commandFixture.GerarCommand();
        var handler = new PagamentoAtividadeHandler(unitOfWork.Object, grpcService.Object);
        var atividade = new Atividade("teste", null, 30, TipoAtividade.Receita, DateOnly.FromDateTime(DateTime.Today));
        var extrato = new ContaCorrenteExtrato(command.ContaCorrenteId, command.UsuarioId, DateTime.Today.Month,
            DateTime.Today.Year);
        extrato.AdicionarAtividade(atividade);
        
        // Act
        unitOfWork.Setup(_ => _.ContaCorrenteExtratoLeituraRepository).Returns(extratoLeituraRepo.Object);
        unitOfWork.Setup(_ => _.AtividadeLeituraRepository).Returns(atividadeLeituraRepo.Object);
        unitOfWork.Setup(_ => _.AtividadeEscritaRepository).Returns(atividadeEscritaRepo.Object);
        extratoLeituraRepo.Setup(_ => _.VerificaSeContaCorrenteEhDoUsuario(command.ContaCorrenteId, command.UsuarioId))
            .Returns(Task.FromResult(false));
        atividadeLeituraRepo.Setup(_ => _.BuscarAtividadeComExtrato(command.ContaCorrenteId, command.AtividadeId))
            .Returns(Task.FromResult<Atividade?>(atividade));
        
        var resultado = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(resultado.Sucesso);
        Assert.False(handler.IsValid);
    }
    
    [Trait("Handler", "PagarAtividade")]
    [Fact]
    public async Task DeveRetornarErroPagarAtividadeContaCorrenteAtividadeNaoExiste()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var atividadeLeituraRepo = new Mock<IAtividadeLeituraRepository>();
        var grpcService = new Mock<IContaCorrenteGrpcService>();
        var command = _commandFixture.GerarCommand();
        var handler = new PagamentoAtividadeHandler(unitOfWork.Object, grpcService.Object);

        // Act
        unitOfWork.Setup(_ => _.AtividadeLeituraRepository).Returns(atividadeLeituraRepo.Object);
        atividadeLeituraRepo.Setup(_ => _.BuscarAtividadeComExtrato(command.ContaCorrenteId, command.AtividadeId))
            .Returns(Task.FromResult<Atividade?>(null));
        
        var resultado = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(resultado.Sucesso);
        Assert.False(handler.IsValid);
    }
    
    [Trait("Handler", "PagarAtividade")]
    [Fact]
    public async Task DeveRetornarErroPagarAtividadeContaCorrenteComandoInvalido()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var grpcService = new Mock<IContaCorrenteGrpcService>();
        var command = _commandFixture.GerarCommand(Guid.Empty);
        var handler = new PagamentoAtividadeHandler(unitOfWork.Object, grpcService.Object);

        // Act
        var resultado = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(resultado.Sucesso);
        Assert.False(handler.IsValid);
    }
    
    [Trait("Handler", "PagarAtividade")]
    [Fact]
    public async Task DeveRetornarErroPagarAtividadeContaCorrenteProblemaNoCommit()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var extratoLeituraRepo = new Mock<IContaCorrenteExtratoLeituraRepository>();
        var atividadeLeituraRepo = new Mock<IAtividadeLeituraRepository>();
        var atividadeEscritaRepo = new Mock<IAtividadeEscritaRepository>();
        var grpcService = new Mock<IContaCorrenteGrpcService>();
        var command = _commandFixture.GerarCommand();
        var handler = new PagamentoAtividadeHandler(unitOfWork.Object, grpcService.Object);
        var atividade = new Atividade("teste", null, 30, TipoAtividade.Receita, DateOnly.FromDateTime(DateTime.Today));
        var extrato = new ContaCorrenteExtrato(command.ContaCorrenteId, command.UsuarioId, DateTime.Today.Month,
            DateTime.Today.Year);
        extrato.AdicionarAtividade(atividade);
        
        // Act
        unitOfWork.Setup(_ => _.ContaCorrenteExtratoLeituraRepository).Returns(extratoLeituraRepo.Object);
        unitOfWork.Setup(_ => _.AtividadeLeituraRepository).Returns(atividadeLeituraRepo.Object);
        unitOfWork.Setup(_ => _.AtividadeEscritaRepository).Returns(atividadeEscritaRepo.Object);
        unitOfWork.Setup(_ => _.Commit()).Returns(Task.FromResult(false));
        extratoLeituraRepo.Setup(_ => _.VerificaSeContaCorrenteEhDoUsuario(command.ContaCorrenteId, command.UsuarioId))
            .Returns(Task.FromResult(true));
        atividadeLeituraRepo.Setup(_ => _.BuscarAtividadeComExtrato(command.ContaCorrenteId, command.AtividadeId))
            .Returns(Task.FromResult<Atividade?>(atividade));
        grpcService.Setup(_ => _.SomarSaldoContaCorrente(command.UsuarioId, command.ContaCorrenteId, atividade.Valor))
            .Returns(Task.FromResult(true));
        
        var resultado = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(resultado.Sucesso);
        Assert.True(handler.IsValid);
    }
    
    [Trait("Handler", "PagarAtividade")]
    [Fact]
    public async Task DeveRetornarErroPagarAtividadeAtividadeJaConstaComoPaga()
    {
        // Arrange
        var unitOfWork = new Mock<IUnitOfWork>();
        var extratoLeituraRepo = new Mock<IContaCorrenteExtratoLeituraRepository>();
        var atividadeLeituraRepo = new Mock<IAtividadeLeituraRepository>();
        var atividadeEscritaRepo = new Mock<IAtividadeEscritaRepository>();
        var grpcService = new Mock<IContaCorrenteGrpcService>();
        var command = _commandFixture.GerarCommand();
        var handler = new PagamentoAtividadeHandler(unitOfWork.Object, grpcService.Object);
        var atividade = new Atividade("teste", null, 30, TipoAtividade.Receita, DateOnly.FromDateTime(DateTime.Today));
        var extrato = new ContaCorrenteExtrato(command.ContaCorrenteId, command.UsuarioId, DateTime.Today.Month,
            DateTime.Today.Year);
        extrato.AdicionarAtividade(atividade);
        atividade.PagarAtividade();
        
        // Act
        unitOfWork.Setup(_ => _.ContaCorrenteExtratoLeituraRepository).Returns(extratoLeituraRepo.Object);
        unitOfWork.Setup(_ => _.AtividadeLeituraRepository).Returns(atividadeLeituraRepo.Object);
        unitOfWork.Setup(_ => _.AtividadeEscritaRepository).Returns(atividadeEscritaRepo.Object);
        unitOfWork.Setup(_ => _.Commit()).Returns(Task.FromResult(true));
        extratoLeituraRepo.Setup(_ => _.VerificaSeContaCorrenteEhDoUsuario(command.ContaCorrenteId, command.UsuarioId))
            .Returns(Task.FromResult(true));
        atividadeLeituraRepo.Setup(_ => _.BuscarAtividadeComExtrato(command.ContaCorrenteId, command.AtividadeId))
            .Returns(Task.FromResult<Atividade?>(atividade));
        grpcService.Setup(_ => _.SomarSaldoContaCorrente(command.UsuarioId, command.ContaCorrenteId, atividade.Valor))
            .Returns(Task.FromResult(true));

        var resultado = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(resultado.Sucesso);
        Assert.False(handler.IsValid);
    }
}