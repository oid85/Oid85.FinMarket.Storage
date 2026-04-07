namespace Oid85.FinMarket.Storage.Core.Requests
{
    public class GetFundamentalParameterListRequest
    {
        public string? Ticker { get; set; }
        public List<string>? Periods { get; set; }
    }
}
