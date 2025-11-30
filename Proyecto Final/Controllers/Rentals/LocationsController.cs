using Application.DTOs.Rentals;
using Application.UseCases.Rentals.Locations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers.Rentals;

[ApiController]
[Route("api/rentals/locations")]
[Authorize]
public class LocationsController : ControllerBase
{
    private readonly CreateLocationUseCase _createLocationUseCase;
    private readonly GetAllLocationsUseCase _getAllLocationsUseCase;
    private readonly GetLocationByIdUseCase _getLocationByIdUseCase;
    private readonly UpdateLocationUseCase _updateLocationUseCase;
    private readonly DeleteLocationUseCase _deleteLocationUseCase;
    private readonly ILogger<LocationsController> _logger;

    public LocationsController(
        CreateLocationUseCase createLocationUseCase,
        GetAllLocationsUseCase getAllLocationsUseCase,
        GetLocationByIdUseCase getLocationByIdUseCase,
        UpdateLocationUseCase updateLocationUseCase,
        DeleteLocationUseCase deleteLocationUseCase,
        ILogger<LocationsController> logger)
    {
        _createLocationUseCase = createLocationUseCase;
        _getAllLocationsUseCase = getAllLocationsUseCase;
        _getLocationByIdUseCase = getLocationByIdUseCase;
        _updateLocationUseCase = updateLocationUseCase;
        _deleteLocationUseCase = deleteLocationUseCase;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LocationDto>>> GetAll()
    {
        try
        {
            var locations = await _getAllLocationsUseCase.ExecuteAsync();
            return Ok(locations);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener ubicaciones");
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LocationDto>> GetById(Guid id)
    {
        try
        {
            var location = await _getLocationByIdUseCase.ExecuteAsync(id);
            return Ok(location);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener ubicación {LocationId}", id);
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpPost]
    public async Task<ActionResult<LocationDto>> Create([FromBody] CreateLocationDto dto)
    {
        try
        {
            var location = await _createLocationUseCase.ExecuteAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = location.Id }, location);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear ubicación");
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<LocationDto>> Update(Guid id, [FromBody] UpdateLocationDto dto)
    {
        try
        {
            var location = await _updateLocationUseCase.ExecuteAsync(id, dto);
            return Ok(location);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar ubicación {LocationId}", id);
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await _deleteLocationUseCase.ExecuteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar ubicación {LocationId}", id);
            return StatusCode(500, "Error interno del servidor");
        }
    }
}
