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
    [ProducesResponseType(typeof(BaseResponse<LoadInstrumentResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<LoadInstrumentResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<LoadInstrumentResponse>), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> GetInstrumentListAsync() =>
        GetResponseAsync(
            () => jobService.LoadInstrumentsAsync(),
            result => new BaseResponse<LoadInstrumentResponse> { Result = result });
}