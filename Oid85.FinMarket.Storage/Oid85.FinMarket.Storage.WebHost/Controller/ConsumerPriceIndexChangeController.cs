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
[Route("api/consumer-price-indexes")]
[ApiController]
public class ConsumerPriceIndexChangeController(
    IConsumerPriceIndexChangeService consumerPriceIndexChangeService)
    : BaseController
{
    /// <summary>
    /// Получить фундаментальные параметры
    /// </summary>
    [HttpPost("list")]
    [ProducesResponseType(typeof(BaseResponse<GetConsumerPriceIndexChangeListResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<GetConsumerPriceIndexChangeListResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<GetConsumerPriceIndexChangeListResponse>), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> GetConsumerPriceIndexChangeListAsync(
        [FromBody] GetConsumerPriceIndexChangeListRequest request) =>
        GetResponseAsync(
            () => consumerPriceIndexChangeService.GetConsumerPriceIndexChangeListAsync(request),
            result => new BaseResponse<GetConsumerPriceIndexChangeListResponse> { Result = result });

    /// <summary>
    /// Создать или изменить фундаментальный параметр
    /// </summary>
    [HttpPost("create-or-update")]
    [ProducesResponseType(typeof(BaseResponse<CreateOrUpdateConsumerPriceIndexChangeResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<CreateOrUpdateConsumerPriceIndexChangeResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<CreateOrUpdateConsumerPriceIndexChangeResponse>), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> CreateOrUpdateConsumerPriceIndexChangeAsync(
        [FromBody] CreateOrUpdateConsumerPriceIndexChangeRequest request) =>
        GetResponseAsync(
            () => consumerPriceIndexChangeService.CreateOrUpdateConsumerPriceIndexChangeAsync(request),
            result => new BaseResponse<CreateOrUpdateConsumerPriceIndexChangeResponse> { Result = result });
}