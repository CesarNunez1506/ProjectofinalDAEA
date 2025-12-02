using Application.DTOs.Inventory;
using Application.UseCases.Inventory.Commands;
using Application.UseCases.Inventory.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers.Inventory;

[ApiController]
[Route("api/inventory/[controller]")]
[Authorize]
public class WarehousesController : ControllerBase
{
    private readonly IMediator _mediator;

    public WarehousesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtener todos los almacenes
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WarehouseDto>>> GetAll()
    {
        var warehouses = await _mediator.Send(new GetAllWarehousesQuery());
        return Ok(warehouses);
    }

    /// <summary>
    /// Obtener almacén por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<WarehouseDto>> GetById(Guid id)
    {
        var warehouse = await _mediator.Send(new GetWarehouseByIdQuery(id));
        if (warehouse == null)
        {
            return NotFound(new { message = "Almacén no encontrado" });
        }
        return Ok(warehouse);
    }

    /// <summary>
    /// Obtener almacenes activos
    /// </summary>
    [HttpGet("active")]
    public async Task<ActionResult<IEnumerable<WarehouseDto>>> GetActive()
    {
        var warehouses = await _mediator.Send(new GetActiveWarehousesQuery());
        return Ok(warehouses);
    }

    /// <summary>
    /// Obtener stock de un almacén
    /// </summary>
    [HttpGet("{id}/stock")]
    public async Task<ActionResult<IEnumerable<WarehouseProductDto>>> GetStock(Guid id)
    {
        var stock = await _mediator.Send(new GetWarehouseStockQuery(id));
        return Ok(stock);
    }

    /// <summary>
    /// Obtener stock de un producto en un almacén
    /// </summary>
    [HttpGet("{warehouseId}/products/{productId}/stock")]
    public async Task<ActionResult<int>> GetProductStock(Guid warehouseId, Guid productId)
    {
        var stock = await _mediator.Send(new GetWarehouseProductStockQuery(warehouseId, productId));
        return Ok(new { warehouseId, productId, stock });
    }

    /// <summary>
    /// Crear un nuevo almacén
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<WarehouseDto>> Create([FromBody] CreateWarehouseDto dto)
    {
        try
        {
            var warehouse = await _mediator.Send(new CreateWarehouseCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id = warehouse.Id }, warehouse);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Actualizar un almacén
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<WarehouseDto>> Update(Guid id, [FromBody] UpdateWarehouseDto dto)
    {
        try
        {
            dto.Id = id;
            var warehouse = await _mediator.Send(new UpdateWarehouseCommand(dto));
            return Ok(warehouse);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Eliminar un almacén
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await _mediator.Send(new DeleteWarehouseCommand(id));
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Agregar producto a un almacén
    /// </summary>
    [HttpPost("add-product")]
    public async Task<ActionResult> AddProduct([FromBody] AddWarehouseProductDto dto)
    {
        try
        {
            await _mediator.Send(new AddProductToWarehouseCommand(dto));
            return Ok(new { message = "Producto agregado al almacén correctamente" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Ajustar stock de un producto
    /// </summary>
    [HttpPost("adjust-stock")]
    public async Task<ActionResult> AdjustStock([FromBody] AdjustStockDto dto)
    {
        try
        {
            await _mediator.Send(new AdjustProductStockCommand(dto));
            return Ok(new { message = "Stock ajustado correctamente" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Transferir productos entre almacenes
    /// </summary>
    [HttpPost("transfer")]
    public async Task<ActionResult> Transfer([FromBody] TransferProductDto dto)
    {
        try
        {
            await _mediator.Send(new TransferProductBetweenWarehousesCommand(dto));
            return Ok(new { message = "Transferencia realizada correctamente" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
