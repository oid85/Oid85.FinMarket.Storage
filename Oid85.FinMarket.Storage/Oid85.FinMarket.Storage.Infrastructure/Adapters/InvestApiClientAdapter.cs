using Oid85.FinMarket.Storage.Application.Interfaces.Adapters;
using Oid85.FinMarket.Storage.Core.Models;

namespace Oid85.FinMarket.Storage.Infrastructure.Adapters
{
    public class InvestApiClientAdapter(
        GetInstrumentsHelper getInstrumentsHelper,
        GetCandlesHelper getCandlesHelper,
        GetBondCouponsHelper getBondCouponsHelper,
        GetPricesHelper getPricesHelper,
        GetDividendInfoHelper getDividendInfoHelper,
        GetForecastHelper getForecastHelper) 
        : IInvestApiClientAdapter
    {
        public Task<List<double>> GetLastPricesAsync(List<Guid> instrumentIds) =>
            getPricesHelper.GetPricesAsync(instrumentIds);

        public Task<List<BondCoupon>> GetBondCouponsAsync(List<Instrument> instruments) =>
            getBondCouponsHelper.GetBondCouponsAsync(instruments);

        public Task<List<Candle>> GetCandlesAsync(Guid instrumentId, DateOnly from, DateOnly to) =>
            getCandlesHelper.GetCandlesAsync(instrumentId, from, to);

        public Task<List<DividendInfo>> GetDividendInfosAsync(List<Instrument> instruments) =>
            getDividendInfoHelper.GetDividendInfoAsync(instruments);

        public Task<List<ForecastConsensus>> GetForecastConsensusesAsync(List<Instrument> instruments) =>
            getForecastHelper.GetForecastConsensusesAsync(instruments);

        public async Task<List<Instrument>> GetInstrumentsAsync() => 
            [
                ..await getInstrumentsHelper.GetSharesAsync(),
                ..await getInstrumentsHelper.GetFuturesAsync(),
                ..await getInstrumentsHelper.GetBondsAsync(),
                ..await getInstrumentsHelper.GetIndexesAsync()
            ];
    }
}
