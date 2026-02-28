namespace Oid85.FinMarket.Storage.Core.Requests
{
    public class GetBondCouponListRequest
    {
        public string Ticker { get; set; }
        public DateOnly From { get; set; }
        public DateOnly To { get; set; }
    }
}
