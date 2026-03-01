namespace Oid85.FinMarket.Storage.Core.Responses
{
    public class GetBondCouponListResponse
    {
        public List<GetBondCouponListItemResponse> BondCoupons { get; set; }
    }

    public class GetBondCouponListItemResponse
    {
        public string Ticker { get; set; }
        public long CouponNumber { get; set; }
        public int CouponPeriod { get; set; }
        public DateOnly CouponDate { get; set; }
        public double PayOneBond { get; set; }
    }
}
