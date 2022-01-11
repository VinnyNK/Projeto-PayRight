using System;
using System.Threading.Tasks;
using Moq;
using PayRight.Conta.Domain.Messages;
using PayRight.Conta.Domain.Repositories;
using PayRight.Conta.Domain.Services;
using Xunit;

namespace PayRight.Conta.Tests.TestesUnitarios.Services;

public class CarteiraServiceTests
{
    [Trait("Service", "Carteira")]
    [Fact]
    public async Task DeveRetornarTrueCriarCarteiraNovoUsuario()
    {
        // Arrange
        var usuarioMessage = new UsuarioMessage()
        {
            AggregateId = Guid.NewGuid(),
            PrimeiroNome = "Astro"
        };
        var carteiraEscritaRepository = new Mock<ICarteiraEscritaRepository>();
        var service = new CarteiraService(carteiraEscritaRepository.Object);
        carteiraEscritaRepository.Setup(_ => _.Commit()).Returns(Task.FromResult(true));

        // Act
        var resultado = await service.CriarCarteiraNovoUsuarioCriado(usuarioMessage);

        // Assert
        Assert.True(resultado);
    }

    [Trait("Service", "Carteira")]
    [Fact]
    public async Task DeveRetornarFalsoQuandoOcorrerErroNoCommit()
    {
        // Arrange
        var usuarioMessage = new UsuarioMessage()
        {
            AggregateId = Guid.NewGuid(),
            PrimeiroNome = "Astro"
        };
        var carteiraEscritaRepository = new Mock<ICarteiraEscritaRepository>();
        var service = new CarteiraService(carteiraEscritaRepository.Object);
        carteiraEscritaRepository.Setup(_ => _.Commit()).Returns(Task.FromResult(false));

        // Act
        var resultado = await service.CriarCarteiraNovoUsuarioCriado(usuarioMessage);

        // Assert
        Assert.False(resultado);
    }
}