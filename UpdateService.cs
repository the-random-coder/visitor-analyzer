
namespace TheRandomCoder.VisitorAnalyzer;

public class UpdateService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var period = TimeSpan.FromDays(1);
        using var timer = new PeriodicTimer(period);
        while (await timer.WaitAsync(stoppingToken))
        {
            try
            {
                await Updater.Update();
            }
            catch (Exception) { }
        }
    }
}