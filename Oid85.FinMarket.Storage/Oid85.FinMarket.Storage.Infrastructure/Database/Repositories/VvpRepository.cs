using Microsoft.EntityFrameworkCore;
using Oid85.FinMarket.Storage.Application.Interfaces.Repositories;
using Oid85.FinMarket.Storage.Core.Models;
using Oid85.FinMarket.Storage.Infrastructure.Database.Entities;

namespace Oid85.FinMarket.Storage.Infrastructure.Database.Repositories
{
    /// <inheritdoc/>
    public class VvpRepository(
        IDbContextFactory<FinMarketContext> contextFactory) 
        : IVvpRepository
    {
        /// <inheritdoc/>
        public async Task<List<Vvp>?> GetVvpsAsync()
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            var entities = await context.VvpEntities.ToListAsync();

            if (entities is null)
                return null;

            var models = entities
                .Select(x =>
                    new Vvp
                    {
                        Id = x.Id,
                        Date = x.Date,
                        Delta = x.Delta
                    })
                .ToList();

            return models.OrderBy(x => x.Date).ToList();
        }
    }
}
