using Oid85.FinMarket.Storage.Core.Models;

namespace Oid85.FinMarket.Storage.Application.Interfaces.Adapters
{
    public interface IInvestApiClientAdapter
    {
        Task<List<Candle>> GetCandlesAsync(Guid instrumentId, DateOnly from, DateOnly to);
        Task<List<BondCoupon>> GetBondCouponsAsync(List<Instrument> instruments);
        Task<List<Instrument>> GetInstrumentsAsync();
        Task<List<double>> GetLastPricesAsync(List<Guid> instrumentIds);
    }
}
