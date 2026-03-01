using Oid85.FinMarket.Storage.Application.Interfaces.Services;

namespace Oid85.FinMarket.Storage.Application.Services
{
    /// <inheritdoc />
    public class JobService(
        IInstrumentService instrumentService,
        ICandleService candleService,
        IBondCouponService bondCouponService) 
        : IJobService
    {
        /// <inheritdoc />
        public Task LoadBondCouponsAsync() =>
            bondCouponService.LoadBondCouponsAsync();

        /// <inheritdoc />
        public Task LoadCandlesAsync() => 
            candleService.LoadCandlesAsync();

        /// <inheritdoc />
        public Task LoadInstrumentsAsync() => 
            instrumentService.LoadInstrumentsAsync();
    }
}
