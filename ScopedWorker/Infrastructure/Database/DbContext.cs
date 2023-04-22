using Microsoft.Data.Sqlite;
using System.Data;

namespace ScopedWorker.Infrastructure.Database;

public interface IDbContext
{
    IDbConnection CreateConnection();
}

public class DbContext : IDbContext
{
    private readonly IConfiguration _configuration;

    public DbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection CreateConnection()
        => new SqliteConnection(_configuration.GetConnectionString("RepositorioPrincipal"));
}