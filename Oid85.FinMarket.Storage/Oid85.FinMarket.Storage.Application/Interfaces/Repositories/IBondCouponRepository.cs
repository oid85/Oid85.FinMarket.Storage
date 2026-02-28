
using Oid85.FinMarket.Storage.Core.Models;

namespace Oid85.FinMarket.Storage.Application.Interfaces.Repositories
{
    public interface IBondCouponRepository
    {
        Task AddAsync(List<BondCoupon> bondCoupons);
        Task<List<BondCoupon>?> GetBondCouponsAsync(string ticker, DateOnly from, DateOnly to);
    }
}
