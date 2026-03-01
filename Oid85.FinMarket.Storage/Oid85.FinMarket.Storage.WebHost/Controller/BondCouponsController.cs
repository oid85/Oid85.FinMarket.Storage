using Microsoft.AspNetCore.Mvc;
using Oid85.FinMarket.Storage.Application.Interfaces.Services;
using Oid85.FinMarket.Storage.Core;
using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;
using Oid85.FinMarket.Storage.WebHost.Controller.Base;

namespace Oid85.FinMarket.Storage.WebHost.Controller;

/// <summary>
/// Купоны
/// </summary>
[Route("api/bond-coupons")]
[ApiController]
public class BondCouponsController(
    IBondCouponService bondCouponService)
    : BaseController
{
    /// <summary>
    /// Получить купоны
    /// </summary>
    [HttpPost("list")]
    [ProducesResponseType(typeof(BaseResponse<GetBondCouponListResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<GetBondCouponListResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<GetBondCouponListResponse>), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> GetCandleListAsync(
        [FromBody] GetBondCouponListRequest request) =>
        GetResponseAsync(
            () => bondCouponService.GetBondCouponListAsync(request),
            result => new BaseResponse<GetBondCouponListResponse> { Result = result });
}