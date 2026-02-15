namespace Oid85.FinMarket.Storage.Core.Responses
{
    public class GetMonetaryAggregateListResponse
    {
        public List<GetMonetaryAggregateListItemResponse> MonetaryAggregates { get; set; } = [];
    }

    public class GetMonetaryAggregateListItemResponse
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public double? M0 { get; set; }
        public double? M1 { get; set; }
        public double? M2 { get; set; }
        public double? M2X { get; set; }
    }
}
