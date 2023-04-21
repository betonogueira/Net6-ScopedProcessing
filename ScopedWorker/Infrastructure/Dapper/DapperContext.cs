using Microsoft.Data.Sqlite;
using System.Data;

namespace ScopedWorker.Infrastructure.Dapper;

public interface IDapperContext
{
    IDbConnection CreateConnection();
}

public class DapperContext : IDapperContext
{
    private readonly IConfiguration _configuration;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection CreateConnection()
        => new SqliteConnection(_configuration.GetConnectionString("RepositorioPrincipal"));
}