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
                            CouponDate = ConvertHelper.TimestampToDateOnly(coupon.CouponDate),
                            PayOneBond = ConvertHelper.MoneyValueToDouble(coupon.PayOneBond)
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
            From = ConvertHelper.DateTimeToTimestamp(DateTime.Today.AddYears(-1)),
            To = ConvertHelper.DateTimeToTimestamp(DateTime.Today.AddYears(1))
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
}