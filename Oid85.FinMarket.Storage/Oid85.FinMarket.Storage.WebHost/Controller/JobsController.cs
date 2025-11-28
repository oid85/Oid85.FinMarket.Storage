using Microsoft.AspNetCore.Mvc;
using Oid85.FinMarket.Storage.Application.Interfaces.Services;
using Oid85.FinMarket.Storage.Core;
using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;
using Oid85.FinMarket.Storage.WebHost.Controller.Base;

namespace Oid85.FinMarket.Storage.WebHost.Controller;

/// <summary>
/// Задачи по расписанию
/// </summary>
[Route("api/jobs")]
[ApiController]
public class JobsController(
    IJobService jobService)
    : BaseController
{
    /// <summary>
    /// Загрузить инструменты
    /// </summary>
    [HttpPost("load-instruments")]
    [ProducesResponseType(typeof(BaseResponse<LoadInstrumentsResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<LoadInstrumentsResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<LoadInstrumentsResponse>), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> LoadInstrumentsAsync() =>
        GetResponseAsync(
            () => jobService.LoadInstrumentsAsync(),
            result => new BaseResponse<LoadInstrumentsResponse> { Result = result });

    /// <summary>
    /// Загрузить свечи
    /// </summary>
    [HttpPost("load-candles")]
    [ProducesResponseType(typeof(BaseResponse<LoadCandlesResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<LoadCandlesResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<LoadCandlesResponse>), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> LoadCandlesAsync() =>
        GetResponseAsync(
            () => jobService.LoadCandlesAsync(),
            result => new BaseResponse<LoadCandlesResponse> { Result = result });
}