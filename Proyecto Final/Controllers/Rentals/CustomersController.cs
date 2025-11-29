using Application.DTOs.Rentals;
using Application.UseCases.Rentals.Customers;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers.Rentals;

[ApiController]
[Route("api/rentals/customers")]
public class CustomersController : ControllerBase
{
    private readonly CreateCustomerUseCase _createCustomerUseCase;
    private readonly GetAllCustomersUseCase _getAllCustomersUseCase;
    private readonly GetCustomerByIdUseCase _getCustomerByIdUseCase;
    private readonly UpdateCustomerUseCase _updateCustomerUseCase;
    private readonly DeleteCustomerUseCase _deleteCustomerUseCase;
    private readonly ILogger<CustomersController> _logger;

    public CustomersController(
        CreateCustomerUseCase createCustomerUseCase,
        GetAllCustomersUseCase getAllCustomersUseCase,
        GetCustomerByIdUseCase getCustomerByIdUseCase,
        UpdateCustomerUseCase updateCustomerUseCase,
        DeleteCustomerUseCase deleteCustomerUseCase,
        ILogger<CustomersController> logger)
    {
        _createCustomerUseCase = createCustomerUseCase;
        _getAllCustomersUseCase = getAllCustomersUseCase;
        _getCustomerByIdUseCase = getCustomerByIdUseCase;
        _updateCustomerUseCase = updateCustomerUseCase;
        _deleteCustomerUseCase = deleteCustomerUseCase;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var customers = await _getAllCustomersUseCase.ExecuteAsync();
            return Ok(customers);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener clientes");
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var customer = await _getCustomerByIdUseCase.ExecuteAsync(id);
            return Ok(customer);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener cliente {CustomerId}", id);
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerDto dto)
    {
        try
        {
            var customer = await _createCustomerUseCase.ExecuteAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear cliente");
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCustomerDto dto)
    {
        try
        {
            var customer = await _updateCustomerUseCase.ExecuteAsync(id, dto);
            return Ok(customer);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar cliente {CustomerId}", id);
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _deleteCustomerUseCase.ExecuteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar cliente {CustomerId}", id);
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }
}
