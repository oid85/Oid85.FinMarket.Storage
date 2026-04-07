namespace Oid85.FinMarket.Storage.Core.Requests
{
    public class GetFundamentalParameterListRequest
    {
        public string? Ticker { get; set; } = null;
        public List<string>? Periods { get; set; } = null;
    }
}
