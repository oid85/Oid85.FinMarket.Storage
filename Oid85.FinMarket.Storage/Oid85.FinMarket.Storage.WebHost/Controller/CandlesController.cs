using Microsoft.AspNetCore.Mvc;
using Oid85.FinMarket.Storage.Application.Interfaces.Services;
using Oid85.FinMarket.Storage.Core;
using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;
using Oid85.FinMarket.Storage.WebHost.Controller.Base;

namespace Oid85.FinMarket.Storage.WebHost.Controller;

/// <summary>
/// Свечи
/// </summary>
[Route("api/candles")]
[ApiController]
public class CandlesController(
    ICandleService candleService)
    : BaseController
{
    /// <summary>
    /// Получить свечи
    /// </summary>
    [HttpPost("list")]
    [ProducesResponseType(typeof(BaseResponse<GetCandleListResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<GetCandleListResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<GetCandleListResponse>), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> GetCandleListAsync(
        [FromBody] GetCandleListRequest request) =>
        GetResponseAsync(
            () => candleService.GetCandleListAsync(request),
            result => new BaseResponse<GetCandleListResponse> { Result = result });

    /// <summary>
    /// Получить последнюю свечу
    /// </summary>
    [HttpPost("last")]
    [ProducesResponseType(typeof(BaseResponse<GetLastCandleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<GetLastCandleResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<GetLastCandleResponse>), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> GetLastCandleAsync(
        [FromBody] GetLastCandleRequest request) =>
        GetResponseAsync(
            () => candleService.GetLastCandleAsync(request),
            result => new BaseResponse<GetLastCandleResponse> { Result = result });
}