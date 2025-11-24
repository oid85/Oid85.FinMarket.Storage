using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Oid85.FinMarket.Storage.Application.Interfaces.Repositories;
using Oid85.FinMarket.Storage.Common.KnownConstants;
using Oid85.FinMarket.Storage.Infrastructure.Database;
using Oid85.FinMarket.Storage.Infrastructure.Database.Repositories;

namespace Oid85.FinMarket.Storage.Infrastructure.Database.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ConfigureInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {    
        services.AddDbContextPool<FinMarketContext>((serviceProvider, options) =>
        {  
            options.UseNpgsql(configuration.GetValue<string>(KnownSettingsKeys.PostgresAthleticConnectionString)!);
        });

        services.AddPooledDbContextFactory<FinMarketContext>(options =>
            options
                .UseNpgsql(configuration.GetValue<string>(KnownSettingsKeys.PostgresAthleticConnectionString)!)
                .EnableServiceProviderCaching(false), poolSize: 32);

        services.AddTransient<IInstrumentRepository, InstrumentRepository>();
    }

    public static async Task ApplyMigrations(this IHost host)
    {
        var scopeFactory = host.Services.GetRequiredService<IServiceScopeFactory>();
        await using var scope = scopeFactory.CreateAsyncScope();
        await using var context = scope.ServiceProvider.GetRequiredService<FinMarketContext>();
        await context.Database.MigrateAsync();
    }
}