using Oid85.FinMarket.Storage.Application.Interfaces.Repositories;
using Oid85.FinMarket.Storage.Application.Interfaces.Services;
using Oid85.FinMarket.Storage.Core.Models;
using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;

namespace Oid85.FinMarket.Storage.Application.Services
{
    public class KeyRateService(
        IKeyRateRepository keyRateRepository) : 
        IKeyRateService
    {
        public async Task<CreateOrUpdateKeyRateResponse> CreateOrUpdateKeyRateAsync(CreateOrUpdateKeyRateRequest request)
        {
            await keyRateRepository.CreateOrUpdateKeyRateAsync(
                new KeyRate
                {
                    Date = request.Date,
                    Value = request.Value
                });

            return new();
        }

        public async Task<GetKeyRateListResponse> GetKeyRateListAsync(GetKeyRateListRequest request)
        {
            var models = await keyRateRepository.GetKeyRatesAsync();

            if (models is null)
                return new GetKeyRateListResponse { KeyRates = [] };

            var response = new GetKeyRateListResponse
            {
                KeyRates = models
                .Select(x => new GetKeyRateListItemResponse
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
