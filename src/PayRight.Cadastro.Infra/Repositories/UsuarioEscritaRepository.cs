using PayRight.Cadastro.Domain.Entities;
using PayRight.Cadastro.Domain.Repositories;
using PayRight.Cadastro.Infra.Contexts;

namespace PayRight.Cadastro.Infra.Repositories;

public class UsuarioEscritaRepository : Repository<Usuario>, IUsuarioEscritaRepository
{
    public UsuarioEscritaRepository(ContextoDbCoadastro db) : base(db)
    { }

    public async Task CriarNovoUsuario(Usuario usuario)
    {
        await DbSet.AddAsync(usuario);
    }

    public void AtualizarUsuario(Usuario usuario)
    {
        DbSet.Update(usuario);
    }

    public async Task<bool> Commit()
    {
        return await Db.SaveChangesAsync() != 0;
    }
}