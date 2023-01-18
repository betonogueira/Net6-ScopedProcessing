using Polly;
using Polly.Retry;
using Serilog;

namespace RabbitOracleWorker.Infrastructure.Resilience;

public static class WaitAndRetryExtensions
{
    public static AsyncRetryPolicy CreateWaitAndRetryPolicy(IEnumerable<TimeSpan> sleepsBeetweenRetries)
    {
        return Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
                sleepDurations: sleepsBeetweenRetries,
                onRetry: (_, span, retryCount, _) =>
                {
                    Log.Logger.Information($"Retentativa: {retryCount} | " +
                        $"Tempo de Espera em segundos: {span.TotalSeconds}");
                });
    }
}