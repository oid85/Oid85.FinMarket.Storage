using Microsoft.AspNetCore.Mvc;
using Oid85.FinMarket.Storage.Application.Interfaces.Services;
using Oid85.FinMarket.Storage.Core;
using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;
using Oid85.FinMarket.Storage.WebHost.Controller.Base;

namespace Oid85.FinMarket.Storage.WebHost.Controller;

/// <summary>
/// Инструменты
/// </summary>
[Route("api/instruments")]
[ApiController]
public class InstrumentsController(
    IInstrumentService instrumentService)
    : BaseController
{
    /// <summary>
    /// Получить инструменты
    /// </summary>
    [HttpPost("list")]
    [ProducesResponseType(typeof(BaseResponse<GetInstrumentListResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<GetInstrumentListResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<GetInstrumentListResponse>), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> GetInstrumentListAsync(
        [FromBody] GetInstrumentListRequest request) =>
        GetResponseAsync(
            () => instrumentService.GetInstrumentListAsync(request),
            result => new BaseResponse<GetInstrumentListResponse> { Result = result });
}