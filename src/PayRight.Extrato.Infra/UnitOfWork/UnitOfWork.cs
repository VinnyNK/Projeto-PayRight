using PayRight.Extrato.Domain.Repositories;
using PayRight.Extrato.Domain.UnitOfWork;
using PayRight.Extrato.Infra.Contexts;
using PayRight.Extrato.Infra.Repositories;

namespace PayRight.Extrato.Infra.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ContextoDbLeitura _contextoDbLeitura;
    private readonly ContextoDbEscrita _contextoDbEscrita;
    private ContaCorrenteExtratoLeituraRepository? _contaCorrenteExtratoLeituraRepository;
    private ContaCorrenteExtratoEscritaRepository? _contaCorrenteExtratoEscritaRepository;
    private AtividadeLeituraRepository? _atividadeLeituraRepository;
    private AtividadeEscritaRepository? _atividadeEscritaRepository;

    public UnitOfWork(ContextoDbLeitura contextoDbLeitura,  ContextoDbEscrita contextoDbEscrita)
    {
        _contextoDbLeitura = contextoDbLeitura;
        _contextoDbEscrita = contextoDbEscrita;
    }
    
    public IContaCorrenteExtratoLeituraRepository ContaCorrenteExtratoLeituraRepository
    {
        get
        {
            return _contaCorrenteExtratoLeituraRepository ??=
                new ContaCorrenteExtratoLeituraRepository(_contextoDbLeitura);
        }
    }

    public IContaCorrenteExtratoEscritaRepository ContaCorrenteExtratoEscritaRepository
    {
        get
        {
            return _contaCorrenteExtratoEscritaRepository ??=
                new ContaCorrenteExtratoEscritaRepository(_contextoDbEscrita);
        }
    }

    public IAtividadeLeituraRepository AtividadeLeituraRepository
    {
        get
        {
            return _atividadeLeituraRepository ??= new AtividadeLeituraRepository(_contextoDbLeitura);
        }
    }

    public IAtividadeEscritaRepository AtividadeEscritaRepository
    {
        get
        {
            return _atividadeEscritaRepository ??= new AtividadeEscritaRepository(_contextoDbEscrita);
        }
    }
    public async Task<bool> Commit()
    {
        return await _contextoDbEscrita.SaveChangesAsync() > 0;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}