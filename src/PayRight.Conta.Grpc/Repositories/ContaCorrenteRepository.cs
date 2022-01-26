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
        const string query = "SELECT COUNT(C.\"ID\") FROM contas_correntes C WHERE C.\"ID\" = @ContaCorrenteId AND C.\"UsuarioId\" = @UsuarioId";

        return await _dbConnection.QueryFirstOrDefaultAsync<bool>(query, new
        {
            ContaCorrenteId = contaCorrenteId,
            UsuarioId = usuarioId
        });
    }
}