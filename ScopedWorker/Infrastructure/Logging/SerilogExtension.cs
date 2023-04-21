using Serilog;
using Serilog.Events;

namespace ScopedWorker.Infrastructure.Logging;

public class SerilogExtension
{
    public static void AddSerilog()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
            .WriteTo.Console()
            .CreateLogger();

        //Como gravar esse log no azure?
    }
}
