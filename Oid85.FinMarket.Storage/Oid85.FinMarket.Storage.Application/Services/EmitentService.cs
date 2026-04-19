using Oid85.FinMarket.Storage.Application.Interfaces.Repositories;
using Oid85.FinMarket.Storage.Application.Interfaces.Services;
using Oid85.FinMarket.Storage.Core.Models;
using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;

namespace Oid85.FinMarket.Storage.Application.Services
{
    public class EmitentService(
        IEmitentRepository emitentRepository) 
        : IEmitentService
    {
        public async Task<CreateOrUpdateEmitentResponse> CreateOrUpdateEmitentAsync(CreateOrUpdateEmitentRequest request)
        {
            await emitentRepository.CreateOrUpdateEmitentAsync(
                new Emitent
                {
                    Id = request.Id,
                    Name = request.Name ?? string.Empty,
                    Rating = request.Rating,
                    KeyWord = request.KeyWord
                });

            return new();
        }

        public async Task<GetEmitentListResponse> GetEmitentListAsync(GetEmitentListRequest request)
        {
            var models = await emitentRepository.GetEmitentsAsync();

            if (models is null)
                return new GetEmitentListResponse { Emitents = [] };

            var response = new GetEmitentListResponse
            {
                Emitents = models
                .Select(x => new GetEmitentListItemResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Rating = x.Rating,
                    KeyWord = x.KeyWord
                })
                .ToList()
            };

            return response;
        }
    }
}
