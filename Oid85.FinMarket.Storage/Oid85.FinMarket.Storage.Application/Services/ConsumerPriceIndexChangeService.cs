using Oid85.FinMarket.Storage.Application.Interfaces.Repositories;
using Oid85.FinMarket.Storage.Application.Interfaces.Services;
using Oid85.FinMarket.Storage.Core.Models;
using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;

namespace Oid85.FinMarket.Storage.Application.Services
{
    public class ConsumerPriceIndexChangeService(
        IConsumerPriceIndexChangeRepository consumerPriceIndexChangeRepository) 
        : IConsumerPriceIndexChangeService
    {
        public async Task<CreateOrUpdateConsumerPriceIndexChangeResponse> CreateOrUpdateConsumerPriceIndexChangeAsync(CreateOrUpdateConsumerPriceIndexChangeRequest request)
        {
            await consumerPriceIndexChangeRepository.CreateOrUpdateConsumerPriceIndexChangeAsync(
                new ConsumerPriceIndexChange
                {
                    Date = request.Date,
                    Value = request.Value
                });

            return new();
        }

        public async Task<GetConsumerPriceIndexChangeListResponse> GetConsumerPriceIndexChangeListAsync(GetConsumerPriceIndexChangeListRequest request)
        {
            var models = await consumerPriceIndexChangeRepository.GetConsumerPriceIndexChangesAsync();

            if (models is null)
                return new GetConsumerPriceIndexChangeListResponse { ConsumerPriceIndexChanges = [] };

            var response = new GetConsumerPriceIndexChangeListResponse
            {
                ConsumerPriceIndexChanges = models
                .Select(x => new GetConsumerPriceIndexChangeListItemResponse
                {
                    Id = x.Id,
                    Date = x.Date,
                    Value = x.Value
                })
                .ToList()
            };

            return response;
        }
    }
}
