using Microsoft.AspNetCore.Mvc;
using Oid85.FinMarket.Storage.Application.Interfaces.Services;
using Oid85.FinMarket.Storage.Core;
using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;
using Oid85.FinMarket.Storage.WebHost.Controller.Base;

namespace Oid85.FinMarket.Storage.WebHost.Controller;

/// <summary>
/// Дивиденды
/// </summary>
[Route("api/dividends")]
[ApiController]
public class DividendsController(
    IDividendService dividendService)
    : BaseController
{
    /// <summary>
    /// Получить дивиденды
    /// </summary>
    [HttpPost("list")]
    [ProducesResponseType(typeof(BaseResponse<GetDividendListResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<GetDividendListResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<GetDividendListResponse>), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> GetDividendListAsync(
        [FromBody] GetDividendListRequest request) =>
        GetResponseAsync(
            () => dividendService.GetDividendListAsync(request),
            result => new BaseResponse<GetDividendListResponse> { Result = result });
}