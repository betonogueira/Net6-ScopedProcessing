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

            //A ideia era aqui conectar numa fila do RabbitMQ e persistir o retorno no Oracle
            //Mas como exemplo ficou apenas consultando o DB, o scoped processing serve
            //para instanciar e dar dispose em tudo a cada rodada
            try
            {
                var clientes = _resiliencePolicy.ExecuteAsync(() => _clienteService.ObterTodos());
                foreach (var cliente in clientes.Result)
                {
                    _logger.LogDebug(new string('*', 40));
                    _logger.LogDebug("ID: " + cliente.Id);
                    _logger.LogDebug("Nome : " + cliente.Name);
                    _logger.LogDebug("Email: " + cliente.Email);
                    _logger.LogDebug(new string('*', 40));
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