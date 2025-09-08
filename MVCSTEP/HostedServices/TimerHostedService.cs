namespace MVCSTEP.HostedServices;

public class TimerHostedService : IHostedService, IDisposable
{
    private Timer? _timer;
    private readonly ILogger<TimerHostedService> _logger;
 
    public TimerHostedService(ILogger<TimerHostedService> logger)
    {
        _logger = logger;
    }
 
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("TimerHostedService запущен.");
        // Запускаем задачу каждые 5 секунд
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        return Task.CompletedTask;
    }
 
    private void DoWork(object? state)
    {
        _logger.LogInformation("Фоновая задача выполняется: {time}", DateTime.Now);
    }
 
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("TimerHostedService останавливается...");
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }
 
    public void Dispose()
    {
        _timer?.Dispose();
    }
}