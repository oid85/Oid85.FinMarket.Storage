using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;

namespace Oid85.FinMarket.Storage.Application.Interfaces.Services
{
    /// <summary>
    /// Сервис работы с купонами облигаций
    /// </summary>
    public interface IBondCouponService
    {
        /// <summary>
        /// Получить купоны
        /// </summary>
        Task<GetBondCouponListResponse> GetBondCouponListAsync(GetBondCouponListRequest request);

        /// <summary>
        /// Загрузить купоны
        /// </summary>
        Task LoadBondCouponsAsync();
    }
}
