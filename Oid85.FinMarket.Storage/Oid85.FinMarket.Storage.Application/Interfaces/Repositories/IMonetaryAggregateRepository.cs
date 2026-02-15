using Oid85.FinMarket.Storage.Core.Models;

namespace Oid85.FinMarket.Storage.Application.Interfaces.Repositories
{
    /// <summary>
    /// Репозиторий денежных агрегатов
    /// </summary>
    public interface IMonetaryAggregateRepository
    {
        Task<Guid?> CreateOrUpdateMonetaryAggregateAsync(MonetaryAggregate monetaryAggregate);
        Task<List<MonetaryAggregate>?> GetMonetaryAggregatesAsync();
    }
}
