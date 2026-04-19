using Microsoft.EntityFrameworkCore;
using Oid85.FinMarket.Storage.Application.Interfaces.Repositories;
using Oid85.FinMarket.Storage.Core.Models;
using Oid85.FinMarket.Storage.Infrastructure.Database.Entities;

namespace Oid85.FinMarket.Storage.Infrastructure.Database.Repositories
{
    public class EmitentRepository(
        IDbContextFactory<FinMarketContext> contextFactory)
        : IEmitentRepository
    {
        public async Task CreateOrUpdateEmitentAsync(Emitent emitent)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            var entity = await context.EmitentEntities
                .FirstOrDefaultAsync(x => x.Id == emitent.Id);

            if (entity is null)
            {
                entity = new EmitentEntity
                {
                    Id = Guid.NewGuid(),
                    Name = emitent.Name,
                    Rating = emitent.Rating,
                    KeyWord = emitent.KeyWord
                };

                await context.AddAsync(entity);
            }

            else
            {
                entity.Name = emitent.Name;
                entity.Rating = emitent.Rating;
                entity.KeyWord = emitent.KeyWord;
            }

            await context.SaveChangesAsync();
        }

        public async Task<List<Emitent>> GetEmitentsAsync()
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            var entities = await context.EmitentEntities.ToListAsync();

            if (entities is null)
                return [];

            var models = entities
                .Select(x =>
                    new Emitent
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Rating = x.Rating,
                        KeyWord = x.KeyWord
                    })
                .ToList();

            return models.OrderBy(x => x.Name).ToList();
        }
    }
}
