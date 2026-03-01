using NLog;
using Tinkoff.InvestApi;
using Tinkoff.InvestApi.V1;

namespace Oid85.FinMarket.Storage.Infrastructure.Adapters;

public class GetPricesHelper(
    ILogger logger,
    InvestApiClient client)
{
    private const int ChunkSize = 50;
    private const int DelayInMilliseconds = 1000;
    
    public async Task<List<double>> GetPricesAsync(List<Guid> instrumentIds)
    {
        var chunks = instrumentIds.Chunk(ChunkSize);

        var result = new List<double>();

        foreach (var chunk in chunks)
        {
            var chunkInstrumentIds = chunk.ToList();
            result.AddRange(await GetPricesByChunkAsync(chunkInstrumentIds));
        }
        
        return result;
    }

    private async Task<List<double>> GetPricesByChunkAsync(List<Guid> chunkInstrumentIds)
    {
        await Task.Delay(DelayInMilliseconds);
            
        var request = CreateGetLastPricesRequest(chunkInstrumentIds);
        var response = await SendGetLastPricesRequest(request);
                
        if (response is null)
            return [];
                
        var result = response.LastPrices
            .Select(x => ConvertHelper.QuotationToDouble(x.Price))
            .ToList();

        return result;      
    }
    
    private async Task<GetLastPricesResponse?> SendGetLastPricesRequest(GetLastPricesRequest request)
    {
        try
        {
            return await client.MarketData.GetLastPricesAsync(request);
        }
        
        catch (Exception exception)
        {
            logger.Error(exception, "Ошибка получения данных. {request}", request);
            return null;
        }
    }
    
    private static GetLastPricesRequest CreateGetLastPricesRequest(List<Guid> instrumentIds)
    {
        var request = new GetLastPricesRequest();
        request.InstrumentId.AddRange(instrumentIds.Select(x => x.ToString()));
        request.LastPriceType = LastPriceType.LastPriceExchange;

        return request;
    }
}