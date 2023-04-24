using ScopedWorker.Infrastructure.Repository;
using ScopedWorker.Infrastructure.Resilience;
using ScopedWorker.Infrastructure.Database;
using ScopedWorker.Domain.Entities;
using FluentMigrator.Runner;
using ScopedWorker.Services;
using System.Reflection;
using Polly;

namespace ScopedWorker.IoC;

public static class IocExtensions
{
    public static IServiceCollection AddDependencias(this IServiceCollection services)
    {
        services.AddScoped<IScopedProcessingService, DefaultScopedProcessingService>();
        services.AddScoped<IRepository<Customer>, CustomerRepository>();
        services.AddScoped<ICustomerService, CustomerService>();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddSingleton<AsyncPolicy>(
                        WaitAndRetryExtensions.CreateWaitAndRetryPolicy(new[]
                        {
                            TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(4), TimeSpan.FromSeconds(7)
                        }));

        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContextFactory<ApplicationDbContext>();
        
        services.AddLogging(c => c.AddFluentMigratorConsole())
                .AddFluentMigratorCore()
                .ConfigureRunner(c => c.AddSQLite()
                    .WithGlobalConnectionString("RepositorioPrincipal")
                    .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations());

        return services;
    }
}