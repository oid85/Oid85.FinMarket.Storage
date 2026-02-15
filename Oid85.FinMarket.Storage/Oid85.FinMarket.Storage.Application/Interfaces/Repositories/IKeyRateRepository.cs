using Oid85.FinMarket.Storage.Core.Models;

namespace Oid85.FinMarket.Storage.Application.Interfaces.Repositories
{
    /// <summary>
    /// Репозиторий ключевой ставки
    /// </summary>
    public interface IKeyRateRepository
    {
        Task<Guid?> CreateOrUpdateKeyRateAsync(KeyRate keyRate);
        Task<List<KeyRate>?> GetKeyRatesAsync();
    }
}
