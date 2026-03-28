using Microsoft.EntityFrameworkCore;
using Oid85.FinMarket.Storage.Application.Interfaces.Repositories;
using Oid85.FinMarket.Storage.Common.KnownConstants;
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
            {
                if (instrument.Nkd is not null)
                    entity.Nkd = instrument.Nkd;

                if (instrument.LastPrice is not null)
                    entity.LastPrice = instrument.LastPrice;

                if (instrument.Nominal is not null)
                    entity.Nominal = instrument.Nominal;

                if (instrument.Nominal is not null)
                    entity.Currency = instrument.Currency;
            }

            else
            {
                entity = new InstrumentEntity
                {
                    Id = Guid.NewGuid(),
                    InstrumentId = instrument.InstrumentId,
                    Ticker = instrument.Ticker,
                    Name = instrument.Name,
                    MaturityDate = instrument.MaturityDate,
                    CouponQuantityPerYear = instrument.CouponQuantityPerYear,
                    Nkd = instrument.Nkd,
                    LastPrice = instrument.LastPrice,
                    Nominal = instrument.Nominal,
                    Currency = instrument.Currency,
                    Type = instrument.Type                   
                };

                await context.AddAsync(entity);
            }
            
            await context.SaveChangesAsync();

            return entity.Id;
        }

        /// <inheritdoc/>
        public async Task DeleteOldBondsAsync()
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            await context.InstrumentEntities
                .Where(x => x.Type == KnownInstrumentTypes.Bond)
                .Where(x => x.MaturityDate != null)
                .Where(x => x.MaturityDate < DateOnly.FromDateTime(DateTime.Today))
                .ExecuteDeleteAsync();

            await context.SaveChangesAsync();
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
                        CouponQuantityPerYear = x.CouponQuantityPerYear,
                        Nkd = x.Nkd,
                        LastPrice = x.LastPrice,
                        Nominal = x.Nominal,
                        Currency = x.Currency,
                        IsActive = x.IsActive
                    })
                .ToList();

            return models;
        }
    }
}
