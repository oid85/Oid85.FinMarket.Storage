using Microsoft.AspNetCore.Mvc;
using Oid85.FinMarket.Storage.Application.Interfaces.Services;
using Oid85.FinMarket.Storage.Core;
using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;
using Oid85.FinMarket.Storage.WebHost.Controller.Base;

namespace Oid85.FinMarket.Storage.WebHost.Controller;

/// <summary>
/// ВВП
/// </summary>
[Route("api/vvp")]
[ApiController]
public class VvpController(
    IVvpService vvpService)
    : BaseController
{
    /// <summary>
    /// Получить данные по ВВП
    /// </summary>
    [HttpPost("list")]
    [ProducesResponseType(typeof(BaseResponse<GetVvpListResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<GetVvpListResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<GetVvpListResponse>), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> GetVvpListAsync(
        [FromBody] GetVvpListRequest request) =>
        GetResponseAsync(
            () => vvpService.GetVvpListAsync(request),
            result => new BaseResponse<GetVvpListResponse> { Result = result });
}