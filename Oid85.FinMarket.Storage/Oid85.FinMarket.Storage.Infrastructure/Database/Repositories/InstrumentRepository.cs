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
                InstrumentId = instrument.InstrumentId,
                Ticker = instrument.Ticker,
                Name = instrument.Name,
                MaturityDate = instrument.MaturityDate,
                Type = instrument.Type
            };

            await context.AddAsync(entity);
            await context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<List<Instrument>?> GetActiveInstrumentsAsync()
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            var entities = await context.InstrumentEntities.Where(x => x.IsActive).ToListAsync();

            if (entities is null)
                return null;

            var models = entities
                .Select(x => 
                    new Instrument
                    {
                        Id = x.Id,
                        InstrumentId = x.InstrumentId,
                        Ticker = x.Ticker,
                        Name = x.Name,
                        Type = x.Type,
                        MaturityDate = x.MaturityDate,
                        IsActive = x.IsActive
                    })
                .ToList();

            return models;
        }
    }
}
