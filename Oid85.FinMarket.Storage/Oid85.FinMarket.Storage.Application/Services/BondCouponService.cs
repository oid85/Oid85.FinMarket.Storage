using System.Diagnostics.Metrics;
using Oid85.FinMarket.Storage.Application.Interfaces.Adapters;
using Oid85.FinMarket.Storage.Application.Interfaces.Repositories;
using Oid85.FinMarket.Storage.Application.Interfaces.Services;
using Oid85.FinMarket.Storage.Common.KnownConstants;
using Oid85.FinMarket.Storage.Core.Models;
using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;

namespace Oid85.FinMarket.Storage.Application.Services
{
    /// <inheritdoc />
    public class BondCouponService(
        IInvestApiClientAdapter investApiClientAdapter,
        IBondCouponRepository bondCouponRepository,
        IInstrumentRepository instrumentRepository) 
        : IBondCouponService
    {
        /// <inheritdoc />
        public async Task<GetBondCouponListResponse> GetBondCouponListAsync(GetBondCouponListRequest request)
        {
            var bondCoupons = await bondCouponRepository.GetBondCouponsAsync(request.Ticker, request.From, request.To);

            if (bondCoupons is null)
                return new GetBondCouponListResponse { BondCoupons = [] };

            return new GetBondCouponListResponse
            {
                BondCoupons = bondCoupons
                .Select(x =>
                new GetBondCouponListItemResponse
                {
                    Ticker = x.Ticker,
                    CouponNumber = x.CouponNumber,
                    CouponPeriod = x.CouponPeriod,
                    CouponDate = x.CouponDate,                 
                    PayOneBond = x.PayOneBond
                })
                .ToList()
            };
        }

        /// <inheritdoc />
        public async Task LoadBondCouponsAsync()
        {
            var instruments = (await instrumentRepository.GetActiveInstrumentsAsync())?.Where(x => x.Type == KnownInstrumentTypes.Bond).ToList();

            if (instruments is null)
                return;

            var bondCoupons = await investApiClientAdapter.GetBondCouponsAsync(instruments);

            await bondCouponRepository.AddAsync(bondCoupons);
        }
    }
}
