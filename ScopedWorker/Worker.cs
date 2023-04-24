using FluentMigrator.Runner;

namespace ScopedWorker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;


    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation($"{nameof(Worker)} is running.");

        await DoWorkAsync(stoppingToken);
    }

    private async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation($"{nameof(Worker)} is working.");

        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            IScopedProcessingService scopedProcessingService =
                scope.ServiceProvider.GetRequiredService<IScopedProcessingService>();

            var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

            try
            {
                migrationService.ListMigrations();
                migrationService.MigrateUp();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while running migrations");
            }

            await scopedProcessingService.DoWorkAsync(stoppingToken);
        }
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation($"{nameof(Worker)} is stopping.");

        await base.StopAsync(stoppingToken);
    }
}