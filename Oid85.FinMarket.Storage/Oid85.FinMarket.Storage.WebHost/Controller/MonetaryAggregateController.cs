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
[Route("api/monetary-aggregates")]
[ApiController]
public class MonetaryAggregateController(
    IMonetaryAggregateService monetaryAggregateService)
    : BaseController
{
    /// <summary>
    /// Получить фундаментальные параметры
    /// </summary>
    [HttpPost("list")]
    [ProducesResponseType(typeof(BaseResponse<GetMonetaryAggregateListResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<GetMonetaryAggregateListResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<GetMonetaryAggregateListResponse>), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> GetMonetaryAggregateListAsync(
        [FromBody] GetMonetaryAggregateListRequest request) =>
        GetResponseAsync(
            () => monetaryAggregateService.GetMonetaryAggregateListAsync(request),
            result => new BaseResponse<GetMonetaryAggregateListResponse> { Result = result });

    /// <summary>
    /// Создать или изменить фундаментальный параметр
    /// </summary>
    [HttpPost("create-or-update")]
    [ProducesResponseType(typeof(BaseResponse<CreateOrUpdateMonetaryAggregateResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<CreateOrUpdateMonetaryAggregateResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<CreateOrUpdateMonetaryAggregateResponse>), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> CreateOrUpdateMonetaryAggregateAsync(
        [FromBody] CreateOrUpdateMonetaryAggregateRequest request) =>
        GetResponseAsync(
            () => monetaryAggregateService.CreateOrUpdateMonetaryAggregateAsync(request),
            result => new BaseResponse<CreateOrUpdateMonetaryAggregateResponse> { Result = result });
}