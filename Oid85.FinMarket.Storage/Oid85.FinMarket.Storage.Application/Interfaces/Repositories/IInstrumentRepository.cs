using Oid85.FinMarket.Storage.Core.Models;

namespace Oid85.FinMarket.Storage.Application.Interfaces.Repositories
{
    /// <summary>
    /// Репозиторий инструментов
    /// </summary>
    public interface IInstrumentRepository
    {
        /// <summary>
        /// Добавление инструмента
        /// </summary>
        Task<Guid?> AddAsync(Instrument instrument);
        
        /// <summary>
        /// Удалить облигации с истекшим сроком погашения
        /// </summary>
        Task DeleteOldBondsAsync();

        /// <summary>
        /// Получить активные инструменты
        /// </summary>
        Task<List<Instrument>?> GetActiveInstrumentsAsync();

        /// <summary>
        /// Получить инструменты
        /// </summary>
        Task<List<Instrument>?> GetInstrumentsAsync();

        /// <summary>
        /// Установить флаг активности
        /// </summary>
        Task SetActiveFlagAsync(Guid instrumentId, bool value);
    }
}
