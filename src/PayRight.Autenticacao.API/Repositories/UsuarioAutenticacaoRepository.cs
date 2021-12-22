using Microsoft.EntityFrameworkCore;
using PayRight.Autenticacao.API.Context;
using PayRight.Autenticacao.API.Models;

namespace PayRight.Autenticacao.API.Repositories;

public class UsuarioAutenticacaoRepository : IUsuarioAutenticacaoRepository
{
    private readonly DbSet<Usuario> _dbSet;
    
    public UsuarioAutenticacaoRepository(ContextoDbLeitura db)
    {
        _dbSet = db.Set<Usuario>();
    }

    public async Task<Usuario?> BuscaUsuarioPorEmail(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(_ => _.Email == email);
    }
}