using Dapper;
using ScopedWorker.Entities;
using ScopedWorker.Infrastructure.Dapper;

namespace ScopedWorker.Infrastructure.Repository;

public class ClienteRepository : IRepository<Cliente>
{
    private IDapperContext _dapperContext;
    public ClienteRepository(IDapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }
    public async Task<IEnumerable<Cliente>> GetAll()
    {
        using (var db = _dapperContext.CreateConnection())
        {
            var query = "Select Id,Name,Email From Clientes";
            var clientes = await db.QueryAsync<Cliente>(query);
            return clientes;
        }
    }
}
