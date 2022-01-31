using System.Data;
using Dapper;
using MySqlConnector;

namespace PayRight.Conta.Grpc.Repositories;

public class ContaCorrenteRepository : IContaCorrenteRepository
{
    private IDbConnection _dbConnection;

    public ContaCorrenteRepository(IConfiguration configuration)
    {
        _dbConnection = new MySqlConnection(configuration.GetConnectionString("DbConnection"));
    }

    public async Task<bool> ExisteContaCorrente(Guid contaCorrenteId, Guid usuarioId)
    {
        const string query = "SELECT COUNT(C.Id) FROM contas_correntes C WHERE C.Id = @ContaCorrenteId AND C.UsuarioId = @UsuarioId";

        var result = await _dbConnection.QueryFirstOrDefaultAsync<int>(query, new
        {
            ContaCorrenteId = contaCorrenteId,
            UsuarioId = usuarioId
        });

        return result > 0;
    }
}