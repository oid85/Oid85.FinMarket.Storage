using Microsoft.EntityFrameworkCore;
using Oid85.FinMarket.Storage.Application.Interfaces.Repositories;
using Oid85.FinMarket.Storage.Core.Models;
using Oid85.FinMarket.Storage.Infrastructure.Database.Entities;

namespace Oid85.FinMarket.Storage.Infrastructure.Database.Repositories
{
    public class DividendRepository(
        IDbContextFactory<FinMarketContext> contextFactory)
        : IDividendRepository
    {
        public async Task AddAsync(List<DividendInfo> dividends)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            foreach (var dividend in dividends)
            {
                var existEntity = await context.DividendInfoEntities
                    .FirstOrDefaultAsync(
                    x =>
                        x.Ticker == dividend.Ticker &&
                        x.RecordDate == dividend.RecordDate &&
                        x.DeclaredDate == dividend.DeclaredDate);

                if (existEntity is null)
                {
                    var entity = new DividendInfoEntity
                    {
                        Id = Guid.NewGuid(),
                        InstrumentId = dividend.InstrumentId,
                        Ticker = dividend.Ticker,
                        RecordDate = dividend.RecordDate,
                        DeclaredDate = dividend.DeclaredDate,
                        Dividend = dividend.Dividend,
                        DividendPrc = dividend.DividendPrc
                    };

                    await context.AddAsync(entity);
                }

                else
                {
                    existEntity.InstrumentId = dividend.InstrumentId;
                    existEntity.Ticker = dividend.Ticker;
                    existEntity.RecordDate = dividend.RecordDate;
                    existEntity.DeclaredDate = dividend.DeclaredDate;
                    existEntity.Dividend = dividend.Dividend;
                    existEntity.DividendPrc = dividend.DividendPrc;
                }

                await context.SaveChangesAsync();
            }
        }

        public async Task<List<DividendInfo>> GetDividendsAsync()
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            var entities = await context.DividendInfoEntities
                .Where(x => x.RecordDate >= DateOnly.FromDateTime(DateTime.Today))
                .OrderBy(x => x.RecordDate)
                .ToListAsync();

            if (entities is null)
                return [];

            var models = entities
                .Select(x =>
                new DividendInfo
                {
                    Id = x.Id,
                    Ticker = x.Ticker,
                    RecordDate = x.RecordDate,
                    DeclaredDate = x.DeclaredDate,
                    Dividend = x.Dividend,
                    DividendPrc = x.DividendPrc
                })
                .ToList();

            return models;
        }
    }
}
