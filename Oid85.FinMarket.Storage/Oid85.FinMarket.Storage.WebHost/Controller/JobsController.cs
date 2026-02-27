using Microsoft.AspNetCore.Mvc;
using Oid85.FinMarket.Storage.Application.Interfaces.Services;
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
    public async Task<IActionResult> LoadInstrumentsAsync()
    {
        await jobService.LoadInstrumentsAsync();
        return Ok();
    }

    /// <summary>
    /// Загрузить свечи
    /// </summary>
    [HttpPost("load-candles")]
    public async Task<IActionResult> LoadCandlesAsync()
    {
        await jobService.LoadCandlesAsync();
        return Ok();
    }

    /// <summary>
    /// Загрузить купоны
    /// </summary>
    [HttpPost("load-bond-coupons")]
    public async Task<IActionResult> LoadBondCouponsAsync()
    {
        await jobService.LoadBondCouponsAsync();
        return Ok();
    }
}