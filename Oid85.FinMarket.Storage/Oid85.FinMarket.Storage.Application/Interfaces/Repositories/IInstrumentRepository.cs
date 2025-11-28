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
        /// Получить активные инструменты
        /// </summary>
        /// <returns></returns>
        Task<List<Instrument>?> GetActiveInstrumentsAsync();
    }
}
