using System.Text.Json.Serialization;
using Hangfire;
using Oid85.FinMarket.Storage.Application.Extensions;
using Oid85.FinMarket.Storage.Common.Converters;
using Oid85.FinMarket.Storage.Common.KnownConstants;
using Oid85.FinMarket.Storage.Infrastructure.Extensions;
using Oid85.FinMarket.Storage.WebHost.Extensions;

namespace Oid85.FinMarket.Storage.WebHost
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                    options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.AllowNamedFloatingPointLiterals;
                });

            builder.Services.AddMemoryCache();
            builder.Services.ConfigureLogger();
            builder.Services.ConfigureCors(builder.Configuration);
            builder.Services.ConfigureHangfire();
            builder.Services.ConfigureApplicationServices();
            builder.Services.ConfigureDatabase(builder.Configuration);
            builder.Services.ConfigureInvestApiClient(builder.Configuration);

            builder.Services.AddWindowsService(options =>
            {
                options.ServiceName = "Oid85.FinMarket.Storage";
            });

            builder.Services.AddOpenApi();

            bool applyMigrations = builder.Configuration.GetValue<bool>(KnownSettingsKeys.PostgresApplyMigrationsOnStart);
            int port = builder.Configuration.GetValue<int>(KnownSettingsKeys.DeployPort);

            var app = builder.Build();

            if (applyMigrations)
                await app.ApplyMigrations();

            app.UseRouting();

            app.UseCors("CorsPolicy");
           
            app.UseHangfireDashboard("/dashboard");

            await app.RegisterHangfireJobs(builder.Configuration);

            app.MapControllers();

            app.MapOpenApi();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v1.json", "v1");
            });

            app.Urls.Add($"http://0.0.0.0:{port}");

            await app.RunAsync();
        }
    }
}
