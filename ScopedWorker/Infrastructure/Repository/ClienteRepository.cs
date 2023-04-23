using Microsoft.EntityFrameworkCore;
using ScopedWorker.Entities;
using ScopedWorker.Infrastructure.Database;

namespace ScopedWorker.Infrastructure.Repository;

public class ClienteRepository : IRepository<Cliente>
{
    private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
    public ClienteRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<Cliente>> GetAll()
    {
        using (var db = _contextFactory.CreateDbContext())
        {
            var clientes = await db.Customers.ToListAsync();
            return clientes;
        }
    }

    public async Task<Cliente> GetById(Guid id)
    {
        using (var db = _contextFactory.CreateDbContext())
        {
            var cliente = await db.Customers.SingleAsync(x=>x.Id == id);
            return cliente;
        }
    }
}
