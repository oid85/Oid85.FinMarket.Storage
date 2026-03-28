using Oid85.FinMarket.Storage.Application.Interfaces.Services;

namespace Oid85.FinMarket.Storage.Application.Services
{
    /// <inheritdoc />
    public class JobService(
        IInstrumentService instrumentService,
        ICandleService candleService,
        IBondCouponService bondCouponService,
        IDividendService dividendService,
        IForecastService forecastService) 
        : IJobService
    {
        /// <inheritdoc />
        public Task LoadBondCouponsAsync() =>
            bondCouponService.LoadBondCouponsAsync();

        /// <inheritdoc />
        public Task LoadBondCouponsAsync(string ticker) =>
            bondCouponService.LoadBondCouponsAsync(ticker);

        /// <inheritdoc />
        public Task LoadCandlesAsync() => 
            candleService.LoadCandlesAsync();

        /// <inheritdoc />
        public Task LoadDividendsAsync() =>
            dividendService.LoadDividendsAsync();

        /// <inheritdoc />
        public Task LoadForecastConsensusesAsync() =>
            forecastService.LoadForecastConsensusesAsync();

        /// <inheritdoc />
        public Task LoadInstrumentsAsync() => 
            instrumentService.LoadInstrumentsAsync();
    }
}
