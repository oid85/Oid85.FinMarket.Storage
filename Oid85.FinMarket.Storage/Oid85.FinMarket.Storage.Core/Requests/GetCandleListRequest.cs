using Oid85.FinMarket.Storage.Core.Models;

namespace Oid85.FinMarket.Storage.Core.Requests
{
    public class GetCandleListRequest
    {
        public string Ticker { get; set; }
        public DateOnly From { get; set; }
        public DateOnly To { get; set; }
    }
}
