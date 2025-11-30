using Application.DTOs.Rentals;
using Application.UseCases.Rentals.Places;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers.Rentals;

[ApiController]
[Route("api/rentals/places")]
[Authorize]
public class PlacesController : ControllerBase
{
    private readonly CreatePlaceUseCase _createPlaceUseCase;
    private readonly GetAllPlacesUseCase _getAllPlacesUseCase;
    private readonly GetPlaceByIdUseCase _getPlaceByIdUseCase;
    private readonly UpdatePlaceUseCase _updatePlaceUseCase;
    private readonly DeletePlaceUseCase _deletePlaceUseCase;
    private readonly ILogger<PlacesController> _logger;

    public PlacesController(
        CreatePlaceUseCase createPlaceUseCase,
        GetAllPlacesUseCase getAllPlacesUseCase,
        GetPlaceByIdUseCase getPlaceByIdUseCase,
        UpdatePlaceUseCase updatePlaceUseCase,
        DeletePlaceUseCase deletePlaceUseCase,
        ILogger<PlacesController> logger)
    {
        _createPlaceUseCase = createPlaceUseCase;
        _getAllPlacesUseCase = getAllPlacesUseCase;
        _getPlaceByIdUseCase = getPlaceByIdUseCase;
        _updatePlaceUseCase = updatePlaceUseCase;
        _deletePlaceUseCase = deletePlaceUseCase;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlaceDto>>> GetAll()
    {
        try
        {
            var places = await _getAllPlacesUseCase.ExecuteAsync();
            return Ok(places);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener lugares");
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PlaceDto>> GetById(Guid id)
    {
        try
        {
            var place = await _getPlaceByIdUseCase.ExecuteAsync(id);
            return Ok(place);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener lugar {PlaceId}", id);
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpPost]
    public async Task<ActionResult<PlaceDto>> Create([FromBody] CreatePlaceDto dto)
    {
        try
        {
            var place = await _createPlaceUseCase.ExecuteAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = place.Id }, place);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear lugar");
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PlaceDto>> Update(Guid id, [FromBody] UpdatePlaceDto dto)
    {
        try
        {
            var place = await _updatePlaceUseCase.ExecuteAsync(id, dto);
            return Ok(place);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar lugar {PlaceId}", id);
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await _deletePlaceUseCase.ExecuteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar lugar {PlaceId}", id);
            return StatusCode(500, "Error interno del servidor");
        }
    }
}
