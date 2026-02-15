using Microsoft.AspNetCore.Mvc;
using Oid85.FinMarket.Storage.Application.Interfaces.Services;
using Oid85.FinMarket.Storage.Core;
using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;
using Oid85.FinMarket.Storage.WebHost.Controller.Base;

namespace Oid85.FinMarket.Storage.WebHost.Controller;

/// <summary>
/// Фундаментальные параметры
/// </summary>
[Route("api/key-rates")]
[ApiController]
public class KeyRateController(
    IKeyRateService keyRateService)
    : BaseController
{
    /// <summary>
    /// Получить фундаментальные параметры
    /// </summary>
    [HttpPost("list")]
    [ProducesResponseType(typeof(BaseResponse<GetKeyRateListResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<GetKeyRateListResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<GetKeyRateListResponse>), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> GetKeyRateListAsync(
        [FromBody] GetKeyRateListRequest request) =>
        GetResponseAsync(
            () => keyRateService.GetKeyRateListAsync(request),
            result => new BaseResponse<GetKeyRateListResponse> { Result = result });

    /// <summary>
    /// Создать или изменить фундаментальный параметр
    /// </summary>
    [HttpPost("create-or-update")]
    [ProducesResponseType(typeof(BaseResponse<CreateOrUpdateKeyRateResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<CreateOrUpdateKeyRateResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<CreateOrUpdateKeyRateResponse>), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> CreateOrUpdateKeyRateAsync(
        [FromBody] CreateOrUpdateKeyRateRequest request) =>
        GetResponseAsync(
            () => keyRateService.CreateOrUpdateKeyRateAsync(request),
            result => new BaseResponse<CreateOrUpdateKeyRateResponse> { Result = result });
}