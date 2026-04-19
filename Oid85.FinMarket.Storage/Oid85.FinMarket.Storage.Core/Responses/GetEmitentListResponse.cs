namespace Oid85.FinMarket.Storage.Core.Responses
{
    public class GetEmitentListResponse
    {
        public List<GetEmitentListItemResponse> Emitents { get; set; } = [];
    }

    public class GetEmitentListItemResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Rating { get; set; }
        public string? KeyWord { get; set; }
    }
}
