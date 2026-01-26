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
                    Value = fundamentalParameter.Value
                };

                await context.AddAsync(entity);
            }

            else
            {
                entity.Value = fundamentalParameter.Value;               
            }

            await context.SaveChangesAsync();

            return entity.Id;
        }

        /// <inheritdoc/>
        public async Task<List<FundamentalParameter>?> GetFundamentalParametersAsync()
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            var entities = await context.FundamentalParameterEntities.ToListAsync();

            if (entities is null)
                return null;

            var models = entities
                .Select(x =>
                    new FundamentalParameter
                    {
                        Id = x.Id,
                        Ticker = x.Ticker,
                        Type = x.Type,
                        Period = x.Period,
                        Value = x.Value
                    })
                .ToList();

            return models;
        }
    }
}
