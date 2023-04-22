using Dapper;
using ScopedWorker.Entities;
using ScopedWorker.Infrastructure.Database;

namespace ScopedWorker.Infrastructure.Repository;

public class ClienteRepository : IRepository<Cliente>
{
    private IDbContext _dbContext;
    public ClienteRepository(IDbContext dapperContext)
    {
        _dbContext = dapperContext;
    }
    public async Task<IEnumerable<Cliente>> GetAll()
    {
        using (var db = _dbContext.CreateConnection())
        {
            var query = "Select Id,Name,Email From Clientes";
            var clientes = await db.QueryAsync<Cliente>(query);
            return clientes;
        }
    }
}
