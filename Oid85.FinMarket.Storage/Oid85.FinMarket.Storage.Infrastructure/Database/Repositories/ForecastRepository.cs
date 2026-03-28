using Microsoft.EntityFrameworkCore;
using Oid85.FinMarket.Storage.Application.Interfaces.Repositories;
using Oid85.FinMarket.Storage.Core.Models;
using Oid85.FinMarket.Storage.Infrastructure.Database.Entities;

namespace Oid85.FinMarket.Storage.Infrastructure.Database.Repositories
{
    public class ForecastRepository(
        IDbContextFactory<FinMarketContext> contextFactory)
        : IForecastRepository
    {
        public async Task AddAsync(List<ForecastConsensus> forecasts)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            foreach (var forecast in forecasts)
            {
                var existEntity = await context.ForecastConsensusEntities
                    .FirstOrDefaultAsync(
                    x =>
                        x.Ticker == forecast.Ticker);

                if (existEntity is null)
                {
                    var entity = new ForecastConsensusEntity
                    {
                        Id = Guid.NewGuid(),
                        InstrumentId = forecast.InstrumentId,
                        Ticker = forecast.Ticker,
                        Currency = forecast.Currency,
                        CurrentPrice = forecast.CurrentPrice,
                        ConsensusPrice = forecast.ConsensusPrice,
                        MinTarget = forecast.MinTarget,
                        MaxTarget = forecast.MaxTarget,
                        RecommendationString = forecast.RecommendationString,
                        PriceChange = forecast.PriceChange,
                        PriceChangeRel = forecast.PriceChangeRel
                    };

                    await context.AddAsync(entity);
                }

                else
                {
                    existEntity.InstrumentId = forecast.InstrumentId;
                    existEntity.Ticker = forecast.Ticker;
                    existEntity.Currency = forecast.Currency;
                    existEntity.CurrentPrice = forecast.CurrentPrice;
                    existEntity.ConsensusPrice = forecast.ConsensusPrice;
                    existEntity.MinTarget = forecast.MinTarget;
                    existEntity.MaxTarget = forecast.MaxTarget;
                    existEntity.RecommendationString = forecast.RecommendationString;
                    existEntity.PriceChange = forecast.PriceChange;
                    existEntity.PriceChangeRel = forecast.PriceChangeRel;
                }

                await context.SaveChangesAsync();
            }
        }

        public async Task<List<ForecastConsensus>> GetForecastsAsync()
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            var entities = await context.ForecastConsensusEntities
                .ToListAsync();

            if (entities is null)
                return [];

            var models = entities
                .Select(x =>
                new ForecastConsensus
                {
                    Id = x.Id,
                    Ticker = x.Ticker,
                    Currency = x.Currency,
                    CurrentPrice = x.CurrentPrice,
                    ConsensusPrice = x.ConsensusPrice,
                    MinTarget = x.MinTarget,
                    MaxTarget = x.MaxTarget,
                    RecommendationString = x.RecommendationString,
                    PriceChange = x.PriceChange,
                    PriceChangeRel = x.PriceChangeRel
                })
                .ToList();

            return models;
        }
    }
}
