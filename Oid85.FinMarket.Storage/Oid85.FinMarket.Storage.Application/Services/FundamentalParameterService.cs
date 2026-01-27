using Oid85.FinMarket.Storage.Application.Interfaces.Repositories;
using Oid85.FinMarket.Storage.Application.Interfaces.Services;
using Oid85.FinMarket.Storage.Core.Models;
using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;

namespace Oid85.FinMarket.Storage.Application.Services
{
    /// <inheritdoc/>
    public class FundamentalParameterService(
        IFundamentalParameterRepository fundamentalParameterRepository) 
        : IFundamentalParameterService
    {
        /// <inheritdoc/>
        public async Task<CreateOrUpdateFundamentalParameterResponse> CreateOrUpdateFundamentalParameterAsync(CreateOrUpdateFundamentalParameterRequest request)
        {
            foreach (var item in request.FundamentalParameters)
                await fundamentalParameterRepository.CreateOrUpdateFundamentalParameterAsync(
                    new FundamentalParameter
                    {
                        Ticker = item.Ticker,
                        Type = item.Type,
                        Period = item.Period,
                        Value = item.Value
                    });

            return new();
        }

        /// <inheritdoc/>
        public async Task<GetFundamentalParameterListResponse> GetFundamentalParameterListAsync(GetFundamentalParameterListRequest request)
        {
            var models = await fundamentalParameterRepository.GetFundamentalParametersAsync();

            if (models is null)
                return new GetFundamentalParameterListResponse { FundamentalParameters = [] };

            var response = new GetFundamentalParameterListResponse
            {
                FundamentalParameters = models
                .Select(x => new GetFundamentalParameterListItemResponse
                {
                    Id = x.Id,
                    Ticker = x.Ticker,
                    Type = x.Type,
                    Period = x.Period,
                    Value = x.Value
                })
                .ToList()
            };

            return response;
        }
    }
}
