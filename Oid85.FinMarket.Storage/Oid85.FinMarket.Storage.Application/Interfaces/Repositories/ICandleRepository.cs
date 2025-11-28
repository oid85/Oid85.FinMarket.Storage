
using Oid85.FinMarket.Storage.Core.Models;

namespace Oid85.FinMarket.Storage.Application.Interfaces.Repositories
{
    /// <summary>
    /// Репозиторий свечей
    /// </summary>
    public interface ICandleRepository
    {
        /// <summary>
        /// Добавить свечи
        /// </summary>
        Task AddAsync(List<Candle> candles);

        /// <summary>
        /// Добавить свечи (без проверки существующих)
        /// </summary>
        Task AddForceAsync(List<Candle> candles);

        /// <summary>
        /// Получить последнюю свечу по тикеру
        /// </summary>
        Task<Candle?> GetLastCandleByTickerAsync(string ticker);
    }
}
