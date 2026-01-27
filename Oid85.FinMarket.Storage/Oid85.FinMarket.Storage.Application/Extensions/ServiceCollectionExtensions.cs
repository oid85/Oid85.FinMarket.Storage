using System.Linq.Expressions;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Oid85.FinMarket.Storage.Application.Interfaces.Services;
using Oid85.FinMarket.Storage.Application.Services;
using Oid85.FinMarket.Storage.Common.KnownConstants;

namespace Oid85.FinMarket.Storage.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ConfigureApplicationServices(
        this IServiceCollection services)
    {
        services.AddTransient<IInstrumentService, InstrumentService>();
        services.AddTransient<ICandleService, CandleService>();
        services.AddTransient<IFundamentalParameterService, FundamentalParameterService>();
        services.AddTransient<IJobService, JobService>();
    }

    public static async Task RegisterHangfireJobs(
        this IHost host,
        IConfiguration configuration)
    {
        var scopeFactory = host.Services.GetRequiredService<IServiceScopeFactory>();
        await using var scope = scopeFactory.CreateAsyncScope();
        var jobService = scope.ServiceProvider.GetRequiredService<IJobService>();

        RegisterJob(KnownJobs.LoadInstruments, () => jobService.LoadInstrumentsAsync());
        RegisterJob(KnownJobs.LoadCandles, () => jobService.LoadCandlesAsync());

        void RegisterJob(string configurationSection, Expression<Func<Task>> methodCall)
        {
            bool enable = configuration.GetValue<bool>($"Hangfire:{configurationSection}:Enable");
            string jobId = configuration.GetValue<string>($"Hangfire:{configurationSection}:JobId")!;
            string cron = configuration.GetValue<string>($"Hangfire:{configurationSection}:Cron")!;

            if (enable)
                RecurringJob.AddOrUpdate(jobId, methodCall, cron);
        }
    }
}