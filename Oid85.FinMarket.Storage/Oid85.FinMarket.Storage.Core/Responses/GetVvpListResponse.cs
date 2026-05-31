namespace Oid85.FinMarket.Storage.Core.Responses
{
    public class GetVvpListResponse
    {
        public List<GetVvpListItemResponse> Vvps { get; set; } = [];
    }

    public class GetVvpListItemResponse
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public double? Delta { get; set; }
    }
}
