using Microsoft.EntityFrameworkCore;
using Oid85.FinMarket.Storage.Application.Interfaces.Repositories;
using Oid85.FinMarket.Storage.Core.Models;
using Oid85.FinMarket.Storage.Infrastructure.Database.Entities;

namespace Oid85.FinMarket.Storage.Infrastructure.Database.Repositories
{
    /// <inheritdoc/>
    public class CandleRepository(
        IDbContextFactory<FinMarketContext> contextFactory)
        : ICandleRepository
    {
        /// <inheritdoc/>
        public async Task AddAsync(List<Candle> candles)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            var entities = candles.Select(x =>
            new CandleEntity
            {
                Id = Guid.NewGuid(),
                Ticker = x.Ticker,
                Open = x.Open,
                Close = x.Close,
                Low = x.Low,
                High = x.High,
                Date = x.Date,
                Ticks = x.Ticks,
                Volume = x.Volume
            });

            foreach (var entity in entities)
            {
                var existEntity = await context.CandleEntities
                    .Where(x => x.Ticker == entity.Ticker)
                    .FirstOrDefaultAsync(x => x.Date == entity.Date);

                if (existEntity is null)
                    await context.AddRangeAsync(entity);
            }
            
            await context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task AddForceAsync(List<Candle> candles)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            var entities = candles.Select(x => 
            new CandleEntity
            {
                Id = Guid.NewGuid(),
                Ticker = x.Ticker,
                Open = x.Open,
                Close = x.Close,
                Low = x.Low,
                High = x.High,
                Date = x.Date,
                Ticks = x.Ticks,
                Volume = x.Volume
            });

            await context.AddRangeAsync(entities);
            await context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<Candle?> GetLastCandleByTickerAsync(string ticker)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            var entity = await context.CandleEntities
                .Where(x => x.Ticker == ticker)
                .OrderByDescending(x => x.Ticks)
                .FirstOrDefaultAsync();

            if (entity is null)
                return null;

            var model = new Candle
            {
                Id = entity.Id,
                Ticker = entity.Ticker,
                Open = entity.Open,
                Close = entity.Close,
                Low = entity.Low,
                High = entity.High,
                Date = entity.Date,
                Ticks = entity.Ticks,
                Volume = entity.Volume
            };

            return model;
        }
    }
}
