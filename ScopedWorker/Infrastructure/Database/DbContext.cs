using Microsoft.EntityFrameworkCore;
using ScopedWorker.Domain.Entities;

namespace ScopedWorker.Infrastructure.Database;

public class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DbSet<Customer> Customers { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(_configuration.GetConnectionString("RepositorioPrincipal"));
    }
}