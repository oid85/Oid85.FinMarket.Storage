namespace Oid85.FinMarket.Storage.Core.Responses
{
    public class GetBondCouponListResponse
    {
        public List<GetBondCouponListItemResponse> BondCoupons { get; set; }
    }

    public class GetBondCouponListItemResponse
    {
        public object Ticker { get; set; }
        public object CouponNumber { get; set; }
        public object CouponPeriod { get; set; }
        public object CouponDate { get; set; }
        public object PayOneBond { get; set; }
    }
}
