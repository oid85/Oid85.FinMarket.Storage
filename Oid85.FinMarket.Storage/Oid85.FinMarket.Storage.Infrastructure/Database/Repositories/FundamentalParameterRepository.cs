using Microsoft.EntityFrameworkCore;
using Oid85.FinMarket.Storage.Application.Interfaces.Repositories;
using Oid85.FinMarket.Storage.Core.Models;
using Oid85.FinMarket.Storage.Infrastructure.Database.Entities;

namespace Oid85.FinMarket.Storage.Infrastructure.Database.Repositories
{
    /// <inheritdoc/>
    public class FundamentalParameterRepository(
        IDbContextFactory<FinMarketContext> contextFactory) 
        : IFundamentalParameterRepository
    {
        /// <inheritdoc/>
        public async Task<Guid?> CreateOrUpdateFundamentalParameterAsync(FundamentalParameter fundamentalParameter)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            var entity = await context.FundamentalParameterEntities
                .FirstOrDefaultAsync(
                x =>
                    x.Ticker == fundamentalParameter.Ticker &&
                    x.Type == fundamentalParameter.Type &&
                    x.Period == fundamentalParameter.Period);

            if (entity is null)
            {
                entity = new FundamentalParameterEntity
                {
                    Id = Guid.NewGuid(),
                    Ticker = fundamentalParameter.Ticker,
                    Type = fundamentalParameter.Type,
                    Period = fundamentalParameter.Period,
                    Value = fundamentalParameter.Value,
                    ExtData = fundamentalParameter.ExtData
                };

                await context.AddAsync(entity);
            }

            else
            {
                entity.Value = fundamentalParameter.Value;
                entity.ExtData = fundamentalParameter.ExtData;
            }

            await context.SaveChangesAsync();

            return entity.Id;
        }

        /// <inheritdoc/>
        public async Task<List<FundamentalParameter>?> GetFundamentalParametersAsync(string? ticker, List<string>? periods)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            var entities = context.FundamentalParameterEntities.AsQueryable();

            if (ticker is not null)
                entities = entities.Where(x => x.Ticker == ticker);

            if (periods is not null)
                entities = entities.Where(x => periods.Contains(x.Period));

            var filteredEntities = await entities.ToListAsync();

            if (filteredEntities is null)
                return null;

            var models = filteredEntities
                .Select(x =>
                    new FundamentalParameter
                    {
                        Id = x.Id,
                        Ticker = x.Ticker,
                        Type = x.Type,
                        Period = x.Period,
                        Value = x.Value,
                        ExtData = x.ExtData
                    })
                .ToList();

            return models;
        }
    }
}
