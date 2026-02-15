using Microsoft.EntityFrameworkCore;
using Oid85.FinMarket.Storage.Application.Interfaces.Repositories;
using Oid85.FinMarket.Storage.Core.Models;
using Oid85.FinMarket.Storage.Infrastructure.Database.Entities;

namespace Oid85.FinMarket.Storage.Infrastructure.Database.Repositories
{
    /// <inheritdoc/>
    public class MonetaryAggregateRepository(
        IDbContextFactory<FinMarketContext> contextFactory) 
        : IMonetaryAggregateRepository
    {
        /// <inheritdoc/>
        public async Task<Guid?> CreateOrUpdateMonetaryAggregateAsync(MonetaryAggregate monetaryAggregate)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            var entity = await context.MonetaryAggregateEntities
                .FirstOrDefaultAsync(x => x.Date == monetaryAggregate.Date);

            if (entity is null)
            {
                entity = new MonetaryAggregateEntity
                {
                    Id = Guid.NewGuid(),
                    Date = monetaryAggregate.Date,
                    M0 = monetaryAggregate.M0,
                    M1 = monetaryAggregate.M1,
                    M2 = monetaryAggregate.M2,
                    M2X = monetaryAggregate.M2X
                };

                await context.AddAsync(entity);
            }

            else
            {
                entity.M0 = monetaryAggregate.M0;
                entity.M1 = monetaryAggregate.M1;
                entity.M2 = monetaryAggregate.M2;
                entity.M2X = monetaryAggregate.M2X;
            }

            await context.SaveChangesAsync();

            return entity.Id;
        }

        /// <inheritdoc/>
        public async Task<List<MonetaryAggregate>?> GetMonetaryAggregatesAsync()
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            var entities = await context.MonetaryAggregateEntities.ToListAsync();

            if (entities is null)
                return null;

            var models = entities
                .Select(x =>
                    new MonetaryAggregate
                    {
                        Id = x.Id,
                        Date = x.Date,
                        M0 = x.M0,
                        M1 = x.M1,
                        M2 = x.M2,
                        M2X = x.M2X
                    })
                .ToList();

            return models.OrderBy(x => x.Date).ToList();
        }
    }
}
