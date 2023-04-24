using ScopedWorker.Services;
using Polly;

namespace ScopedWorker;

public interface IScopedProcessingService
{
    Task DoWorkAsync(CancellationToken stoppingToken);
}

public class DefaultScopedProcessingService : IScopedProcessingService
{
    private readonly ILogger<DefaultScopedProcessingService> _logger;
    private readonly ICustomerService _customerService;
    private readonly AsyncPolicy _resiliencePolicy;

    public DefaultScopedProcessingService(ILogger<DefaultScopedProcessingService> logger,
        ICustomerService customerService, AsyncPolicy resiliencePolicy)
    {
        _logger = logger;
        _resiliencePolicy = resiliencePolicy;
        _customerService = customerService;
    }

    public async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("ScopedProcessing running at: {time}", DateTimeOffset.Now);

            try
            {
                var customers = _resiliencePolicy.ExecuteAsync(() => _customerService.GetAll());
                foreach (var customer in customers.Result)
                {
                    _logger.LogDebug(new string('*', 40));
                    _logger.LogDebug("ID: " + customer.Id);
                    _logger.LogDebug("Nome : " + customer.Name);
                    _logger.LogDebug("Email: " + customer.Email);
                    _logger.LogDebug(new string('*', 40) + "\n");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in ScopedProcessing");
            }

            await Task.Delay(3000, stoppingToken);
        }
    }
}