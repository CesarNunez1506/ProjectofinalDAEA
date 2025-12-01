using Application.DTOs.Sales;
using Application.Features.Sales.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly ProcessSaleUseCase _processSale;
    private readonly UpdateSaleUseCase _updateSale;
    private readonly CancelSaleUseCase _cancelSale;
    private readonly ProcessRefundUseCase _refundSale;

    public SalesController(
        ProcessSaleUseCase processSale,
        UpdateSaleUseCase updateSale,
        CancelSaleUseCase cancelSale,
        ProcessRefundUseCase refundSale)
    {
        _processSale = processSale;
        _updateSale = updateSale;
        _cancelSale = cancelSale;
        _refundSale = refundSale;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleDto dto)
    {
        var result = await _processSale.ExecuteAsync(dto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSale(Guid id, [FromBody] UpdateSaleDto dto)
    {
        dto.Id = id;
        var result = await _updateSale.ExecuteAsync(dto);
        return Ok(result);
    }

    [HttpPut("cancel/{id}")]
    public async Task<IActionResult> CancelSale(Guid id)
    {
        var result = await _cancelSale.ExecuteAsync(id);
        return Ok(result);
    }

    [HttpPost("refund")]
    public async Task<IActionResult> Refund([FromBody] RefundDto dto)
    {
        var result = await _refundSale.ExecuteAsync(dto);
        return Ok(result);
    }
}
