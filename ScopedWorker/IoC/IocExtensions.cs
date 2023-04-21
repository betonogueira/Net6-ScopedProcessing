using Polly;
using System.Reflection;
using FluentMigrator.Runner;
using ScopedWorker.Entities;
using ScopedWorker.Services;
using ScopedWorker.Infrastructure.Dapper;
using ScopedWorker.Infrastructure.Repository;
using ScopedWorker.Infrastructure.Resilience;
using ScopedWorker;

namespace ScopedWorker.IoC;

public static class IocExtensions
{
    public static IServiceCollection AddDependencias(this IServiceCollection services)
    {
        services.AddScoped<IScopedProcessingService, DefaultScopedProcessingService>();
        services.AddScoped<IRepository<Cliente>, ClienteRepository>();
        services.AddScoped<IClienteService, ClienteService>();

        services.AddSingleton<AsyncPolicy>(
                        WaitAndRetryExtensions.CreateWaitAndRetryPolicy(new[]
                        {
                            TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(4), TimeSpan.FromSeconds(7)
                        }));

        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddScoped<IDapperContext, DapperContext>();
        
        services.AddLogging(c => c.AddFluentMigratorConsole())
                .AddFluentMigratorCore()
                .ConfigureRunner(c => c.AddSQLite()
                    .WithGlobalConnectionString("RepositorioPrincipal")
                    .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations());

        return services;
    }
}