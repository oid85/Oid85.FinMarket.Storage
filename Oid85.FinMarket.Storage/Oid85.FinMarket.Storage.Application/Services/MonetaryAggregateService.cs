using Oid85.FinMarket.Storage.Application.Interfaces.Repositories;
using Oid85.FinMarket.Storage.Application.Interfaces.Services;
using Oid85.FinMarket.Storage.Core.Models;
using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;

namespace Oid85.FinMarket.Storage.Application.Services
{
    public class MonetaryAggregateService(
        IMonetaryAggregateRepository monetaryAggregateRepository) 
        : IMonetaryAggregateService
    {
        public async Task<CreateOrUpdateMonetaryAggregateResponse> CreateOrUpdateMonetaryAggregateAsync(CreateOrUpdateMonetaryAggregateRequest request)
        {
            await monetaryAggregateRepository.CreateOrUpdateMonetaryAggregateAsync(
                new MonetaryAggregate
                {
                    Date = request.Date,
                    M0 = request.M0,
                    M1 = request.M1,
                    M2 = request.M2,
                    M2X = request.M2X
                });

            return new();
        }

        public async Task<GetMonetaryAggregateListResponse> GetMonetaryAggregateListAsync(GetMonetaryAggregateListRequest request)
        {
            var models = await monetaryAggregateRepository.GetMonetaryAggregatesAsync();

            if (models is null)
                return new GetMonetaryAggregateListResponse { MonetaryAggregates = [] };

            var response = new GetMonetaryAggregateListResponse
            {
                MonetaryAggregates = models
                .Select(x => new GetMonetaryAggregateListItemResponse
                {
                    Id = x.Id,
                    Date = x.Date,
                    M0 = x.M0,
                    M1 = x.M1,
                    M2 = x.M2,
                    M2X = x.M2X
                })
                .ToList()
            };

            return response;
        }
    }
}
