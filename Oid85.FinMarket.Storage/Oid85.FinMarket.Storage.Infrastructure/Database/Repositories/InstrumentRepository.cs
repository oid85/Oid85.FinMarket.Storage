using Microsoft.EntityFrameworkCore;
using Oid85.FinMarket.Storage.Application.Interfaces.Repositories;
using Oid85.FinMarket.Storage.Core.Models;
using Oid85.FinMarket.Storage.Infrastructure.Database.Entities;

namespace Oid85.FinMarket.Storage.Infrastructure.Database.Repositories
{
    /// <inheritdoc/>
    public class InstrumentRepository(
        IDbContextFactory<FinMarketContext> contextFactory)
        : IInstrumentRepository
    {
        /// <inheritdoc/>
        public async Task<Guid?> AddAsync(Instrument instrument)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            var entity = await context.InstrumentEntities.FirstOrDefaultAsync(x => x.Ticker == instrument.Ticker);

            if (entity is not null)
                return null;

            entity = new InstrumentEntity
            {
                Id = Guid.NewGuid(),
                Ticker = instrument.Ticker,
                Name = instrument.Name,
                Type = instrument.Type
            };

            await context.AddAsync(entity);
            await context.SaveChangesAsync();

            return entity.Id;
        }
    }
}
