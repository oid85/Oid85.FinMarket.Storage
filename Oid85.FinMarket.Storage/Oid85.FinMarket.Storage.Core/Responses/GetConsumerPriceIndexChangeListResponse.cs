namespace Oid85.FinMarket.Storage.Core.Responses
{
    public class GetConsumerPriceIndexChangeListResponse
    {
        public List<GetConsumerPriceIndexChangeListItemResponse> ConsumerPriceIndexChanges { get; set; } = [];
    }

    public class GetConsumerPriceIndexChangeListItemResponse
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public double? Value { get; set; }
    }
}
