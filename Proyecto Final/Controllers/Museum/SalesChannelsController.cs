using Application.DTOs.Museum;
using Application.UseCases.Museum.SalesChannels.Commands;
using Application.UseCases.Museum.SalesChannels.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers.Museum;

// Controlador para gestionar los canales de venta del museo
[ApiController]
[Route("api/museum/sales-channels")]
[Tags("Museum")]
public class SalesChannelsController : ControllerBase
{
    private readonly CreateSalesChannelUseCase _createSalesChannel;
    private readonly UpdateSalesChannelUseCase _updateSalesChannel;
    private readonly DeleteSalesChannelUseCase _deleteSalesChannel;
    private readonly GetAllSalesChannelsUseCase _getAllSalesChannels;
    private readonly GetSalesChannelByIdUseCase _getSalesChannelById;

    public SalesChannelsController(
        CreateSalesChannelUseCase createSalesChannel,
        UpdateSalesChannelUseCase updateSalesChannel,
        DeleteSalesChannelUseCase deleteSalesChannel,
        GetAllSalesChannelsUseCase getAllSalesChannels,
        GetSalesChannelByIdUseCase getSalesChannelById)
    {
        _createSalesChannel = createSalesChannel;
        _updateSalesChannel = updateSalesChannel;
        _deleteSalesChannel = deleteSalesChannel;
        _getAllSalesChannels = getAllSalesChannels;
        _getSalesChannelById = getSalesChannelById;
    }

    // Controlador para gestionar los canales de venta del museo
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var salesChannels = await _getAllSalesChannels.ExecuteAsync();
            return Ok(salesChannels);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    // Controlador para gestionar los canales de venta del museo
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var salesChannel = await _getSalesChannelById.ExecuteAsync(id);
            return Ok(salesChannel);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    // Controlador para gestionar los canales de venta del museo
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSalesChannelDto dto)
    {
        try
        {
            var salesChannel = await _createSalesChannel.ExecuteAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = salesChannel.Id }, salesChannel);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    // Controlador para gestionar los canales de venta del museo
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSalesChannelDto dto)
    {
        try
        {
            var salesChannel = await _updateSalesChannel.ExecuteAsync(id, dto);
            return Ok(salesChannel);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    // Controlador para gestionar los canales de venta del museo
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _deleteSalesChannel.ExecuteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }
}
