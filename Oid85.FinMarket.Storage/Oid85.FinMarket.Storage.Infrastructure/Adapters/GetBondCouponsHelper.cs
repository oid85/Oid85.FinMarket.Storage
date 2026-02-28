using Google.Protobuf.WellKnownTypes;
using NLog;
using Oid85.FinMarket.Storage.Core.Models;
using Tinkoff.InvestApi;
using Tinkoff.InvestApi.V1;
using Instrument = Oid85.FinMarket.Storage.Core.Models.Instrument;

namespace Oid85.FinMarket.Storage.Infrastructure.Adapters;

public class GetBondCouponsHelper(
    ILogger logger,
    InvestApiClient client)
{
    private const int DelayInMilliseconds = 1000;
    
    public async Task<List<BondCoupon>> GetBondCouponsAsync(List<Instrument> instruments)
    {
        await Task.Delay(DelayInMilliseconds);
            
        var bondCoupons = new List<BondCoupon>();
            
        foreach (var instrument in instruments)
        {
            await Task.Delay(DelayInMilliseconds);

            var request = CreateGetBondCouponsRequest(instrument.InstrumentId);
            var response = await SendGetBondCouponsRequest(request);

            if (response is null)
                continue;

            if (response.Events is not null)
                foreach (var coupon in response.Events)
                    if (coupon is not null)
                    {
                        var bondCoupon = new BondCoupon
                        {
                            InstrumentId = instrument.InstrumentId,
                            Ticker = instrument.Ticker,
                            CouponNumber = coupon.CouponNumber,
                            CouponPeriod = coupon.CouponPeriod,
                            CouponDate = TimestampToDateOnly(coupon.CouponDate),
                            PayOneBond = MoneyValueToDouble(coupon.PayOneBond)
                        };

                        bondCoupons.Add(bondCoupon);   
                    }
        }

        return bondCoupons;
    }
    
    private static GetBondCouponsRequest CreateGetBondCouponsRequest(Guid instrumentId) =>
        new()
        {
            InstrumentId = instrumentId.ToString(),
            From = DateOnlyToTimestamp(DateTime.Today.AddYears(-1)),
            To = DateOnlyToTimestamp(DateTime.Today.AddYears(1))
        };
    
    private async Task<GetBondCouponsResponse?> SendGetBondCouponsRequest(GetBondCouponsRequest request)
    {
        try
        {
            return await client.Instruments.GetBondCouponsAsync(request);
        }
        
        catch (Exception exception)
        {
            logger.Error(exception, "Ошибка получения данных. {request}", request);
            return null;
        }
    }

    private static Timestamp DateOnlyToTimestamp(DateTime dateTime) =>
        Timestamp.FromDateTime(dateTime.ToUniversalTime());

    private static DateOnly TimestampToDateOnly(Timestamp timestamp)
    {
        if (timestamp is null)
            return DateOnly.MinValue;

        return DateOnly.FromDateTime(timestamp.ToDateTime());
    }

    private static double MoneyValueToDouble(MoneyValue moneyValue)
    {
        if (moneyValue is null)
            return 0.0;

        return moneyValue.Units + moneyValue.Nano / 1_000_000_000.0;
    }
}