using Microsoft.EntityFrameworkCore;
using Oid85.FinMarket.Storage.Common.KnownConstants;
using Oid85.FinMarket.Storage.Infrastructure.Database.Entities;
using Oid85.FinMarket.Storage.Infrastructure.Database.Schemas;

namespace Oid85.FinMarket.Storage.Infrastructure.Database;

public class FinMarketContext(DbContextOptions<FinMarketContext> options) : DbContext(options)
{
    public DbSet<InstrumentEntity> InstrumentEntities { get; set; }
    public DbSet<CandleEntity> CandleEntities { get; set; }
    public DbSet<FundamentalParameterEntity> FundamentalParameterEntities { get; set; }
    public DbSet<ConsumerPriceIndexChangeEntity> ConsumerPriceIndexChangeEntities { get; set; }
    public DbSet<MonetaryAggregateEntity> MonetaryAggregateEntities { get; set; }
    public DbSet<KeyRateEntity> KeyRateEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .HasDefaultSchema(KnownDatabaseSchemas.Default)
            .ApplyConfigurationsFromAssembly(
                typeof(FinMarketContext).Assembly,
                type => type
                    .GetInterface(typeof(IFinMarketSchema).ToString()) != null)
            .UseIdentityAlwaysColumns();
    }    
}