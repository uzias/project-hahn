using Api.Infrastructure.Core.Contracts;

namespace WorkerService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;

    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope();

            IFetchApiJob job =
                scope.ServiceProvider.GetRequiredService<IFetchApiJob>();

            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            _logger.LogDebug("Worker running at: {time}", DateTimeOffset.Now);
            await job.RunAsync();
            
            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }

    
}
