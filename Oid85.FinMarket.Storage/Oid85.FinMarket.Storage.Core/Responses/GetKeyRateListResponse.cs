namespace Oid85.FinMarket.Storage.Core.Responses
{
    public class GetKeyRateListResponse
    {
        public List<GetKeyRateListItemResponse> KeyRates { get; set; } = [];
    }

    public class GetKeyRateListItemResponse
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public double? Value { get; set; }
    }
}
