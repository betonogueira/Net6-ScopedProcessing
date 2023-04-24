using ScopedWorker.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using ScopedWorker.Domain.Entities;

namespace ScopedWorker.Infrastructure.Repository;

public class CustomerRepository : IRepository<Customer>
{
    private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
    public CustomerRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<Customer>> GetAll()
    {
        using (var db = _contextFactory.CreateDbContext())
        {
            var clientes = await db.Customers.ToListAsync();
            return clientes;
        }
    }

    public async Task<Customer> GetById(Guid id)
    {
        using (var db = _contextFactory.CreateDbContext())
        {
            var cliente = await db.Customers.SingleAsync(x=>x.Id == id);
            return cliente;
        }
    }
}
