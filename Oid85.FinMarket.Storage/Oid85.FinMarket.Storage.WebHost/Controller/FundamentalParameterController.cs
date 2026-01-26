using Microsoft.AspNetCore.Mvc;
using Oid85.FinMarket.Storage.Application.Interfaces.Repositories;
using Oid85.FinMarket.Storage.Application.Interfaces.Services;
using Oid85.FinMarket.Storage.Core;
using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;
using Oid85.FinMarket.Storage.WebHost.Controller.Base;

namespace Oid85.FinMarket.Storage.WebHost.Controller;

/// <summary>
/// Фундаментальные параметры
/// </summary>
[Route("api/fundamental-parameters")]
[ApiController]
public class FundamentalParameterController(
    IFundamentalParameterService fundamentalParameterService)
    : BaseController
{
    /// <summary>
    /// Получить фундаментальные параметры
    /// </summary>
    [HttpPost("list")]
    [ProducesResponseType(typeof(BaseResponse<GetFundamentalParameterListResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<GetFundamentalParameterListResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<GetFundamentalParameterListResponse>), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> GetFundamentalParameterListAsync(
        [FromBody] GetFundamentalParameterListRequest request) =>
        GetResponseAsync(
            () => fundamentalParameterService.GetFundamentalParameterListAsync(request),
            result => new BaseResponse<GetFundamentalParameterListResponse> { Result = result });

    /// <summary>
    /// Создать или изменить фундаментальный параметр
    /// </summary>
    [HttpPost("create-or-update")]
    [ProducesResponseType(typeof(BaseResponse<CreateOrUpdateFundamentalParameterResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<CreateOrUpdateFundamentalParameterResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<CreateOrUpdateFundamentalParameterResponse>), StatusCodes.Status500InternalServerError)]
    public Task<IActionResult> CreateOrUpdateFundamentalParameterAsync(
        [FromBody] CreateOrUpdateFundamentalParameterRequest request) =>
        GetResponseAsync(
            () => fundamentalParameterService.CreateOrUpdateFundamentalParameterAsync(request),
            result => new BaseResponse<CreateOrUpdateFundamentalParameterResponse> { Result = result });
}