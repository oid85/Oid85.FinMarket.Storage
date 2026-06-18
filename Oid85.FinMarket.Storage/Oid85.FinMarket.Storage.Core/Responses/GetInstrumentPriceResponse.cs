namespace Oid85.FinMarket.Storage.Core.Responses
{
    public class GetInstrumentPriceResponse
    {
        public List<TickerPriceItem> Items { get; set; } = [];
    }

    public class TickerPriceItem
    {
        public string Ticker { get; set; } = string.Empty;
        public double Price { get; set; }
    }
}
