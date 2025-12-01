using Application.DTOs.Sales;
using Application.Features.Sales.CashSessions;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CashSessionsController : ControllerBase
{
    private readonly OpenCashSessionUseCase _openSession;
    private readonly CloseCashSessionUseCase _closeSession;
    private readonly AddCashMovementUseCase _addMovement;
    private readonly ReconcileCashUseCase _reconcileCash;

    public CashSessionsController(
        OpenCashSessionUseCase openSession,
        CloseCashSessionUseCase closeSession,
        AddCashMovementUseCase addMovement,
        ReconcileCashUseCase reconcileCash)
    {
        _openSession = openSession;
        _closeSession = closeSession;
        _addMovement = addMovement;
        _reconcileCash = reconcileCash;
    }

    [HttpPost("open")]
    public async Task<IActionResult> Open([FromBody] OpenCashSessionDto dto)
    {
        return Ok(await _openSession.ExecuteAsync(dto));
    }

    [HttpPost("close")]
    public async Task<IActionResult> Close([FromBody] CloseCashSessionDto dto)
    {
        return Ok(await _closeSession.ExecuteAsync(dto));
    }

    [HttpPost("movement")]
    public async Task<IActionResult> AddMovement([FromBody] CashMovementDto dto)
    {
        return Ok(await _addMovement.ExecuteAsync(dto));
    }

    [HttpPost("reconcile")]
    public async Task<IActionResult> Reconcile([FromBody] ReconcileCashDto dto)
    {
        return Ok(await _reconcileCash.ExecuteAsync(dto));
    }
}
