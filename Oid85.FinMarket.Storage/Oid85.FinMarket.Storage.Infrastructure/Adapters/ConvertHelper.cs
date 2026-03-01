using Google.Protobuf.WellKnownTypes;
using Tinkoff.InvestApi.V1;

namespace Oid85.FinMarket.Storage.Infrastructure.Adapters
{
    public static class ConvertHelper
    {
        public static Timestamp DateTimeToTimestamp(DateTime dateTime) =>
            Timestamp.FromDateTime(dateTime.ToUniversalTime());

        public static DateOnly TimestampToDateOnly(Timestamp timestamp)
        {
            if (timestamp is null)
                return DateOnly.MinValue;

            return DateOnly.FromDateTime(timestamp.ToDateTime());
        }

        public static Timestamp DateOnlyToTimestamp(DateOnly dateOnly) =>
            Timestamp.FromDateTime(dateOnly.ToDateTime(TimeOnly.MinValue).ToUniversalTime());

        public static double MoneyValueToDouble(MoneyValue moneyValue)
        {
            if (moneyValue is null)
                return 0.0;

            return moneyValue.Units + moneyValue.Nano / 1_000_000_000.0;
        }

        public static double QuotationToDouble(Quotation quotation) =>
            quotation is null ? 0.0 : quotation.Units + quotation.Nano / 1_000_000_000.0;
    }
}
