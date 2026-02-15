using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;

namespace Oid85.FinMarket.Storage.Application.Interfaces.Services
{
    /// <summary>
    /// Сервис индексов потребительских цен
    /// </summary>
    public interface IConsumerPriceIndexChangeService
    {
        Task<CreateOrUpdateConsumerPriceIndexChangeResponse> CreateOrUpdateConsumerPriceIndexChangeAsync(CreateOrUpdateConsumerPriceIndexChangeRequest request);
        Task<GetConsumerPriceIndexChangeListResponse> GetConsumerPriceIndexChangeListAsync(GetConsumerPriceIndexChangeListRequest request);
    }
}
