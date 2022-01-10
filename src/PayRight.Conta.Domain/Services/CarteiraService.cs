using PayRight.Conta.Domain.Entities;
using PayRight.Conta.Domain.Models;
using PayRight.Conta.Domain.Repositories;

namespace PayRight.Conta.Domain.Services;

public class CarteiraService : ICarteiraService
{
    private readonly ICarteiraEscritaRepository _carteiraEscritaRepository;

    public CarteiraService(ICarteiraEscritaRepository carteiraEscritaRepository)
    {
        _carteiraEscritaRepository = carteiraEscritaRepository;
    }

    public async Task<bool> CriarCarteiraNovoUsuarioCriado(UsuarioMessage usuario)
    {
        var carteira = new Carteira(usuario.AggregateId);

        await _carteiraEscritaRepository.CriarNovaCarteira(carteira);

        return await _carteiraEscritaRepository.Commit();
    }
}