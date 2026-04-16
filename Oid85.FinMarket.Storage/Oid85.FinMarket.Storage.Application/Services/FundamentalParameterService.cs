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
                        Value = item.Value,
                        ExtData = item.ExtData
                    });

            return new();
        }

        /// <inheritdoc/>
        public async Task<GetFundamentalParameterListResponse> GetFundamentalParameterListAsync(GetFundamentalParameterListRequest request)
        {
            var models = await fundamentalParameterRepository.GetFundamentalParametersAsync(request.Ticker, request.Periods);

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
                    Value = x.Value,
                    ExtData = x.ExtData
                })
                .ToList()
            };

            return response;
        }

        /// <inheritdoc/>
        public async Task DividendImportAsync()
        {
            string separator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            string path = @"c:\Users\79131\Downloads\dividends.txt";
            var lines = await File.ReadAllLinesAsync(path);

            string ticker = lines[0].Trim();

            for (int i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split('\t');

                var value = Convert.ToDouble(parts[1].Trim().Replace(",", separator).Replace(".", separator));

                var fundamentalParameter = new FundamentalParameter
                {
                    Ticker = ticker,
                    Type = "Dividend",
                    Period = parts[0].Trim(),
                    Value = value,
                    ExtData = string.Empty
                };

                await fundamentalParameterRepository.CreateOrUpdateFundamentalParameterAsync(fundamentalParameter);
            }
        }
    }
}
