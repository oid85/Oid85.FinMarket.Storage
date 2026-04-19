using Microsoft.AspNetCore.Mvc;
using Oid85.FinMarket.Storage.Application.Interfaces.Services;
using Oid85.FinMarket.Storage.Core;
using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;
using Oid85.FinMarket.Storage.WebHost.Controller.Base;

namespace Oid85.FinMarket.Storage.WebHost.Controller;

/// <summary>
/// Эмитенты
/// </summary>
[Route("api/emitents")]
[ApiController]
public class EmitentController(
    IEmitentService emitentService)
    : BaseController
{
    /// <summary>
    /// Получить фундаментальные параметры
    /// </summary>
    [HttpPost("list")]
    [ProducesResponseType(typeof(BaseResponse<GetEmitentListResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<GetEmitentListResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<GetEmitentListResponse>), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> GetEmitentListAsync(
        [FromBody] GetEmitentListRequest request) =>
        GetResponseAsync(
            () => emitentService.GetEmitentListAsync(request),
            result => new BaseResponse<GetEmitentListResponse> { Result = result });

    /// <summary>
    /// Создать или изменить фундаментальный параметр
    /// </summary>
    [HttpPost("create-or-update")]
    [ProducesResponseType(typeof(BaseResponse<CreateOrUpdateEmitentResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<CreateOrUpdateEmitentResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<CreateOrUpdateEmitentResponse>), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> CreateOrUpdateEmitentAsync(
        [FromBody] CreateOrUpdateEmitentRequest request) =>
        GetResponseAsync(
            () => emitentService.CreateOrUpdateEmitentAsync(request),
            result => new BaseResponse<CreateOrUpdateEmitentResponse> { Result = result });
}