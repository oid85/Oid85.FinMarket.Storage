using Oid85.FinMarket.Storage.Core.Models;

namespace Oid85.FinMarket.Storage.Application.Interfaces.Adapters
{
    public interface IInvestApiClientAdapter
    {
        Task<List<Candle>> GetCandleAsync(Guid instrumentId, DateOnly from, DateOnly to);
        Task<List<Instrument>> GetInstrumentsAsync();
    }
}
