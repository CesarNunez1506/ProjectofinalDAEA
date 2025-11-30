using Application.DTOs.Rentals;
using Application.UseCases.Rentals;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers.Rentals;

[ApiController]
[Route("api/rentals")]
public class RentalsController : ControllerBase
{
    private readonly CreateRentalUseCase _createRentalUseCase;
    private readonly GetAllRentalsUseCase _getAllRentalsUseCase;
    private readonly GetRentalByIdUseCase _getRentalByIdUseCase;
    private readonly UpdateRentalUseCase _updateRentalUseCase;
    private readonly ToggleRentalStatusUseCase _toggleRentalStatusUseCase;
    private readonly ILogger<RentalsController> _logger;

    public RentalsController(
        CreateRentalUseCase createRentalUseCase,
        GetAllRentalsUseCase getAllRentalsUseCase,
        GetRentalByIdUseCase getRentalByIdUseCase,
        UpdateRentalUseCase updateRentalUseCase,
        ToggleRentalStatusUseCase toggleRentalStatusUseCase,
        ILogger<RentalsController> logger)
    {
        _createRentalUseCase = createRentalUseCase;
        _getAllRentalsUseCase = getAllRentalsUseCase;
        _getRentalByIdUseCase = getRentalByIdUseCase;
        _updateRentalUseCase = updateRentalUseCase;
        _toggleRentalStatusUseCase = toggleRentalStatusUseCase;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RentalDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var rentals = await _getAllRentalsUseCase.ExecuteAsync();
            return Ok(rentals);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener alquileres");
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RentalDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var rental = await _getRentalByIdUseCase.ExecuteAsync(id);
            return Ok(rental);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener alquiler {RentalId}", id);
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(RentalCreatedResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateRentalDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _createRentalUseCase.ExecuteAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear alquiler");
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(RentalDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRentalDto dto)
    {
        try
        {
            var rental = await _updateRentalUseCase.ExecuteAsync(id, dto);
            return Ok(rental);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar alquiler {RentalId}", id);
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    [HttpPatch("{id}/toggle-status")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ToggleStatus(Guid id)
    {
        try
        {
            var newStatus = await _toggleRentalStatusUseCase.ExecuteAsync(id);
            return Ok(new { message = "Estado actualizado correctamente", status = newStatus });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al cambiar estado del alquiler {RentalId}", id);
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }
}
