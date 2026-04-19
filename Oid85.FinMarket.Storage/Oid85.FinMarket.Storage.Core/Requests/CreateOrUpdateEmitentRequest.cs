namespace Oid85.FinMarket.Storage.Core.Requests
{
    public class CreateOrUpdateEmitentRequest
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Rating { get; set; }
        public string? KeyWord { get; set; }
    }
}
