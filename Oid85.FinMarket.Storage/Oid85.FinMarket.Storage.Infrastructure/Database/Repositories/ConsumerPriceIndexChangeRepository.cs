using Microsoft.EntityFrameworkCore;
using Oid85.FinMarket.Storage.Application.Interfaces.Repositories;
using Oid85.FinMarket.Storage.Core.Models;
using Oid85.FinMarket.Storage.Infrastructure.Database.Entities;

namespace Oid85.FinMarket.Storage.Infrastructure.Database.Repositories
{
    /// <inheritdoc/>
    public class ConsumerPriceIndexChangeRepository(
        IDbContextFactory<FinMarketContext> contextFactory) 
        : IConsumerPriceIndexChangeRepository
    {
        /// <inheritdoc/>
        public async Task<Guid?> CreateOrUpdateConsumerPriceIndexChangeAsync(ConsumerPriceIndexChange consumerPriceIndexChange)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            var entity = await context.ConsumerPriceIndexChangeEntities
                .FirstOrDefaultAsync(x => x.Date == consumerPriceIndexChange.Date);

            if (entity is null)
            {
                entity = new ConsumerPriceIndexChangeEntity
                {
                    Id = Guid.NewGuid(),
                    Date = consumerPriceIndexChange.Date,
                    Value = consumerPriceIndexChange.Value
                };

                await context.AddAsync(entity);
            }

            else
            {
                entity.Value = consumerPriceIndexChange.Value;               
            }

            await context.SaveChangesAsync();

            return entity.Id;
        }

        /// <inheritdoc/>
        public async Task<List<ConsumerPriceIndexChange>?> GetConsumerPriceIndexChangesAsync()
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            var entities = await context.ConsumerPriceIndexChangeEntities.ToListAsync();

            if (entities is null)
                return null;

            var models = entities
                .Select(x =>
                    new ConsumerPriceIndexChange
                    {
                        Id = x.Id,
                        Date = x.Date,
                        Value = x.Value
                    })
                .ToList();

            return models.OrderBy(x => x.Date).ToList();
        }
    }
}
