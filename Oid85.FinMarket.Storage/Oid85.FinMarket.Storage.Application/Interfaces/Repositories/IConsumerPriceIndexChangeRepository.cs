using Oid85.FinMarket.Storage.Core.Models;

namespace Oid85.FinMarket.Storage.Application.Interfaces.Repositories
{
    /// <summary>
    /// Репозиторий индексов потребительских цен
    /// </summary>
    public interface IConsumerPriceIndexChangeRepository
    {
        Task<Guid?> CreateOrUpdateConsumerPriceIndexChangeAsync(ConsumerPriceIndexChange consumerPriceIndexChange);
        Task<List<ConsumerPriceIndexChange>?> GetConsumerPriceIndexChangesAsync();
    }
}
