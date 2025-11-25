using Application.DTOs.Inventory;
using Application.UseCases.Inventory.Commands;
using Application.UseCases.Inventory.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers.Inventory;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SuppliersController : ControllerBase
{
    private readonly IMediator _mediator;

    public SuppliersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtener todos los proveedores
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SupplierDto>>> GetAll()
    {
        var suppliers = await _mediator.Send(new GetAllSuppliersQuery());
        return Ok(suppliers);
    }

    /// <summary>
    /// Obtener proveedor por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierDto>> GetById(Guid id)
    {
        var supplier = await _mediator.Send(new GetSupplierByIdQuery(id));
        if (supplier == null)
        {
            return NotFound(new { message = "Proveedor no encontrado" });
        }
        return Ok(supplier);
    }

    /// <summary>
    /// Obtener proveedores activos
    /// </summary>
    [HttpGet("active")]
    public async Task<ActionResult<IEnumerable<SupplierDto>>> GetActive()
    {
        var suppliers = await _mediator.Send(new GetActiveSuppliersQuery());
        return Ok(suppliers);
    }

    /// <summary>
    /// Buscar proveedor por RUC
    /// </summary>
    [HttpGet("ruc/{ruc}")]
    public async Task<ActionResult<SupplierDto>> GetByRuc(long ruc)
    {
        var supplier = await _mediator.Send(new GetSupplierByRucQuery(ruc));
        if (supplier == null)
        {
            return NotFound(new { message = "Proveedor no encontrado" });
        }
        return Ok(supplier);
    }

    /// <summary>
    /// Buscar proveedores por nombre
    /// </summary>
    [HttpGet("search/{name}")]
    public async Task<ActionResult<IEnumerable<SupplierDto>>> SearchByName(string name)
    {
        var suppliers = await _mediator.Send(new SearchSuppliersByNameQuery(name));
        return Ok(suppliers);
    }

    /// <summary>
    /// Crear un nuevo proveedor
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<SupplierDto>> Create([FromBody] CreateSupplierDto dto)
    {
        try
        {
            var supplier = await _mediator.Send(new CreateSupplierCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id = supplier.Id }, supplier);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Actualizar un proveedor
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<SupplierDto>> Update(Guid id, [FromBody] UpdateSupplierDto dto)
    {
        try
        {
            dto.Id = id;
            var supplier = await _mediator.Send(new UpdateSupplierCommand(dto));
            return Ok(supplier);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Eliminar un proveedor
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await _mediator.Send(new DeleteSupplierCommand(id));
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
