namespace Oid85.FinMarket.Storage.Core.Responses
{
    public class GetInstrumentListResponse
    {
        public List<GetInstrumentListItemResponse> Instruments { get; set; }
    }

    public class GetInstrumentListItemResponse
    {
        /// <summary>
        /// Тикер
        /// </summary>
        public string Ticker { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Дата погашения
        /// </summary>
        public DateOnly? MaturityDate { get; set; } = null;

        /// <summary>
        /// Кол-во купонов в год
        /// </summary>
        public int? CouponQuantityPerYear { get; set; } = null;

        /// <summary>
        /// НКД
        /// </summary>
        public double? Nkd { get; set; } = null;
    }
}
