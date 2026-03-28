using Microsoft.AspNetCore.Mvc;
using Oid85.FinMarket.Storage.Application.Interfaces.Services;
using Oid85.FinMarket.Storage.Application.Services;
using Oid85.FinMarket.Storage.Core;
using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;
using Oid85.FinMarket.Storage.WebHost.Controller.Base;

namespace Oid85.FinMarket.Storage.WebHost.Controller;

/// <summary>
/// Прогнозы
/// </summary>
[Route("api/forecasts")]
[ApiController]
public class ForecastsController(
    IForecastService forecastsService)
    : BaseController
{
    /// <summary>
    /// Получить прогнозы
    /// </summary>
    [HttpPost("list")]
    [ProducesResponseType(typeof(BaseResponse<GetForecastListResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<GetForecastListResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<GetForecastListResponse>), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> GetForecastListAsync(
        [FromBody] GetForecastListRequest request) =>
        GetResponseAsync(
            () => forecastsService.GetForecastListAsync(request),
            result => new BaseResponse<GetForecastListResponse> { Result = result });
}