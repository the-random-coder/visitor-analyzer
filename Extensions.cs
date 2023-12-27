namespace TheRandomCoder.VisitorAnalyzer;

public static class Extensions
{
    public static async Task<bool> WaitAsync(this PeriodicTimer timer, CancellationToken stoppingToken)
    {
        return !stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken);
    }
}