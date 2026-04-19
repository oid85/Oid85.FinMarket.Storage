using Microsoft.EntityFrameworkCore;
using Oid85.FinMarket.Storage.Application.Interfaces.Repositories;
using Oid85.FinMarket.Storage.Core.Models;
using Oid85.FinMarket.Storage.Infrastructure.Database.Entities;

namespace Oid85.FinMarket.Storage.Infrastructure.Database.Repositories
{
    /// <inheritdoc/>
    public class KeyRateRepository(
        IDbContextFactory<FinMarketContext> contextFactory) 
        : IKeyRateRepository
    {
        /// <inheritdoc/>
        public async Task<Guid?> CreateOrUpdateKeyRateAsync(KeyRate keyRate)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            var entity = await context.KeyRateEntities
                .FirstOrDefaultAsync(x => x.Date == keyRate.Date);

            if (entity is null)
            {
                entity = new KeyRateEntity
                {
                    Id = Guid.NewGuid(),
                    Date = keyRate.Date,
                    Value = keyRate.Value
                };

                await context.AddAsync(entity);
            }

            else
            {
                entity.Value = keyRate.Value;               
            }

            await context.SaveChangesAsync();

            return entity.Id;
        }

        /// <inheritdoc/>
        public async Task<List<KeyRate>?> GetKeyRatesAsync()
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            var entities = await context.KeyRateEntities.ToListAsync();

            if (entities is null)
                return null;

            var models = entities
                .Select(x =>
                    new KeyRate
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
