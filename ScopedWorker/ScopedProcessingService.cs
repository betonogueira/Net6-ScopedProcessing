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
    private readonly IClienteService _clienteService;
    private readonly AsyncPolicy _resiliencePolicy;

    public DefaultScopedProcessingService(ILogger<DefaultScopedProcessingService> logger,
        IClienteService clienteService, AsyncPolicy resiliencePolicy)
    {
        _logger = logger;
        _resiliencePolicy = resiliencePolicy;
        _clienteService = clienteService;
    }

    public async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("ScopedProcessing running at: {time}", DateTimeOffset.Now);

            try
            {
                var clientes = _resiliencePolicy.ExecuteAsync(() => _clienteService.GetAll());
                foreach (var cliente in clientes.Result)
                {
                    _logger.LogDebug(new string('*', 40));
                    _logger.LogDebug("ID: " + cliente.Id);
                    _logger.LogDebug("Nome : " + cliente.Name);
                    _logger.LogDebug("Email: " + cliente.Email);
                    _logger.LogDebug(new string('*', 40) + "\n");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu uma excecao em ScopedProcessing");
            }

            await Task.Delay(3000, stoppingToken);
        }
    }
}