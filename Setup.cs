namespace TheRandomCoder.VisitorAnalyzer;

public static class Setup
{
    public static WebApplication CreateApp(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddHostedService<UpdateService>();
        builder.Services.AddMemoryCache();
        return builder.Build();
    }
}