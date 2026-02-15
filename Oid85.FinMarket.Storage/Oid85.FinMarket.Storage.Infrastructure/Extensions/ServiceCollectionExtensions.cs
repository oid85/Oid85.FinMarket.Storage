using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Oid85.FinMarket.Storage.Application.Interfaces.Adapters;
using Oid85.FinMarket.Storage.Application.Interfaces.Repositories;
using Oid85.FinMarket.Storage.Common.KnownConstants;
using Oid85.FinMarket.Storage.Common.Utils;
using Oid85.FinMarket.Storage.Infrastructure.Adapters;
using Oid85.FinMarket.Storage.Infrastructure.Database;
using Oid85.FinMarket.Storage.Infrastructure.Database.Repositories;

namespace Oid85.FinMarket.Storage.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ConfigureDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {    
        services.AddDbContextPool<FinMarketContext>((serviceProvider, options) =>
        {  
            options.UseNpgsql(configuration.GetValue<string>(KnownSettingsKeys.PostgresFinMarketStorageConnectionString)!);
        });

        services.AddPooledDbContextFactory<FinMarketContext>(options =>
            options
                .UseNpgsql(configuration.GetValue<string>(KnownSettingsKeys.PostgresFinMarketStorageConnectionString)!)
                .EnableServiceProviderCaching(false), poolSize: 32);

        services.AddTransient<IInstrumentRepository, InstrumentRepository>();
        services.AddTransient<ICandleRepository, CandleRepository>();
        services.AddTransient<IFundamentalParameterRepository, FundamentalParameterRepository>();
        services.AddTransient<IConsumerPriceIndexChangeRepository, ConsumerPriceIndexChangeRepository>();
        services.AddTransient<IMonetaryAggregateRepository, MonetaryAggregateRepository>();
        services.AddTransient<IKeyRateRepository, KeyRateRepository>();
    }

    public static void ConfigureInvestApiClient(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInvestApiClient((_, settings) =>
        {
            settings.AccessToken = StringUtils.Base64Decode(
                configuration.GetValue<string>(KnownSettingsKeys.TinkoffToken)!);
        });

        services.AddTransient<GetInstrumentsHelper>();
        services.AddTransient<GetCandlesHelper>();

        services.AddTransient<IInvestApiClientAdapter, InvestApiClientAdapter>();
    }

    public static async Task ApplyMigrations(this IHost host)
    {
        var scopeFactory = host.Services.GetRequiredService<IServiceScopeFactory>();
        await using var scope = scopeFactory.CreateAsyncScope();
        await using var context = scope.ServiceProvider.GetRequiredService<FinMarketContext>();
        await context.Database.MigrateAsync();
    }
}