using Hangfire;
using NLog;
using ILogger = NLog.ILogger;

namespace Oid85.FinMarket.Storage.WebHost.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ConfigureLogger(this IServiceCollection services)
    {
        LogManager
            .Setup()
            .LoadConfigurationFromFile("nlog.config");

        services.AddTransient(typeof(ILogger), _ => 
            LogManager.GetLogger(AppDomain.CurrentDomain.FriendlyName));
    }
    
    public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.SetIsOriginAllowed(_ => true);
                builder.AllowCredentials();
            });
        });
    }

    public static void ConfigureHangfire(
    this IServiceCollection services)
    {
        services.AddHangfire(config => config.UseInMemoryStorage());
        services.AddHangfireServer();
    }
}