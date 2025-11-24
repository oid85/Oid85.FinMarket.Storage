using Microsoft.Extensions.DependencyInjection;
using Oid85.FinMarket.Storage.Application.Interfaces.Services;
using Oid85.FinMarket.Storage.Application.Services;

namespace Oid85.FinMarket.Storage.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ConfigureApplicationServices(
        this IServiceCollection services)
    {
        services.AddTransient<IInstrumentService, InstrumentService>();
    }
}