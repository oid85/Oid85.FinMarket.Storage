using Oid85.FinMarket.Storage.Application.Interfaces.Repositories;
using Oid85.FinMarket.Storage.Application.Interfaces.Services;
using Oid85.FinMarket.Storage.Core.Models;
using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;

namespace Oid85.FinMarket.Storage.Application.Services
{
    public class VvpService(
        IVvpRepository vvpRepository) :
        IVvpService
    {
        public async Task<GetVvpListResponse> GetVvpListAsync(GetVvpListRequest request)
        {
            var models = await vvpRepository.GetVvpsAsync();

            if (models is null)
                return new GetVvpListResponse { Vvps = [] };

            var response = new GetVvpListResponse
            {
                Vvps = models
                .Select(x => new GetVvpListItemResponse
                {
                    Id = x.Id,
                    Date = x.Date,
                    Delta = x.Delta
                })
                .ToList()
            };

            return response;
        }
    }
}
