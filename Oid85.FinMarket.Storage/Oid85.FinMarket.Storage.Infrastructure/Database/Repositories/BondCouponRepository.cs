using Microsoft.EntityFrameworkCore;
using Oid85.FinMarket.Storage.Application.Interfaces.Repositories;
using Oid85.FinMarket.Storage.Core.Models;
using Oid85.FinMarket.Storage.Infrastructure.Database.Entities;
using static Grpc.Core.Metadata;

namespace Oid85.FinMarket.Storage.Infrastructure.Database.Repositories
{
    public class BondCouponRepository(
        IDbContextFactory<FinMarketContext> contextFactory) 
        : IBondCouponRepository
    {
        public async Task AddAsync(List<BondCoupon> bondCoupons)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            foreach (var bondCoupon in bondCoupons)
            {
                var existEntity = await context.BondCouponEntities
                    .Where(x => x.Ticker == bondCoupon.Ticker)
                    .FirstOrDefaultAsync(x => x.CouponNumber == bondCoupon.CouponNumber);

                if (existEntity is null)
                {
                    var entity = new BondCouponEntity
                    {
                        Id = Guid.NewGuid(),
                        InstrumentId = bondCoupon.InstrumentId,
                        Ticker = bondCoupon.Ticker,
                        CouponDate = bondCoupon.CouponDate,
                        CouponNumber = bondCoupon.CouponNumber,
                        CouponPeriod = bondCoupon.CouponPeriod,
                        PayOneBond = bondCoupon.PayOneBond
                    };

                    await context.AddAsync(entity);
                }

                else
                {
                    existEntity.PayOneBond = bondCoupon.PayOneBond;
                }

                await context.SaveChangesAsync();
            }
        }

        public async Task<List<BondCoupon>?> GetBondCouponsAsync(string ticker, DateOnly from, DateOnly to)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            var entities = await context.BondCouponEntities
                .Where(x => x.Ticker == ticker)
                .Where(x => x.CouponDate >= from)
                .Where(x => x.CouponDate <= to)
                .OrderBy(x => x.CouponDate)
                .ToListAsync();

            if (entities is null)
                return [];

            var models = entities
                .Select(x =>
                new BondCoupon
                {
                    Id = x.Id,
                    Ticker = x.Ticker,
                    CouponNumber = x.CouponNumber,
                    CouponPeriod = x.CouponPeriod,
                    CouponDate = x.CouponDate,
                    PayOneBond = x.PayOneBond
                })
                .ToList();

            return models;
        }
    }
}
