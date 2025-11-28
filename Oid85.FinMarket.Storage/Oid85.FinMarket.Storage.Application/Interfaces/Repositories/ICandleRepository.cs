
using Oid85.FinMarket.Storage.Core.Models;

namespace Oid85.FinMarket.Storage.Application.Interfaces.Repositories
{
    public interface ICandleRepository
    {
        Task AddAsync(List<Candle> candles);
        Task AddForceAsync(List<Candle> candles);
        Task<Candle?> GetLastCandleByTickerAsync(string ticker);
    }
}
