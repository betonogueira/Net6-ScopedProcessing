using Dapper;
using ScopedWorker.Entities;
using ScopedWorker.Infrastructure.Database;

namespace ScopedWorker.Infrastructure.Repository;

public class ClienteRepository : IRepository<Cliente>
{
    private IDbContext _dbContext;
    public ClienteRepository(IDbContext context)
    {
        _dbContext = context;
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

    public async Task<Cliente> GetById(Guid id)
    {
        using (var db = _dbContext.CreateConnection())
        {
            var query = "Select Id,Name,Email From Clientes Where Id=@idGuid";
            var parameters = new { idGuid = id };
            var cliente = await db.QueryFirstAsync<Cliente>(query, parameters);
            return cliente;
        }
    }
}
